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
using WPF_Fallout_Character_Manager.Utilities;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.ViewModels.Serialization;
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
                ChangeTracker.SetDirty();
            }
        }
        //

        // Opening a dialog
        private bool _isDialogOpen = false;
        public bool IsDialogOpen
        {
            get => _isDialogOpen;
            set => Update(ref _isDialogOpen, value);
        }

        public static event Action<ViewModelBase, bool>? DialogStateChanged;
        protected void NotifyDialogStateChanged(ViewModelBase sender, bool isOpen)
        {
            DialogStateChanged?.Invoke(sender, isOpen);
        }
        private void OnDialogStateChanged(ViewModelBase sender, bool isOpen)
        {
            if (sender == this)
                return;

            IsDialogOpen = isOpen;
        }
        //

        // Constructor
        public ViewModelBase()
        {
            OpenModifierModalWindowCommand = new RelayCommand(OpenModifierModal);

            DialogStateChanged += OnDialogStateChanged;
        }
        //

        // Destructor
        ~ViewModelBase()
        {
            DialogStateChanged -= OnDialogStateChanged;
        }
        //

        // Open Modifier Windows
        public RelayCommand OpenModifierModalWindowCommand { get; private set; }
        // ChatGPT told me to do that in order to open the Modal through the View to follow the MVVM pattern. Double-check it.
        //public Action<object>? OnRequestOpenModifierModalWindow;

        private void OpenModifierModal(object modValue)
        {
            // test (which works!)
            //MessageBox.Show("I triggered the OpenModifiedModalWindowCommand!");


            //if(modInt != null)
            //{
            //    OnRequestOpenModifierModalWindow?.Invoke(modInt);
            //}

            // Summon the window directly from this command.
            Window? window = null;

            switch(modValue)
            {
                case ModInt modInt:
                {
                    window = new ModifiersWindow(new ModifierSystem.ModIntViewModel(modInt));
                    break;
                }
                case ModFloat modFloat:
                {
                    window = new ModifiersWindow(new ModifierSystem.ModFloatViewModel(modFloat));
                    break;
                }
                case ModString modString:
                {
                    window = new ModifiersWindow(new ModifierSystem.ModStringViewModel(modString));
                    break;
                }
                default:
                    {
                        MessageBox.Show("Invalid object passed to OpenModifierModal — expected ModValue<T>.");
                        return;
                    }
            }

            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            NotifyDialogStateChanged(this, true);
            try
            {
                window.ShowDialog();
            }
            finally
            {
                NotifyDialogStateChanged(this, false);
            }
        }
        //
    }
}
