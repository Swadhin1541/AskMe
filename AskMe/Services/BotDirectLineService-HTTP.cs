using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Xamarin;
using AskMe.Controls;
using AskMe.Utility;
using AskMe.ViewModel;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AskMe.Services
{
    public class BotDirectLineService : IDisposable
    {
        private ChannelAccount userAccount;
        private DirectLineClient client;
        private Conversation conversation;
        private Thread readMessageThread;
        private MainPageViewModel viewModel;

        public bool IsMessageSent { get; set; } = false;

        public BotDirectLineService(MainPageViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public async Task StartBotConversation()
        {
            try
            {
                userAccount = new ChannelAccount(Constants.Email);
                var credential = new DirectLineClientCredentials(Constants.DirectLineSecret, Constants.BotEndPoint);
                client = new DirectLineClient(credential);
                conversation = await client.Conversations.StartConversationAsync();
                readMessageThread = new Thread(() => ReadBotMessagesAsync(client, conversation.ConversationId));
                readMessageThread.Start();
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "Problem occurred while connecting with Bot. " + Constants.ErrorString, "OK");
                });
            }
        }

        public async Task SendMessageToBot(string text, object value = null)
        {
            if (!string.IsNullOrWhiteSpace(text) || value != null)
            {
                if (conversation == null)
                {
                    await StartBotConversation();
                    if (conversation == null)
                        return;
                }
                else if (readMessageThread == null)
                {
                    readMessageThread = new Thread(() => ReadBotMessagesAsync(client, conversation.ConversationId));
                    readMessageThread.Start();
                }

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
                    Debug.WriteLine("Message sent: " + DateTime.Now.ToLongTimeString());
                    IsMessageSent = true;
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Error", "Problem occurred while posting query to Bot. " + Constants.ErrorString, "OK");
                    });
                }
            }
        }

        public void ReadBotMessagesAsync(DirectLineClient client, string conversationId)
        {
            string watermark = null;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                try
                {
                    var activitySet = client.Conversations.GetActivities(conversationId, watermark);
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

                    if(IsMessageSent && !activityFound)
                    {
                        IsMessageSent = false;
                        //MessagingCenter.Send(App.Current.MainPage, "AddLoadingMessage");
                    }
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if(IsMessageSent)
                        {
                            //MessagingCenter.Send(App.Current.MainPage, "RemoveLoadingMessage");
                            IsMessageSent = false;
                        }
                        //App.Current.MainPage.DisplayAlert("Error", "Problem occurred while fetching response from Bot. Trying to solve it!!!", "OK");
                    });
                    readMessageThread = null;
                    return false;
                }
                return true;
            });
        }

        public void Dispose()
        {
            if (client == null)
                return;

            var userActivity = new Activity
            {
                From = new ChannelAccount(Constants.Email),
                Type = ActivityTypes.EndOfConversation
            };
            client.Conversations.PostActivityAsync(conversation.ConversationId, userActivity);
            client.Dispose();
            client = null;
            userAccount = null;
            conversation = null;
            readMessageThread = null;
            viewModel = null;
        }
    }
}
