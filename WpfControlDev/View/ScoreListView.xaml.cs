using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlDev.Model;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// ScoreListView.xaml 的交互逻辑
    /// </summary>
    public partial class ScoreListView : Window
    {
        public ScoreListView()
        {
            InitializeComponent();
            var vm = new ScoreListViewModel();
            this.DataContext = vm;
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = ((e.Item as Student).UserName != "Marry Jane");
        }

        private void SortHelper(string propertyName)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(scoreListViewModel.StudentList);

            if (view.SortDescriptions.Count > 0 && view.SortDescriptions[0].PropertyName == propertyName && view.SortDescriptions[0].Direction == ListSortDirection.Ascending)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(propertyName, ListSortDirection.Descending));
            }
            else
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(propertyName, ListSortDirection.Ascending));
            }
        }

        private void SortName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortScore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GroupScore1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GroupScore2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveToNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveToPrevious_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
