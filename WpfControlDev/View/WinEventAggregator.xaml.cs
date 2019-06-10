using EventAggregatorPratice;
using Prism.Events;
using System;
using System.Collections.Generic;
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

namespace WpfControlDev.View
{
    /// <summary>
    /// WinEventAggregator.xaml 的交互逻辑
    /// </summary>
    public partial class WinEventAggregator : Window
    {
        public WinEventAggregator()
        {
            InitializeComponent();
            SetSubscribe();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //抛出事件
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("用户昵称：").Append(txtNick.Text).Append(Environment.NewLine);
            strBuilder.Append("用户年龄：").Append(txtAge.Text).Append(Environment.NewLine);
            strBuilder.Append("用户性别：").Append(txtSex.Text).Append(Environment.NewLine);
            strBuilder.Append("用户电话：").Append(txtTel.Text).Append(Environment.NewLine);
            strBuilder.Append("用户住址：").Append(txtAddress.Text).Append(Environment.NewLine);
            SetPublish(strBuilder.ToString());
        }

        public void SetPublish(string messageData)
        {
            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<GetInputMessages>()
                .Publish(messageData);
        }

        public void SetSubscribe()
        {
            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<GetInputMessages>()
                .Subscribe(ReceiveMessage, ThreadOption.UIThread, true);
        }

        public void ReceiveMessage(string messageData)
        {
            this.txtResult.Text = messageData;
        }
    }
}


namespace EventAggregatorPratice
{
    /// <summary>
    /// 自定义的事件，一定要继承自CompositePresentationEvent类，或者继承EventBase，不做任何实现，相当于一个标志，给这个事件一个名字
    /// </summary>
    public class GetInputMessages : PubSubEvent<string>
    {
    }

    public class EventAggregatorRepository
    {
        public EventAggregatorRepository()
        {
            eventAggregator = new EventAggregator();
        }

        public IEventAggregator eventAggregator;
        public static EventAggregatorRepository eventRepository = null;

        //单例，保持内存唯一实例
        public static EventAggregatorRepository GetInstance()
        {
            if (eventRepository == null)
            {
                eventRepository = new EventAggregatorRepository();
            }
            return eventRepository;
        }
    }
}