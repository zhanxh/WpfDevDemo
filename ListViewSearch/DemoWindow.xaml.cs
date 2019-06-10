using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ListViewSearch
{
    /// <summary>
    /// Interaction logic for DemoWindow.xaml
    /// </summary>
    public partial class DemoWindow : Window
    {
        Person[] ps;
        CollectionViewSource cvs;
        public DemoWindow()
        {

            InitializeComponent();
            ps = GetSourceData();

            cvs = new CollectionViewSource();
            cvs.Source = ps;
            cvs.Filter += OnViewFilter;
            Binding bd = new Binding();
            bd.Source = cvs;

            listView.SetBinding(ItemCollection, bd);
        }
        

        

        private Person[]  GetSourceData(){
            Person[] ps = new Person[6000];
            for (int i = 0; i < 1000; i++)
            {
                ps[i].Name = "IF" + i;
                ps[i].Age = i;
            }
            for (int i = 0; i < 1000; i++)
            {
                ps[i + 1000].Name = "IC" + i;
                ps[i + 1000].Age = i;
            }
            for (int i = 0; i < 1000; i++)
            {
                ps[i + 2000].Name = "ID" + i;
                ps[i + 2000].Age = i;
            }
            for (int i = 0; i < 1000; i++)
            {
                ps[i + 3000].Name = "IE" + i;
                ps[i + 3000].Age = i;
            }
            for (int i = 0; i < 1000; i++)
            {
                ps[i + 4000].Name = "IH" + i;
                ps[i + 4000].Age = i;
            }
            for (int i = 0; i < 1000; i++)
            {
                ps[i + 5000].Name = "IG" + i;
                ps[i + 5000].Age = i;
            }
            return ps;
        }

        #region Event Handlers

        private void OnViewFilter(object sender, FilterEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                e.Accepted = (e.Item as Person).Name.Contains(txtFilter.Text);
            }
        }

        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            (FindResource("ViewSource") as CollectionViewSource).View.Refresh();
        }

        #endregion
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age{ get; set; }
    }
}
