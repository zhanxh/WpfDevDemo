using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControlDev.Yoyo
{
    public class BuyOrSell : Control
    {
        public static char Buy = '1';
        public static char Sell = '2';

        private RadioButton rbBuy;
        private RadioButton rbSale;

        static BuyOrSell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BuyOrSell),
                new FrameworkPropertyMetadata(typeof(BuyOrSell)));
        }

        public static readonly DependencyProperty EntrustBsProperty = DependencyProperty.Register(
            "EntrustBs", typeof(char), typeof(BuyOrSell), new PropertyMetadata('1'));

        public char EntrustBs
        {
            get { return (char)GetValue(EntrustBsProperty); }
            set { SetValue(EntrustBsProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            rbBuy = GetTemplateChild("rbBuy") as RadioButton;
            if (rbBuy != null)
            {
                rbBuy.Checked += Buy_Checked;
            }

            rbSale = GetTemplateChild("rbSale") as RadioButton;
            if (rbSale != null)
            {
                rbSale.Checked += Sell_Checked;
            }
            GotFocus += BuyOrSellPanel_GotFocus;
            PreviewKeyDown += BuyOrSellPanel_PreviewKeyDown;
            base.OnApplyTemplate();
        }

        private void BuyOrSellPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None &&
                  (e.KeyStates == Keyboard.GetKeyStates(Key.Left) || e.KeyStates == Keyboard.GetKeyStates(Key.Right)))
            {
                if (!rbSale.IsFocused && !rbBuy.IsFocused)
                    return;
                if (rbSale.IsChecked == true)
                {
                    rbBuy.IsChecked = true;
                    rbBuy.Focus();
                }
                else
                {
                    rbSale.IsChecked = true;
                    rbSale.Focus();
                }

                e.Handled = true;
            }
        }

        private void BuyOrSellPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (rbBuy.IsChecked == true)
            {
                rbBuy.Focus();
            }
            else if (rbSale.IsChecked == true)
            {
                rbSale.Focus();
            }
        }

        private void Buy_Checked(object sender, RoutedEventArgs e)
        {
            EntrustBs = Buy;
            if (!rbBuy.IsFocused)
            {
                rbBuy.Focus();
            }
        }

        private void Sell_Checked(object sender, RoutedEventArgs e)
        {
            EntrustBs = Sell;
            if (!rbSale.IsFocused)
            {
                rbSale.Focus();
            }
        }

    }
}