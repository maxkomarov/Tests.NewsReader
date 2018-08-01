using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Tests.NewsReader.UI.ViewModels
{
    /// <summary>
    /// Superclass for all list-based viewmodels
    /// </summary>
    public abstract class BaseListViewModel<T, K> : BaseViewModel where T : class
    {
        #region Singleton implementation
        /// <summary>
        /// Store only class instance
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(() => CreateInstanceOfT());

        /// <summary>
        /// Create instance of type
        /// </summary>
        /// <returns></returns>
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }

        /// <summary>
        /// Return singleton instance
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return instance.Value;
        }

        #endregion

        #region Local vars
        private T selectedItem = null;
        private ObservableCollection<K> items = new ObservableCollection<K>();
        private ICommand showDialogCommand;
        private ICommand refreshCommand;
        #endregion

        #region Public properties
        /// <summary>
        /// Variable to store items of list
        /// </summary>
        public ObservableCollection<K> Items
        {
            get
            {
                RefreshList();
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
        public T SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(() => SelectedItem);
            }
        }
        #endregion

        #region Command interfaces implementation

        /// <summary>
        /// Show detail form for selected list item
        /// </summary>
        public ICommand ShowDialogCommand
        {
            get
            {
                showDialogCommand = showDialogCommand ?? new Common.CommandBase(i => ShowDialog(), null);
                return showDialogCommand;
            }
        }

        /// <summary>
        /// Refresh list content
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                refreshCommand = refreshCommand ?? new Common.CommandBase(i => RefreshList(), null);
                return refreshCommand;
            }
        }
        #endregion

        /// <summary>
        /// Abstract method for implementation in derived classes to showing appropriate child form for list item
        /// </summary>
        /// <param name="viewModel"></param>
        protected abstract void ShowDialog(object viewModel = null);

        /// <summary>
        /// Abstract method for implementation in derived classes to refresh list content
        /// </summary>
        protected abstract void RefreshList();
    }
}
