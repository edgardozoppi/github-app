using GitHubApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitHubApp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            MessagingCenter.Subscribe<MainViewModel, string>(this, "Error", OnError);
            this.BindingContext = new MainViewModel();
        }

        private void OnError(MainViewModel vm, string message)
        {
            DisplayAlert("Error", message, "OK");
        }
    }
}
