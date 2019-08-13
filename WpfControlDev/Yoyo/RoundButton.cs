using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfControlDev.Yoyo
{
    public class RoundButton : Button
    {

        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(RoundButton), new PropertyMetadata(22D));

        public static readonly DependencyProperty EllipseStrokeThicknessProperty = DependencyProperty.Register("EllipseStrokeThickness", typeof(double), typeof(RoundButton), new PropertyMetadata(1D));

        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(RoundButton));

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(RoundButton), new PropertyMetadata(12D));

        public static readonly DependencyProperty BackgroundMouseOverBrushProperty = DependencyProperty.Register("BackgroundMouseOverBrush", typeof(SolidColorBrush), typeof(RoundButton), new UIPropertyMetadata(null));

        static RoundButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundButton), new FrameworkPropertyMetadata(typeof(RoundButton)));
        }

        /// <summary>
        /// 获取或设置椭圆直径。
        /// </summary>
        [Description("获取或设置椭圆直径")]
        [Category("Common Properties")]
        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        /// <summary>
        /// 获取或设置椭圆笔触粗细。
        /// </summary>
        [Description("获取或设置椭圆笔触粗细")]
        [Category("Common Properties")]
        public double EllipseStrokeThickness
        {
            get { return (double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        /// <summary>
        /// 获取或设置图标路径数据。
        /// </summary>        
        [Description("获取或设置图标路径数据")]
        [Category("Common Properties")]
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        /// <summary>
        ///获取或设置icon图标的高和宽。
        /// </summary>       
        [Description("获取或设置icon图标的高和宽")]
        [Category("Common Properties")]
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        ///获取或设置icon图标的高和宽。
        /// </summary>       
        [Description("获取或设置icon图标的高和宽")]
        [Category("Common Properties")]
        public SolidColorBrush BackgroundMouseOverBrush
        {
            get { return (SolidColorBrush)GetValue(BackgroundMouseOverBrushProperty); }
            set { SetValue(BackgroundMouseOverBrushProperty, value); }
        }

    }
}