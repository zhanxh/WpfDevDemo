using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfControlDev.Extend;

namespace WpfControlDev.ViewModel
{
    public class FastKeyVm : ViewModelBase
    {
        public FastKeyVm()
        {
            _KeyBindings = new ObservableCollection<KeyBinding>();
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.Q, ModifierKeys.Control|ModifierKeys.Alt, "Ctrl+Q")) { CommandParameter = "Key.Q" });
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.W, ModifierKeys.Control)) { CommandParameter = "Key.W" });
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.E, ModifierKeys.Control)) { CommandParameter = "Key.E" });
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.R, ModifierKeys.Control)) { CommandParameter = "Key.R" });
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.T, ModifierKeys.Control)) { CommandParameter = "Key.T" });
            _KeyBindings.Add(new KeyBinding(AltF4, new KeyGesture(Key.Y, ModifierKeys.Control)) { CommandParameter = "Key.Y" });
        }
        public ICommand AltF4
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("Key : " + obj.ToString());
                });
            }
        }

        ObservableCollection<KeyBinding> _KeyBindings;
        public ObservableCollection<KeyBinding> KeyBindings
        {
            get { return _KeyBindings; }
            set
            {
                _KeyBindings = value;
                RaisePropertyChanged("KeyBindings");
            }
        }
    }
   
}
