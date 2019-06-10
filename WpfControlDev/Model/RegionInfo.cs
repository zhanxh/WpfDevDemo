using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlDev.Model
{
    /// <summary>
    /// 地区信息
    /// </summary>
    public class RegionInfo : INotifyPropertyChanged   
    {
        private string _province;//省  
        private string _city;//市  
        private string _area;//区  

        public event PropertyChangedEventHandler PropertyChanged;

        public string Province
        {
            get { return _province; }
            set
            {
                _province = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Province"));
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                PropertyChanged(this, new PropertyChangedEventArgs("City"));
            }
        }
        public string Area
        {
            get { return _area; }
            set
            {
                _area = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Area"));
            }
        }
        public RegionInfo(string province, string city, string area)//构造函数  
        {
            _province = province;
            _city = city;
            _area = area;
        }
    }

    public class RegionMap
    {
        public string DataKey { get; set; }
        public string DataValue { get; set; }
    }
}
