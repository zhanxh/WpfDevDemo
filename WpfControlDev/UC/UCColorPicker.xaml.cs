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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlDev.UC
{
    /// <summary>
    /// UCColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class UCColorPicker : UserControl
    {
        public UCColorPicker()
        {
            InitializeComponent();
        }

        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(UCColorPicker), new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
        public static DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(UCColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRBGChanged)));
        public static DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(UCColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRBGChanged)));
        public static DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(UCColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRBGChanged)));


        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {//此处不会触发OnColorRBGChanged,造成递归
            Color newColor = (Color)e.NewValue;
            Color oldColor = (Color)e.OldValue;

            UCColorPicker ucColorPicker = (UCColorPicker)sender;
            ucColorPicker.Red = newColor.R; 
            ucColorPicker.Green = newColor.G;
            ucColorPicker.Blue = newColor.B;

            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
            args.RoutedEvent = UCColorPicker.ColorChangedEvent;
            ucColorPicker.RaiseEvent(args);
        }

        private static void OnColorRBGChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UCColorPicker ucColorPicker = (UCColorPicker)sender;
            Color color = ucColorPicker.Color;

            if(e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
            }
            if (e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
            }
            if (e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
            }
            ucColorPicker.Color = color;
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(UCColorPicker));

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent,value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
    }
}
