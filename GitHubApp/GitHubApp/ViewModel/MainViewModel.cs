using GitHubApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GitHubApp.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        private bool _searching;
        private readonly ObservableCollection<RepoViewModel> _repositories;

        public MainViewModel()
        {
            _repositories = new ObservableCollection<RepoViewModel>();

            this.SearchCommand = new Command(OnSearchAsync, OnCanSearch);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SearchCommand { get; private set; }

        public ObservableCollection<RepoViewModel> Repositories => _repositories;

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                this.SearchCommand.ChangeCanExecute();
            }
        }

        private bool Searching
        {
            get => _searching;
            set
            {
                if (_searching != value)
                {
                    _searching = value;
                    this.SearchCommand.ChangeCanExecute();
                }
            }
        }

        private bool OnCanSearch()
        {
            var result = !this.Searching && !string.IsNullOrWhiteSpace(this.SearchQuery);
            return result;
        }

        public async void OnSearchAsync()
        {
            try
            {
                this.Searching = true;
                _repositories.Clear();

                var query = EscapeCharacters(this.SearchQuery);
                query = $"https://api.github.com/search/repositories?q={query}+in:name&sort=stars&order=desc";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "GitHubApp");
                    var json = await client.GetStringAsync(query);
                    var result = JsonConvert.DeserializeObject<QueryResult>(json);

                    foreach (var repo in result.items)
                    {
                        var vm = new RepoViewModel(repo);
                        _repositories.Add(vm);
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(this, "Error", ex.Message);
            }
            finally
            {
                this.Searching = false;
            }
        }

        private string EscapeCharacters(string query)
        {
            return query.Replace(" ", "+");
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
