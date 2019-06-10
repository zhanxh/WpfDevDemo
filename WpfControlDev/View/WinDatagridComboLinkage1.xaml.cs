using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfControlDev.Model;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinDatagridComboLinkage.xaml 的交互逻辑
    /// </summary>
    public partial class WinDatagridComboLinkage1 : Window
    {
        ObservableCollection<RegionInfo> regionInfoList = new ObservableCollection<RegionInfo>();//DataGrid的数据源  
        ObservableCollection<RegionInfo> regionInfoSelectList = new ObservableCollection<RegionInfo>();//用于DataGrid的模板列加载时提供选项 

        ObservableCollection<RegionMap> ProvinceList = new ObservableCollection<RegionMap>();
        ObservableCollection<RegionMap> CityList = new ObservableCollection<RegionMap>();
        ObservableCollection<RegionMap> AreaList = new ObservableCollection<RegionMap>();

        public WinDatagridComboLinkage1()
        {
            InitializeComponent();
            //
            regionInfoList.Add(new RegionInfo("广东省", "深圳市", "罗湖区"));
            regionInfoList.Add(new RegionInfo("广东省", "深圳市", "南山区"));
            regionInfoList.Add(new RegionInfo("", "", ""));

            //三级联动数据项  
            regionInfoSelectList.Add(new RegionInfo("广东省", "深圳市", "罗湖区"));
            regionInfoSelectList.Add(new RegionInfo("广东省", "深圳市", "南山区"));
            regionInfoSelectList.Add(new RegionInfo("广东省", "潮州市", "湘桥区"));
            regionInfoSelectList.Add(new RegionInfo("广东省", "潮州市", "枫溪区"));
            regionInfoSelectList.Add(new RegionInfo("湖北省", "武汉市", "江夏区"));
            regionInfoSelectList.Add(new RegionInfo("湖北省", "武汉市", "武昌区"));

            ProvinceList = new ObservableCollection<RegionMap>(regionInfoSelectList.GroupBy(p => p.Province).Select(p => new RegionMap() { DataKey = p.Key, DataValue = p.Key }));
            CityList = new ObservableCollection<RegionMap>(regionInfoSelectList.GroupBy(p => new { p.Province, p.City }).Select(p => new RegionMap() { DataKey = p.Key.Province, DataValue = p.Key.City }));
            AreaList = new ObservableCollection<RegionMap>(regionInfoSelectList.GroupBy(p => new { p.Province, p.City, p.Area }).Select(p => new RegionMap() { DataKey = p.Key.City, DataValue = p.Key.Area }));

            dataGrid.ItemsSource = regionInfoList;//绑定数据源

            
        }

        private void ProvinceDropDownClosed(object sender, System.EventArgs e)
        {

        }

        private void ProvinceLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox curComboBox = sender as ComboBox;
            curComboBox.ItemsSource = ProvinceList;
            curComboBox.SelectedItem = ProvinceList.FirstOrDefault(p => p.DataValue == curComboBox.Text);
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉

            //ComboBox curComboBox = sender as ComboBox;
            ////为下拉控件绑定数据源，并选择原选项为默认选项  
            //string text = curComboBox.Text;
            ////去除重复项查找，跟数据库连接时可以让数据库来实现  
            //var query = regionInfoSelectList.GroupBy(p => p.Province).Select(p => new { Province = p.FirstOrDefault().Province });
            //int itemcount = 0;
            //curComboBox.SelectedIndex = itemcount;
            //foreach (var item in query.ToList())
            //{
            //    if (item.Province == text)
            //    {
            //        curComboBox.SelectedIndex = itemcount;
            //        break;
            //    }
            //    itemcount++;
            //}
            //curComboBox.ItemsSource = query;
            //curComboBox.IsDropDownOpen = true;//获得焦点后下拉  
        }

        private void CityDropDownClosed(object sender, System.EventArgs e)
        {

        }

        private void CityLoaded(object sender, RoutedEventArgs e)
        {
            string province = (dataGrid.SelectedItem as RegionInfo).Province;
            var citys = new ObservableCollection<RegionMap>(CityList.Where(p => p.DataKey == province).ToArray());
            ComboBox curComboBox = sender as ComboBox;
            curComboBox.ItemsSource = citys;
            curComboBox.SelectedItem = citys.FirstOrDefault(p => p.DataValue == curComboBox.Text);
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉

            ////获得当前选中项的省份信息  
            //string province = (dataGrid.SelectedItem as RegionInfo).Province;
            ////查找选中省份下的市作为数据源  
            //var query = (from l in regionInfoSelectList
            //             where (l.Province == province)
            //             group l by l.City into grouped
            //             select new { City = grouped.Key });
            //ComboBox curComboBox = sender as ComboBox;
            ////为下拉控件绑定数据源，并选择原选项为默认选项    
            //string text = curComboBox.Text;
            ////去除重复项查找，跟数据库连接时可以让数据库来实现  
            //int itemcount = 0;
            //curComboBox.SelectedIndex = itemcount;
            //foreach (var item in query.ToList())
            //{
            //    if (item.City == text)
            //    {
            //        curComboBox.SelectedIndex = itemcount;
            //        break;
            //    }
            //    itemcount++;
            //}
            //curComboBox.ItemsSource = query;
            //curComboBox.IsDropDownOpen = true;//获得焦点后下拉  
        }

        private void AreaDropDownClosed(object sender, System.EventArgs e)
        {

        }

        private void AreaLoaded(object sender, RoutedEventArgs e)
        {
            string city = (dataGrid.SelectedItem as RegionInfo).City;
            var areas = new ObservableCollection<RegionMap>(AreaList.Where(p => p.DataKey == city).ToArray());
            ComboBox curComboBox = sender as ComboBox;
            curComboBox.ItemsSource = areas;
            curComboBox.SelectedItem = areas.FirstOrDefault(p => p.DataValue == curComboBox.Text);
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉

            //string province = (dataGrid.SelectedItem as RegionInfo).Province;
            //string city = (dataGrid.SelectedItem as RegionInfo).City;
            ////查找选中省份下的市作为数据源  
            //var query = (from l in regionInfoSelectList
            //             where (l.Province == province && l.City == city)
            //             group l by l.Area into grouped
            //             select new { Area = grouped.Key });

            //ComboBox curComboBox = sender as ComboBox;
            ////为下拉控件绑定数据源，并选择原选项为默认选项  
            //string text = curComboBox.Text;
            ////去除重复项查找，跟数据库连接时可以让数据库来实现  
            //int itemcount = 0;
            //curComboBox.SelectedIndex = itemcount;
            //foreach (var item in query.ToList())
            //{
            //    if (item.Area == text)
            //    {
            //        curComboBox.SelectedIndex = itemcount;
            //        break;
            //    }
            //    itemcount++;
            //}
            //curComboBox.ItemsSource = query;
            //curComboBox.IsDropDownOpen = true;//获得焦点后下拉  
        }
    }
}
