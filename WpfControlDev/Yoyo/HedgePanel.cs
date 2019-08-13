using System.Windows;
using System.Windows.Controls;

namespace WpfControlDev.Yoyo
{
    public class HedgePanel : Control
    {
        public static char Hedge = '1';
        public static char Arbitrage = '2';

        private CheckBox _hedge;
        private CheckBox _arbitrage;

        public static readonly DependencyProperty EntrustHedgeProperty = DependencyProperty.Register(
            "EntrustHedge", typeof(char), typeof(HedgePanel), new PropertyMetadata(' '));

        public char EntrustHedge
        {
            get { return (char)GetValue(EntrustHedgeProperty); }
            set { SetValue(EntrustHedgeProperty, value); }
        }

        static HedgePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HedgePanel), new FrameworkPropertyMetadata(typeof(HedgePanel)));
        }

        public override void OnApplyTemplate()
        {
            _hedge = GetTemplateChild("rbHedging") as CheckBox;
            if (_hedge != null)
            {
                _hedge.Checked += Hedge_Checked;
                _hedge.Unchecked += _hedge_Unchecked;
            }
            _arbitrage = GetTemplateChild("rbArbitrage") as CheckBox;
            if (_arbitrage != null)
            {
                _arbitrage.Checked += Arbitrage_Checked;
                _arbitrage.Unchecked += _arbitrage_Unchecked;
            }
            base.OnApplyTemplate();
        }

        private void _arbitrage_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_hedge?.IsChecked != null && !_hedge.IsChecked.Value)
            {
                EntrustHedge = '0';
            }
        }

        private void _hedge_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_arbitrage?.IsChecked != null && !_arbitrage.IsChecked.Value)
            {
                EntrustHedge = '0';
            }
        }

        private void Arbitrage_Checked(object sender, RoutedEventArgs e)
        {
            if (_hedge != null)
            {
                _hedge.IsChecked = false;
            }
            EntrustHedge = Arbitrage;
        }

        private void Hedge_Checked(object sender, RoutedEventArgs e)
        {
            if (_arbitrage != null)
            {
                _arbitrage.IsChecked = false;
            }
            EntrustHedge = Hedge;
        }
    }
}