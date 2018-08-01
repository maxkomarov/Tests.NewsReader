using System;
using System.Windows.Input;

namespace Tests.NewsReader.UI.ViewModels
{
    public class NewsItemViewModel : BaseViewModel
    {
        #region Local variables
        private string title;
        private string summary;
        private string sourceUrl;
        private DateTime publishDate;
        #endregion

        #region Properties
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(() => Title);
            }
        }

        public string Summary
        {
            get => summary;
            set
            {
                summary = value;
                OnPropertyChanged(() => Summary);
            }
        }

        public string SourceUrl
        {
            get => sourceUrl;
            set
            {
                sourceUrl = value;
                OnPropertyChanged(() => SourceUrl);
            }
        }

        public DateTime PublishDate
        {
            get => publishDate;
            set
            {
                publishDate = value;
                OnPropertyChanged(() => PublishDate);
            }
        }
        #endregion  

        /// <summary>
        /// Default ctor
        /// </summary>
        public NewsItemViewModel(){}

        /// <summary>
        /// Converter for explicit convertion from ServiceModel.NewsItem type to NewsItemVewModel type
        /// </summary>
        /// <param name="syndicationItem">SyndicationItem object</param>
        public static explicit operator NewsItemViewModel(ServiceModel.NewsItem newsItem)
        {
            NewsItemViewModel viewModel = new NewsItemViewModel();
            viewModel.Title = newsItem.Title;
            viewModel.Summary = newsItem.Summary;
            viewModel.PublishDate = newsItem.PublishDate;
            viewModel.sourceUrl = newsItem.SourceUrl;
            return viewModel;
        }

        #region Command implementation
        private ICommand openCommand;

        /// <summary>
        /// Open command interface
        /// </summary>
        public ICommand OpenCommand
        {
            get
            {
                openCommand = openCommand ?? new Common.CommandBase(i => Open(), null);
                return openCommand;
            }
        }

        /// <summary>
        /// Open current news in default browser window
        /// </summary>
        private void Open()
        {
            System.Diagnostics.Process.Start(sourceUrl);
        }
        #endregion
    }
}
