using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Tests.NewsReader.UI.ViewModels
{
    /// <summary>
    /// Represent viewmodel of collection of news item from required news feed
    /// </summary>
    public class NewsFeedViewModel : BaseViewModel
    {
        #region Local vars
        private string title;
        private string imageUrl;
        private string rssAddress;
        private bool clientIsBusy = false;

        private NewsItemViewModel selectedItem = null;
        private ObservableCollection<NewsItemViewModel> items = new ObservableCollection<NewsItemViewModel>();
        #endregion

        #region Public properties
        /// <summary>
        /// RSS-feed title
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(() => Title);
            }
        }

        /// <summary>
        /// RSS-feed image Url
        /// </summary>
        public string ImageUrl
        {
            get => imageUrl;
            set
            {
                imageUrl = value;
                OnPropertyChanged(() => ImageUrl);
            }
        }

        /// <summary>
        /// RSS-feed Url
        /// </summary>
        public string RssAddress
        {
            get => rssAddress;
            set
            {
                rssAddress = value;
                OnPropertyChanged(() => RssAddress);
            }
        }

        /// <summary>
        /// Variable to store items of list
        /// </summary>
        public ObservableCollection<NewsItemViewModel> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged(() => Items);
            }
        }

        /// <summary>
        /// Represent currently selected item
        /// </summary>
        public NewsItemViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(() => SelectedItem);
            }
        }
        #endregion

        /// <summary>
        /// Default ctor
        /// </summary>
        public NewsFeedViewModel()
        {
            RefreshList(Properties.Settings.Default.RssAddressUrl);
        }

        #region Command implementation
        private ICommand openFeedCommand;
        private Common.CommandBase refreshCommand;

        #region Command interfaces implementation
        /// <summary>
        /// Refresh list content command interface
        /// </summary>
        public Common.CommandBase RefreshCommand
        {
            get
            {
                refreshCommand = refreshCommand ?? new Common.CommandBase(i => RefreshList(rssAddress), CanRefreshCommand );
                return refreshCommand;
            }
        }

        public bool CanRefreshCommand(object _) { return !clientIsBusy; }
            
        /// <summary>
        /// Open current feed in browser window command interface
        /// </summary>
        public ICommand OpenFeedCommand
        {
            get
            {
                openFeedCommand = openFeedCommand ?? new Common.CommandBase(i => OpenFeed(), null);
                return openFeedCommand;
            }
        }
        #endregion

        #region Command implementation
        /// <summary>
        /// Refresh list with actual data
        /// </summary>
        protected void RefreshList(string rssAddress)
        {
            Items.Clear();
            RssAddress = rssAddress;
            Client.NewsReaderClient client = Client.NewsReaderClient.GetInstance();

            client.LoadAsyncStarted += (sender, e) => clientIsBusy = true;

            client.LoadAsyncCompleted += (sender, e) =>
            {
                Title = e.Title;
                ImageUrl = e.ImageUrl;
                foreach (ServiceModel.NewsItem newsItem in e.Items)
                    Items.Add((NewsItemViewModel)newsItem);
                clientIsBusy = false;
            };

            client.LoadAsync(rssAddress);
        }

        /// <summary>
        /// Open current RSS-feed in default browser window
        /// </summary>
        private void OpenFeed()
        {
            System.Diagnostics.Process.Start(rssAddress);
        }
        #endregion
        #endregion
    }
}
