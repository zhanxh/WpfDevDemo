using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControlDev.Yoyo
{
    public class OpenOrClose : Control
    {
        public static char Open = '1';
        public static char Close = '2';
        public static char CloseToday = '4';

        private RadioButton rbOpen;
        private RadioButton rbClose;
        private RadioButton rbCloseToday;

        static OpenOrClose()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OpenOrClose), new FrameworkPropertyMetadata(typeof(OpenOrClose)));
        }

        public static readonly DependencyProperty EntrustKpProperty = DependencyProperty.Register(
                                                  "EntrustKp", typeof(char), typeof(OpenOrClose), new PropertyMetadata('1'));

        public char EntrustKp
        {
            get { return (char)GetValue(EntrustKpProperty); }
            set { SetValue(EntrustKpProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            rbOpen = GetTemplateChild("rbOpen") as RadioButton;
            if (rbOpen != null)
            {
                rbOpen.Checked += Open_Checked;
            }

            rbClose = GetTemplateChild("rbClose") as RadioButton;
            if (rbClose != null)
            {
                rbClose.Checked += Close_Checked;
            }

            rbCloseToday = GetTemplateChild("rbCloseToday") as RadioButton;
            if (rbCloseToday != null)
            {
                rbCloseToday.Checked += Closetoday_Checked;
            }
            GotFocus += OpenOrClosePanel_GotFocus;
            PreviewKeyDown += OpenOrClose_PreviewKeyDown;
            base.OnApplyTemplate();
        }

        private void OpenOrClosePanel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (rbOpen.IsChecked == true)
            {
                rbOpen.Focus();
            }
            else if (rbClose.IsChecked == true)
            {
                rbClose.Focus();
            }
            else if (rbCloseToday.IsChecked == true)
            {
                rbCloseToday.Focus();
            }
        }

        private void OpenOrClose_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None &&
                  (e.KeyStates == Keyboard.GetKeyStates(Key.Left) || e.KeyStates == Keyboard.GetKeyStates(Key.Right)))
            {
                if (!rbOpen.IsFocused && !rbClose.IsFocused && !rbCloseToday.IsFocused)
                    return;
                if (e.Key == Key.Left)
                {
                    if (rbOpen.IsChecked == true)
                    {
                        //rbOpen.IsChecked = false;
                        if (rbCloseToday.IsVisible)
                        {
                            //rbClose.IsChecked = false;
                            rbCloseToday.IsChecked = true;
                            rbCloseToday.Focus();
                        }
                        else
                        {
                            rbClose.IsChecked = true;
                            rbClose.Focus();
                            //rbCloseToday.IsChecked = false;
                        }
                    }
                    else if (rbClose.IsChecked == true)
                    {
                        rbOpen.IsChecked = true;
                        rbOpen.Focus();
                        //rbClose.IsChecked = false;
                        //rbCloseToday.IsChecked = false;
                    }
                    else if (rbCloseToday.IsChecked == true)
                    {
                        //rbOpen.IsChecked = false;
                        rbClose.IsChecked = true;
                        rbClose.Focus();
                        //rbCloseToday.IsChecked = false;
                    }
                }
                else if (e.Key == Key.Right)
                {
                    if (rbOpen.IsChecked == true)
                    {
                        //rbOpen.IsChecked = false;
                        rbClose.IsChecked = true;
                        rbClose.Focus();
                        //rbCloseToday.IsChecked = false;
                    }
                    else if (rbClose.IsChecked == true)
                    {
                        //rbClose.IsChecked = false;
                        if (rbCloseToday.IsVisible)
                        {
                            rbCloseToday.IsChecked = true;
                            rbCloseToday.Focus();
                            //rbOpen.IsChecked = false;
                        }
                        else
                        {
                            rbOpen.IsChecked = true;
                            rbOpen.Focus();
                            //rbCloseToday.IsChecked = false;
                        }


                    }
                    else if (rbCloseToday.IsChecked == true)
                    {
                        rbOpen.IsChecked = true;
                        rbOpen.Focus();
                        //rbClose.IsChecked = false;
                        //rbCloseToday.IsChecked = false;
                    }
                }

                e.Handled = true;
            }
        }

        private void Open_Checked(object sender, RoutedEventArgs e)
        {
            EntrustKp = Open;
            if (!rbOpen.IsFocused)
            {
                rbOpen.Focus();
            }
        }

        private void Close_Checked(object sender, RoutedEventArgs e)
        {
            EntrustKp = Close;
            if (!rbClose.IsFocused)
            {
                rbClose.Focus();
            }
        }

        private void Closetoday_Checked(object sender, RoutedEventArgs e)
        {
            EntrustKp = CloseToday;
            if (!rbCloseToday.IsFocused)
            {
                rbCloseToday.Focus();
            }
        }
    }
}