using AskMe.Utility;
using AskMe.ViewModel;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using Xamarin.Forms;

namespace AskMe.Services
{
    public class BotDirectLineService : IDisposable
    {
        private DirectLineClient client;
        private Conversation conversation;
        WebSocket webSocket;
        string watermark = null;
        MainPageViewModel viewModel;
        private Timer botConnectionRenewer;

        public bool IsMessageSent { get; set; } = false;

        public BotDirectLineService(MainPageViewModel viewModel)
        {
            this.viewModel = viewModel;

            Device.StartTimer(TimeSpan.FromMinutes(9), () =>
            {
                try
                {
                    if (client != null && client.Tokens != null)
                        client.Tokens.RefreshTokenAsync();
                }
                catch (Exception)
                {
                    client = null;
                    conversation = null;
                    webSocket = null;
                }
                return true;
            });

            botConnectionRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback),
                                           this,
                                           TimeSpan.FromMinutes(5),
                                           TimeSpan.FromMilliseconds(-1));

        }

        private async void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                if (webSocket != null)
                {
                    webSocket.OnMessage -= WebSocket_OnMessage;
                    webSocket.OnClose -= WebSocket_OnClose;
                    webSocket.OnError -= WebSocket_OnError;
                }
                webSocket = null;
                client = null;

                await StartBotConversation();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Failed renewing bot connection. Details: {0}", ex.Message));
            }
        }

        public async Task StartBotConversation()
        {
            try
            {
                //var tokenResponse = await new DirectLineClient(Constants.DirectLineSecret).Tokens.GenerateTokenForNewConversationAsync();
                //client = new DirectLineClient(tokenResponse.Token);

                var credential = new DirectLineClientCredentials(Constants.DirectLineSecret, Constants.BotEndPoint);
                client = new DirectLineClient(credential);

                conversation = await client.Conversations.StartConversationAsync();

                webSocket = new WebSocket(conversation.StreamUrl);
                webSocket.OnMessage += WebSocket_OnMessage;
                webSocket.OnClose += WebSocket_OnClose;
                webSocket.OnError += WebSocket_OnError;
                webSocket.ConnectAsync();

                try
                {
                    botConnectionRenewer.Change(TimeSpan.FromMinutes(5), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
                }
            }
            catch (Exception)
            {
                botConnectionRenewer.Change(Timeout.Infinite, Timeout.Infinite);
                if(webSocket != null)
                {
                    webSocket.OnMessage -= WebSocket_OnMessage;
                    webSocket.OnClose -= WebSocket_OnClose;
                    webSocket.OnError -= WebSocket_OnError;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "Problem occurred while connecting with Bot. " + Constants.ErrorString, "OK");
                });
            }
        }

        public async Task SendMessageToBot(string text, object value = null)
        {
            if (client == null || conversation == null)
            {
                await StartBotConversation();
                if (conversation == null)
                    return;
            }

            if (webSocket == null)
            {
                Debug.WriteLine("WebSocket connection closed unexpectedly. Connecting again...");
                webSocket = new WebSocket(conversation.StreamUrl);
                webSocket.OnMessage += WebSocket_OnMessage;
                webSocket.OnClose += WebSocket_OnClose;
                webSocket.OnError += WebSocket_OnError;
                webSocket.Connect();
            }

            if (!string.IsNullOrWhiteSpace(text) || value != null)
            {
                var userActivity = new Activity();
                {
                    userActivity.From = new ChannelAccount(Constants.Email, Constants.Username);
                    userActivity.Value = value;
                    if (!string.IsNullOrWhiteSpace(text))
                        userActivity.Text = text;
                    userActivity.Type = ActivityTypes.Message;
                };
                try
                {
                    await client.Conversations.PostActivityAsync(conversation.ConversationId, userActivity);
                    Debug.WriteLine("Message sent: " + DateTime.Now);
                    IsMessageSent = true;
                    botConnectionRenewer.Change(TimeSpan.FromMinutes(5), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception)
                {
                    conversation = null;
                    webSocket = null;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Error", "Problem occurred while posting query to Bot. " + Constants.ErrorString, "OK");
                    });
                }
            }
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
                return;

            var activitySet = JsonConvert.DeserializeObject<ActivitySet>(e.Data);
            watermark = activitySet?.Watermark;

            var activities = from x in activitySet.Activities
                             where x.From.Id == Constants.BotId || x.From.Name == Constants.BotId
                             select x;

            bool activityFound = false;
            foreach (Activity activity in activities)
            {
                Debug.WriteLine("Message Received: " + DateTime.Now.ToLongTimeString());

                activityFound = true;
                IsMessageSent = false;

                viewModel.GetBotResponse(activity);
            }

            if (IsMessageSent && !activityFound)
            {
                IsMessageSent = false;
                //MessagingCenter.Send(App.Current.MainPage, "AddLoadingMessage");
            }
        }

        private void WebSocket_OnError(object sender, ErrorEventArgs e)
        {
            Debug.WriteLine("WebSocket error: " + e.Message);
        }

        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            Debug.WriteLine("WebSocket closed...");

            //var socket = sender as WebSocket;
            //socket.OnMessage -= WebSocket_OnMessage;
            //socket.OnClose -= WebSocket_OnClose;
            //socket.OnError -= WebSocket_OnError;

            //if (socket.Equals(webSocket))
            //    webSocket = null;
        }

        public void Dispose()
        {
            if (client == null)
                return;

            var userActivity = new Activity
            {
                From = new ChannelAccount(Constants.Email, Constants.Username),
                Type = ActivityTypes.EndOfConversation
            };
            client.Conversations.PostActivityAsync(conversation.ConversationId, userActivity);
            client.Dispose();
            client = null;
            conversation = null;

            webSocket.OnMessage -= WebSocket_OnMessage;
            webSocket.OnClose -= WebSocket_OnClose;
            webSocket.OnError -= WebSocket_OnError;
            if (webSocket.IsAlive)
                webSocket.CloseAsync();
            webSocket = null;
            viewModel = null;
        }
    }
}
