using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    public class LimbConditionsViewModel : ViewModelBase
    {
        // local variables
        private XtrnlLimbConditionsModel _xtrnlLimbConditions;
        private LimbConditionsModel _limbConditions;
        private string _currentLimbName;
        private bool _isModalOpen = false;
        //

        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel
        {
            get => _xtrnlLimbConditions;
            set => Update(ref _xtrnlLimbConditions, value);
        }

        public LimbConditionsModel LimbConditionsModel
        {
            get => _limbConditions;
            set => Update(ref _limbConditions, value);
        }

        public string CurrentLimbName
        {
            get => _currentLimbName;
            set => Update(ref _currentLimbName, value);
        }

        public bool IsModalOpen
        {
            get => _isModalOpen;
            set => Update(ref _isModalOpen, value);
        }

        // constructor
        public LimbConditionsViewModel(XtrnlLimbConditionsModel? xtrnlLimbConditionsModel, LimbConditionsModel limbConditions)
        {
            _xtrnlLimbConditions = xtrnlLimbConditionsModel;
            LimbConditionsModel = limbConditions;
            CurrentLimbName = "Placeholder Limb Name";
            IsModalOpen = false;

            OpenLimbConditionsModalWindowCommand = new RelayCommand(OpenLimbConditionsModal);
        }
        //

        // Open Limb Conditions Window
        public RelayCommand OpenLimbConditionsModalWindowCommand { get; private set; }

        private void OpenLimbConditionsModal(object windowName)
        {

            if (windowName is string typedWindowName)
                CurrentLimbName = typedWindowName + " Conditions";
            else
                CurrentLimbName = "Invalid Limb Name";

            IsModalOpen = true;

            var window = new LimbConditionsWindow(this);
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X - 140;
            window.Top = mousePoint.Y + 100;

            window.ShowDialog();
        }
        //
    }
}
