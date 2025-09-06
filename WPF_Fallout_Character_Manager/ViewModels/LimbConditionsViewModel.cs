using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    public class LimbConditionsViewModel : ViewModelBase
    {
        // local variables
        private XtrnlLimbConditionsModel _xtrnlLimbConditionsModel;
        private LimbConditionsModel _limbConditionsModel;
        private string _currentLimbName;
        private bool _isModalOpen = false;
        //

        // public variables
        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel
        {
            get => _xtrnlLimbConditionsModel;
            set => Update(ref _xtrnlLimbConditionsModel, value);
        }

        public LimbConditionsModel LimbConditionsModel
        {
            get => _limbConditionsModel;
            set => Update(ref _limbConditionsModel, value);
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
        
        public int EyesConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("eyes", StringComparison.OrdinalIgnoreCase));
        public int HeadConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("head", StringComparison.OrdinalIgnoreCase));
        public int ArmsConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("arms", StringComparison.OrdinalIgnoreCase));
        public int TorsoConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("torso", StringComparison.OrdinalIgnoreCase));
        public int GroinConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("groin", StringComparison.OrdinalIgnoreCase));
        public int LegsConditionsCount => _limbConditionsModel.LimbConditions.Count(lc => lc.Target.Equals("legs", StringComparison.OrdinalIgnoreCase));

        public ObservableCollection<LimbCondition> XtrnlLimbConditionsObserved { get; }  // the contents will change based on which limb list is opened
        public ObservableCollection<LimbCondition> ActiveLimbConditionsObserved { get; } // the contents will change based on which limb list is opened
        //

        // constructor
        public LimbConditionsViewModel(XtrnlLimbConditionsModel? xtrnlLimbConditionsModel, LimbConditionsModel limbConditions)
        {
            _xtrnlLimbConditionsModel = xtrnlLimbConditionsModel;
            LimbConditionsModel = limbConditions;
            CurrentLimbName = "Placeholder Limb Name";
            IsModalOpen = false;
            XtrnlLimbConditionsObserved = new ObservableCollection<LimbCondition>();
            ActiveLimbConditionsObserved = new ObservableCollection<LimbCondition>();

            // Bind a function to the command so it executes every time the command is sent 
            OpenLimbConditionsModalWindowCommand = new RelayCommand(OpenLimbConditionsModal);

            AddLimbConditionCommand = new RelayCommand(
                _ =>
                {
                    LimbCondition lc = XtrnlLimbConditionsObserved.FirstOrDefault();
                    LimbConditionsModel.AddLimbCondition(lc);
                });

            RemoveLimbConditionCommand = new RelayCommand(
                param =>
                {
                    if (param is LimbCondition limbCondition)
                    {
                        LimbConditionsModel.RemoveCondition(limbCondition);
                    }
                });

            ReplaceLimbConditionCommand = new RelayCommand(
                param =>
                {
                    if (param is LimbCondition limbCondition)
                    { 
                        ReplaceLimbConditionVM(limbCondition);
                    }
                });
            //

            // Update the counters of the limb conditions every time the observable collection in the Model changes.
            _limbConditionsModel.LimbConditions.CollectionChanged += (s, e) =>
            {
                RaiseAllCountsChanged();
                UpdateObservedCollections();
            };
            //
        }
        //

        // Methods
        private void RaiseAllCountsChanged()
        {
            OnPropertyChanged(nameof(EyesConditionsCount));
            OnPropertyChanged(nameof(HeadConditionsCount));
            OnPropertyChanged(nameof(ArmsConditionsCount));
            OnPropertyChanged(nameof(TorsoConditionsCount));
            OnPropertyChanged(nameof(GroinConditionsCount));
            OnPropertyChanged(nameof(LegsConditionsCount));
        }

        private void UpdateObservedCollections()
        {
            // Update available options for selecting a new condition
            XtrnlLimbConditionsObserved.Clear();
            foreach(LimbCondition lc in XtrnlLimbConditionsModel.LimbConditions)
            {
                if(lc.Target == CurrentLimbName)
                    XtrnlLimbConditionsObserved.Add(lc);
            }
            //

            // Update the active conditions only for the selected limb
            ActiveLimbConditionsObserved.Clear();
            foreach(LimbCondition lc in LimbConditionsModel.LimbConditions)
            {
                if(lc.Target == CurrentLimbName)
                    ActiveLimbConditionsObserved.Add(lc);
            }
            //
        }
        //

        // Open Limb Conditions Window
        public RelayCommand OpenLimbConditionsModalWindowCommand { get; private set; }

        private void OpenLimbConditionsModal(object windowName)
        {
            if (windowName is string typedWindowName)
                CurrentLimbName = typedWindowName;
            else
                CurrentLimbName = "Invalid Limb Name";

            UpdateObservedCollections();

            IsModalOpen = true;

            // Open Modal window
            var window = new LimbConditionsWindow(this);
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X - 140;
            window.Top = mousePoint.Y + 100;

            window.ShowDialog();
            //
        }
        //

        // Add/Remove Limb Condition
        public RelayCommand AddLimbConditionCommand { get; private set; }
        public RelayCommand RemoveLimbConditionCommand { get; }
        //

        // Replace Limb Condition
        public RelayCommand ReplaceLimbConditionCommand { get; }

        private void ReplaceLimbConditionVM(LimbCondition selectedCondition)
        {
            if (selectedCondition == null)
                return;

            // Match the old condition to the one from the Active Limb conditions
            var oldCondition = ActiveLimbConditionsObserved.FirstOrDefault(lc => lc.Target == selectedCondition.Target && lc.Name != selectedCondition.Name);

            if (oldCondition != null)
            {
                LimbConditionsModel.ReplaceLimbCondition(oldCondition, selectedCondition);
                UpdateObservedCollections();
            }
        }
        //
    }
}
