using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestFilterUserControl.Models;

namespace TestFilterUserControl.ViewModels
{
    public class DataFilterViewModel : INotifyPropertyChanged
    {
        private string _filterText;
        private ICollectionView _itemsView;//is used to work with data

        public ObservableCollection<Item> Items { get; set; }//changeable collection

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));//create a double binding and notify of the change for filtering 
                _itemsView.Refresh();//update collection for filtering results
            }
        }

        public ICollectionView ItemsView
        {
            get => _itemsView;
        }

        public DataFilterViewModel()
        {
            Items = new ObservableCollection<Item>
            {
                new Item { Name = "Apple" },
                new Item { Name = "Banana" },
                new Item { Name = "Orange" },
                new Item { Name = "Mango" },
                new Item { Name = "Apple Orange" }
            };

            _itemsView = CollectionViewSource.GetDefaultView(Items);//creating a binding to get a view with which filtering can be processed 
            _itemsView.Filter = FilterItems;//filter condition binding
        }

        private bool FilterItems(object obj)
        {
            if (string.IsNullOrEmpty(FilterText)) return true;

            return obj is Item item && item.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
        }

        //notifies about the change of data on the sent binding parameter
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}