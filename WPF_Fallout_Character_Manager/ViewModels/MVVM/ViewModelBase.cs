using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

namespace WPF_Fallout_Character_Manager.ViewModels.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void Update<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
        //

        // Constructor
        public ViewModelBase()
        {
            OpenModifierModalWindowCommand = new RelayCommand(OpenModal);
        }
        //

        // Open Modifier Windows
        public RelayCommand OpenModifierModalWindowCommand { get; private set; }
        // ChatGPT told me to do that in order to open the Modal through the View to follow the MVVM pattern. Double-check it.
        public Action<object>? OnRequestOpenModifierModalWindow;

        private void OpenModal(object modInt)
        {
            // test (which works!)
            //MessageBox.Show("I triggered the OpenModifiedModalWindowCommand!");


            //if(modInt != null)
            //{
            //    OnRequestOpenModifierModalWindow?.Invoke(modInt);
            //}

            // Summon the window directly from this command.
            if (modInt is ModInt typedModInt)
            {
                var window = new ModifiersWindow(new ModifierSystem.ModIntViewModel(typedModInt));
                var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
                window.Left = mousePoint.X + 100;
                window.Top = mousePoint.Y + 100;

                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid object passed to OpenModal — expected ModInt.");
            }
        }
        //
    }
}
