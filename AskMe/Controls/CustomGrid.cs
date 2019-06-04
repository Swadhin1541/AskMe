using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AskMe.Controls
{
    public class CustomGrid : Grid
    {
        public CustomGrid()
        {
            Padding = new Thickness(0);
            Margin = new Thickness(0);
            RowSpacing = 0;
            ColumnSpacing = 0;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }

        /// <summary>
        /// Command which executes on click.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomGrid), null);

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomGrid), null);

        //public ICommand LongPressCommand
        //{
        //    get { return (ICommand)GetValue(LongPressCommandProperty); }
        //    set { SetValue(LongPressCommandProperty, value); }
        //}

        //public static readonly BindableProperty LongPressCommandProperty =
        //    BindableProperty.Create("LongPressCommand", typeof(ICommand), typeof(CustomGrid), null);

        //public object LongPressCommandParameter
        //{
        //    get { return GetValue(LongPressCommandParameterProperty); }
        //    set { SetValue(LongPressCommandParameterProperty, value); }
        //}

        //public static readonly BindableProperty LongPressCommandParameterProperty =
        //    BindableProperty.Create("LongPressCommandParameter", typeof(object), typeof(CustomGrid), null);

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Animate the tile when tap.
                    AnchorX = 0.5;
                    AnchorY = 0.5;
                    await this.ScaleTo(0.95, 50, Easing.Linear);
                    await Task.Delay(100);
                    await this.ScaleTo(1, 50, Easing.Linear);

                    //Below code execute the actual tap command
                    Command?.Execute(CommandParameter);
                });
            }
        }
    }
}
