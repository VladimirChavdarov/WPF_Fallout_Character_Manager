using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            set
            {
                Update(ref _currentLimbName, value);

                if(XtrnlLimbConditionsObserved != null)
                    XtrnlLimbConditionsObserved.Refresh();
                if(ActiveLimbConditionsObserved != null)
                    ActiveLimbConditionsObserved.Refresh();
            }
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

        public ICollectionView XtrnlLimbConditionsObserved { get; }
        //public ObservableCollection<LimbCondition> XtrnlLimbConditionsObserved { get; }  // the contents will change based on which limb list is opened
        public ICollectionView ActiveLimbConditionsObserved { get; }
        //public ObservableCollection<LimbCondition> ActiveLimbConditionsObserved { get; } // the contents will change based on which limb list is opened
        //

        // constructor
        public LimbConditionsViewModel(XtrnlLimbConditionsModel? xtrnlLimbConditionsModel, LimbConditionsModel limbConditions)
        {
            _xtrnlLimbConditionsModel = xtrnlLimbConditionsModel;
            LimbConditionsModel = limbConditions;
            CurrentLimbName = "Placeholder Limb Name";
            IsModalOpen = false;
            XtrnlLimbConditionsObserved = CollectionViewSource.GetDefaultView(XtrnlLimbConditionsModel.LimbConditions);
            XtrnlLimbConditionsObserved.Filter = FilterByLimb;
            ActiveLimbConditionsObserved = CollectionViewSource.GetDefaultView(LimbConditionsModel.LimbConditions);
            ActiveLimbConditionsObserved.Filter = FilterByLimb;

            // Bind a function to the command so it executes every time the command is sent 
            OpenLimbConditionsModalWindowCommand = new RelayCommand(OpenLimbConditionsModal);

            AddLimbConditionCommand = new RelayCommand(_ =>
            {
                // Pick a default template
                LimbCondition? template = XtrnlLimbConditionsModel.LimbConditions.FirstOrDefault(lc => lc.Target == CurrentLimbName);
                if (template == null)
                    return;

                // Always create a new instance
                LimbCondition? newCondition = template.Clone();
                newCondition.SelectedExternalCondition = template; // SelectedExternalCondition should always get its value from the XtrnlLimbConditions.

                LimbConditionsModel.AddLimbCondition(newCondition);

            });

            RemoveLimbConditionCommand = new RelayCommand(
                param =>
                {
                    if (param is LimbCondition limbCondition)
                    {
                        LimbConditionsModel.RemoveCondition(limbCondition);
                    }
                });

            ReplaceLimbConditionCommand = new RelayCommand(param =>
            {
                if (param is LimbCondition activeCondition && activeCondition.SelectedExternalCondition != null)
                {
                    LimbCondition oldLimbCondition = activeCondition.Clone();

                    LimbCondition? selected = activeCondition.SelectedExternalCondition;

                    // Copy all fields from the selected external template
                    activeCondition.Name = selected.Name;
                    activeCondition.Target = selected.Target;
                    activeCondition.APCost = selected.APCost;
                    activeCondition.Modifier = selected.Modifier;
                    activeCondition.Effects = selected.Effects;
                    activeCondition.Description = selected.Description;

                    // Clear temporary selection
                    activeCondition.SelectedExternalCondition = selected;

                    LimbConditionsModel.ReplaceLimbCondition(oldLimbCondition, activeCondition);
                }
            });

            //

            // Update the counters of the limb conditions every time the observable collection in the Model changes.
            _limbConditionsModel.LimbConditions.CollectionChanged += (s, e) =>
            {
                RaiseAllCountsChanged();
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

        private bool FilterByLimb(object obj)
        {
            if(obj is LimbCondition lc)
            {
                return lc.Target.Equals(CurrentLimbName, StringComparison.OrdinalIgnoreCase);
            }
            return false;
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
        //
    }
}
