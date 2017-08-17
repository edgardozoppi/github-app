using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GitHubApp.View
{
    public class CustomCell : ViewCell
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomCell), null, propertyChanged: OnCommandPropertyChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomCell), null, propertyChanged: OnCommandParameterPropertyChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => (ICommand)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private static void OnCommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Stuff to handle changes, not really required
        }

        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // More stuff to handle changes
        }

        private static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // More stuff to handle changes
        }
        
        protected override void OnTapped()
        {
            if (this.Command != null)
            {
                this.Command.Execute(this.CommandParameter);
            }
        }
    }
}
