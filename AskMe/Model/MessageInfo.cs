using AdaptiveCards;
using AskMe.Interfaces;
using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AskMe.Model
{
    public enum MessageType
    {
        IncomingText,
        IncomingCard,
        OutgoingText,
        Loading,
        IncomingTextWithoutIcon
    }

    public class Message : BaseModel
    {
        private string displayText;
        public string DisplayText
        {
            get { return displayText; }
            set { SetProperty(ref displayText, value); }
        }

        private FormattedString formattedText;
        public FormattedString FormattedText
        {
            get { return formattedText; }
            set { SetProperty(ref formattedText, value); }
        }

        private string speechText;
        public string SpeechText
        {
            get { return speechText; }
            set { SetProperty(ref speechText, value); }
        }

        private MessageType messageType;
        public MessageType MessageType
        {
            get { return messageType; }
            set { SetProperty(ref messageType, value); }
        }

        private Activity activity;
        public Activity Activity
        {
            get { return activity; }
            set { SetProperty(ref activity, value); }
        }

        private AdaptiveCard adaptiveCard;
        public AdaptiveCard AdaptiveCard
        {
            get { return adaptiveCard; }
            set { SetProperty(ref adaptiveCard, value); }
        }

        private bool isSpeaking;
        public bool IsSpeaking
        {
            get { return isSpeaking; }
            set
            {
                //Below code commented due to crash occurred randomly while adding message in 
                //MainPageViewModel.Messages collection.
                //if (!value)
                //    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Current.MainPage, "SpeakingCompleted"));

                SetProperty(ref isSpeaking, value);
            }
        }

        public IViewToImageService ViewToImageService { get; set; }
    }

    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
