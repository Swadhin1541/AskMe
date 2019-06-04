using AdaptiveCards;
using AdaptiveCards.Rendering.Xamarin;
using AskMe.Interfaces;
using AskMe.Model;
using AskMe.Model.Email;
using AskMe.Services;
using AskMe.Utility;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AskMe.ViewModel
{
    public class MainPageViewModel : BaseModel
    {
        BingSpeechService bingSpeechService;
        BotDirectLineService botService;
        SpeechSynthesize SpeechSynthesizer;
        SpeechSynthesize.InputOptions speechInputOptions;

        ListView listView;

        bool isNewQueryStarted = false;
        bool isRecordingCancelled = false;

        List<string> newUserGreetings = new List<string>()
        {
            "Hi ", "Hello, ", "Hello there, ", "Hi there, "
        };

        List<string> existingUserGreetings = new List<string>()
        {
            "Welcome, ", "Good to see you, ", "Hello there, ", "Hey "
        };

        private string headerText;
        public string HeaderText
        {
            get { return headerText; }
            set { SetProperty(ref headerText, value); }
        }

        private string placeHolderText;
        public string PlaceHolderText
        {
            get { return placeHolderText; }
            set { SetProperty(ref placeHolderText, value); }
        }

        private string entryText;
        public string EntryText
        {
            get { return entryText; }
            set
            {
                UpdateSendButtonVisibility(value);
                SetProperty(ref entryText, value);
            }
        }

        private bool isRecording;
        public bool IsRecording
        {
            get { return isRecording; }
            set { SetProperty(ref isRecording, value); }
        }

        private bool canSend;
        public bool CanSend
        {
            get { return canSend; }
            set { SetProperty(ref canSend, value); }
        }

        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }

        private ObservableCollection<CardAction> heroCardButtons;
        public ObservableCollection<CardAction> HeroCardButtons
        {
            get { return heroCardButtons; }
            set { SetProperty(ref heroCardButtons, value); }
        }

        public Command HeroCardTapCommand { get; set; }
        public Command SendRecordCommand { get; set; }
        public Command EntryReturnCommand { get; set; }

        public MainPageViewModel()
        {
            DependencyService.Get<ISpeechToTextService>().Initialize();
            InitializeBotService();
            InitializeDefaultMessages();
            InitializeTextToSpeech();

            HeroCardButtons = new ObservableCollection<CardAction>();

            HeroCardTapCommand = new Command(ExecuteHeroCardTap);
            SendRecordCommand = new Command(ExecuteRecordOrSend);
            EntryReturnCommand = new Command(ExecuteEntryReturn);

            MessagingCenter.Subscribe<ISpeechToTextService, string>(this, "FinalResponse", ReadFinalResponse);
            MessagingCenter.Subscribe<ISpeechToTextService, string>(this, "PartialResponse", ReadPartialResponse);
            MessagingCenter.Subscribe<RenderedAdaptiveCard, AdaptiveActionEventArgs>(this, "AdaptiveCardAction", HandleAdaptiveCardAction);

            //MessagingCenter.Subscribe<BotDirecLineService, Activity>(this, "BotResponse", GetBotResponse);
            //MessagingCenter.Subscribe<Page>(this, "AddLoadingMessage", AddLoadingMessage);
            //MessagingCenter.Subscribe<Page>(this, "RemoveLoadingMessage", RemoveLoadingMessage);

            //LanguageUtility.GenerateLanguageModel();
        }

        private void InitializeTextToSpeech()
        {
            SpeechSynthesizer = new SpeechSynthesize();
            speechInputOptions = new SpeechSynthesize.InputOptions()
            {
                RequestUri = new Uri(Constants.CRISTTSEndPoint),
                VoiceType = Gender.Female,
                Locale = Constants.DefaultLocale,
                VoiceName = Constants.VoiceName,
                OutputFormat = AudioOutputFormat.Riff24Khz16BitMonoPcm,
            };

            SpeechSynthesizer.OnAudioAvailable += (sender, e) =>
            {
                if (!IsRecording)
                    DependencyService.Get<ITextToSpeechService>().Speak(e.EventData);
            };

            //speechInputOptions.Text = "first";
            //SpeechSynthesizer.SpeakAsync(CancellationToken.None, speechInputOptions);

            Device.BeginInvokeOnMainThread(() => DependencyService.Get<ITextToSpeechService>().Initialize());
        }

        private async void InitializeBotService()
        {
            botService = new BotDirectLineService(this);
            await botService.StartBotConversation();
        }

        private void InitializeBingSpeechService()
        {
            bingSpeechService = new BingSpeechService(new AuthenticationService(Constants.BingSpeechAuthenticationURL, Constants.BingSpeechApiKey), Device.RuntimePlatform);
        }

        private void ExecuteEntryReturn(object obj)
        {
            if (listView == null)
                listView = obj as ListView;
            if (!string.IsNullOrWhiteSpace(EntryText))
                AddMessageAndSendResponse(EntryText, true);
            CanSend = false;
        }

        private async void ExecuteRecordOrSend(object obj)
        {
            listView = obj as ListView;
            if(CanSend || IsRecording)
            {
                if (isRecording)
                {
                    isRecordingCancelled = true;
                    DependencyService.Get<ISpeechToTextService>().StopRecording();
                    if (isNewQueryStarted && Messages.Count > 0)
                        Messages.RemoveAt(Messages.Count - 1);
                }
                else
                {
                    AddMessageAndSendResponse(EntryText, true);
                }
                isNewQueryStarted = false;
                PlaceHolderText = string.Empty;
                IsRecording = false;
                CanSend = false;
            }
            else
            {
                DependencyService.Get<ITextToSpeechService>().StopSpeaking();
                await DependencyService.Get<ISpeechToTextService>().StartRecording();
                isRecordingCancelled = false;
                IsRecording = true;
                PlaceHolderText = "AskMe is listening...";
            }
        }

        private void UpdateSendButtonVisibility(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (!CanSend)
                    CanSend = true;
            }
            else if (CanSend)
                CanSend = false;
        }

        private void AddLoadingMessage(Page page)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!messages.Any(m => m.MessageType == MessageType.Loading))
                    Messages.Add(new Message() { MessageType = MessageType.Loading });
            });
        }

        private void RemoveLoadingMessage(Page page)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var message = messages.FirstOrDefault(m => m.MessageType == MessageType.Loading);
                if (message != null)
                    Messages.Remove(message);
            });
        }

        private async void AddMessageAndSendResponse(string queryText, bool sendResponse = true, bool isNew = true)
        {
            if (string.IsNullOrWhiteSpace(queryText))
                return;

            DependencyService.Get<ITextToSpeechService>().StopSpeaking();
            AddMessage(listView, queryText, MessageType.OutgoingText, isNew);
            if (sendResponse)
            {
                await botService.SendMessageToBot(queryText);

                if (botService.IsMessageSent)
                    AddLoadingMessage(App.Current.MainPage);
            }
        }

        public void GetBotResponse(Activity activity)
        {
            Task.Factory.StartNew(() =>
            {
                if (activity != null)
                {
                    if (activity.ChannelData != null)
                    {
                        var emailChannelData = activity.GetChannelData<Model.Email.EmailChannelData>();
                        if (emailChannelData.Attachment.Payload.EMailData != null)
                        {
                            Device.BeginInvokeOnMainThread(() => SendMail(emailChannelData));
                        }
                        else
                            AddMessage(listView, activity.Text, MessageType.IncomingCard, true, activity);
                    }
                    else if (activity.Attachments?.FirstOrDefault(m => m.ContentType == Constants.AdaptiveCardType) is Attachment cardAttachment)
                    {
                        try
                        {
                            var jObject = (JObject)cardAttachment.Content;
                            var card = jObject.ToObject<AdaptiveCard>();
                            AddMessage(listView, activity.Text, MessageType.IncomingCard, true, activity, card);
                        }
                        catch { }
                    }
                    else if (!string.IsNullOrEmpty(activity.Text))
                    {
                        var text = Regex.Replace(activity.Text, "\\:.*?\\:", "");
                        AddMessage(listView, text, MessageType.IncomingText, true, activity);

                        RenderHeroCard(activity);
                    }
                    else
                        RenderHeroCard(activity);
                }
            });
        }

        private void RenderHeroCard(Activity activity)
        {
            if (activity.Attachments?.FirstOrDefault(m => m.ContentType == Constants.HeroCardType) is Attachment cardAttachment)
            {
                try
                {
                    var jObject = (JObject)cardAttachment.Content;
                    var card = jObject.ToObject<HeroCard>();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        HeroCardButtons = new ObservableCollection<CardAction>(card.Buttons);
                    });
                }
                catch { }
            }
        }

        private void ExecuteHeroCardTap(object obj)
        {
            if(obj is CardAction action)
            {
                AddMessageAndSendResponse(action.Value.ToString());
            }
        }

        private async void SendMail(EmailChannelData emailChannelData)
        {
            var emailData = emailChannelData.Attachment.Payload.EMailData;
            var chartData = emailData.ChartData;
            if (chartData != null)
            {
                var message = Messages.LastOrDefault(m => m.Activity != null && m.Activity.ChannelData != null);
                if (message != null && message.ViewToImageService != null)
                {
                    var imageData = message.ViewToImageService.GetImageData();
                    var emailInfo = new EmailInfo();
                    emailInfo.From = Constants.Email;
                    emailInfo.To = emailData.Receipients;
                    emailInfo.Subject = emailData.Subject;
                    emailInfo.Htmlbody = emailData.Notes;
                    if (!string.IsNullOrEmpty(imageData))
                    {
                        emailInfo.Htmlbody += " <img src = 'cid:chart' />";
                        emailInfo.Attachmentarray = new List<Attachmentarray>()
                        { 
                            new Attachmentarray(){Content = imageData, Encoding = "base64", Filename = "Chart.jpg", Cid="chart" }
                        };
                    }

                    using (var httpClient = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(emailInfo);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var uri = new Uri(Constants.EmailAPI, UriKind.RelativeOrAbsolute);
                        var response = await httpClient.PostAsync(uri, content);

                        string status = "Email sent...";
                        if (!response.IsSuccessStatusCode)
                            status = "Problem in sending email. Please try again...";

                        AddMessage(listView, status, MessageType.IncomingText);
                    }
                }
            }
        }

        private void ReadPartialResponse(ISpeechToTextService service, string response)
        {
            if (isRecordingCancelled)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                AddMessageAndSendResponse(response, false, !isNewQueryStarted);
                isNewQueryStarted = true;
            });
        }

        private void ReadFinalResponse(ISpeechToTextService service, string response)
        {
            if (isRecordingCancelled)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                //response = LanguageUtility.GetProperLanguageContent(response);
                AddMessageAndSendResponse(response, true, !isNewQueryStarted);
                isNewQueryStarted = false;

                PlaceHolderText = string.Empty;
                IsRecording = false;
                CanSend = false;
            });
        }

        private void HandleAdaptiveCardAction(RenderedAdaptiveCard renderdCard, AdaptiveActionEventArgs args)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    if (args.Action is AdaptiveOpenUrlAction openUrlAction)
                    {
                        Process.Start(openUrlAction.Url.AbsoluteUri);
                    }
                    else if (args.Action is AdaptiveSubmitAction submitAction)
                    {
                        var inputs = renderdCard.UserInputs.AsJson();
                        var text = submitAction.Data is String data ? data : null;
                        if (inputs.Count > 0)
                        {
                            //Merge the Action.Submit Data property with the inputs
                            inputs.Merge(submitAction.Data);
                        }
                        object value = null;
                        if (inputs.Count > 0)
                            value = inputs;
                        else if (text == null && submitAction.Data != null)
                            value = submitAction.Data;
                         
                        await botService.SendMessageToBot(text, value);
                        if (!string.IsNullOrWhiteSpace(text))
                            AddMessage(listView, text, MessageType.OutgoingText);
                    }
                    else
                    {
                        var text = args.Action.Title;
                        await botService.SendMessageToBot(text);
                        if (!string.IsNullOrWhiteSpace(text))
                            AddMessage(listView, text, MessageType.OutgoingText);
                    }
                }
                catch (Exception)
                {
                }
            });
        }

        private void AddMessage(ListView listview, string text, MessageType type, bool isNew = true, Activity activity = null, AdaptiveCard card = null)
        {
            Message message = null;
            if (isNew)
            {
                RemoveLoadingMessage(App.Current.MainPage);
                message = new Message()
                {
                    Activity = activity,
                    DisplayText = text,
                    MessageType = type,
                    AdaptiveCard = card
                };
                 
                if (!string.IsNullOrEmpty(text))
                    message.FormattedText = text.GetFormattedString(17);

                if (type == MessageType.IncomingText || type == MessageType.IncomingCard)
                {
                    Task.Factory.StartNew(() => Speak(activity, message, card, text));
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    HeroCardButtons.Clear();
                    MessagingCenter.Send(App.Current.MainPage, "HidePreviousButtons");
                    Messages.Add(message);
                });

                EntryText = string.Empty;
            }
            else
            {
                message = Messages[Messages.Count - 1];
                message.DisplayText = text;
                Device.BeginInvokeOnMainThread(() =>
                {
                    var hash = new Hashtable();
                    //hash.Add("Index", Messages.IndexOf(message));
                    MessagingCenter.Send(App.Current.MainPage, "ReloadAndScrollListView", hash);
                });
            }
        }

        private void Speak(Activity activity, Message message, AdaptiveCard card, string text)
        {
            var speak = text;
            if (!string.IsNullOrWhiteSpace(activity?.Speak))
                speak = activity.Speak.Replace("<s>", "").Replace("</s>", "");
            else if (!string.IsNullOrWhiteSpace(card?.Speak))
                speak = card.Speak.Replace("<s>", "").Replace("</s>", "");

            if (string.IsNullOrWhiteSpace(speak))
                return;

            speak = speak.Replace("*", "");

            message.SpeechText = speak;
            message.IsSpeaking = true;

            //DependencyService.Get<ITextToSpeechService>().Speak(speak, message);

            speechInputOptions.Text = speak;
            SpeechSynthesizer.SpeakAsync(CancellationToken.None, speechInputOptions);
        }

        private void InitializeDefaultMessages()
        {
            Messages = new ObservableCollection<Message>();
            if (App.IsNewUser)
            {
                HeaderText = newUserGreetings[new Random().Next(0, 4)];// + Constants.Username + ".";
            }
            else
            {
                HeaderText = existingUserGreetings[new Random().Next(0, 3)];// + Constants.Username + ".";
            }
            var s = "How can I help you today?";
            Messages.Add(new Message()
            {
                FormattedText = s.GetFormattedString(17),
                MessageType = MessageType.IncomingText,
            });
        }
    }
}
