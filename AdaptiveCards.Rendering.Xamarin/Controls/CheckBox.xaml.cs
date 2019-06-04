using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdaptiveCards.Rendering.Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckBox : ContentView
	{
        private bool disableClicks = false;
		public CheckBox ()
		{
            InitializeComponent();

            NotSelectedImage.Source = ImageSource.FromResource("AdaptiveCards.Rendering.Xamarin.Resources.NotSelected.png");
            SelectedImage.Source = ImageSource.FromResource("AdaptiveCards.Rendering.Xamarin.Resources.Selected.png");

            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (!disableClicks)
                        IsChecked = !IsChecked;
                })
            };
            GestureRecognizers.Add(tapGesture);

            MessagingCenter.Subscribe<Page>(this, "HidePreviousButtons", (sender) =>
            {
                disableClicks = true;
                MessagingCenter.Unsubscribe<Page>(this, "HidePreviousButtons");
            });
        }

        public Action<CheckBox> CheckUnCheckAction { get; set; }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create("IsChecked", typeof(bool), typeof(CheckBox), false, BindingMode.Default, null, OnIsCheckedPropertyChanged);

        private static void OnIsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ischecked = (bool)newValue;
            var checkBox = bindable as CheckBox;
            checkBox.SelectedImage.IsVisible = ischecked;
            checkBox.NotSelectedImage.IsVisible = !ischecked;

            if (!AdaptiveChoiceSetRenderer.CheckBoxUpdating)
                checkBox.CheckUnCheckAction(checkBox);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CheckBox), string.Empty, BindingMode.Default, null, OnTextPropertyChanged);

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var text = newValue?.ToString();
            var checkBox = bindable as CheckBox;
            checkBox.TextLabel.Text = text;
        }
    }
}