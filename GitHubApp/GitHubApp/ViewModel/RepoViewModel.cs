using GitHubApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GitHubApp.ViewModel
{
    class RepoViewModel
    {
        private Item _repo;

        public RepoViewModel(Item repo)
        {
            _repo = repo;

            this.SelectCommand = new Command(OnSelect);
        }

        public string Name => _repo.name;
        public string OwnerUserName => _repo.owner.login;
        public string Description => _repo.description;

        public ICommand SelectCommand { get; private set; }

        private void OnSelect()
        {
            try
            {
                var uri = new Uri(_repo.html_url);
                Device.OpenUri(uri);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(this, "Error", ex.Message);
            }
        }
    }
}
