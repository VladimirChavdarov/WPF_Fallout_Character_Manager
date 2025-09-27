using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;
using Condition = WPF_Fallout_Character_Manager.Models.External.Condition;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class ConditionsViewModel : ViewModelBase
    {
        // local variables
        XtrnlConditionsModel _xtrnlConditionsModel;
        ConditionsModel _conditionsModel;
        Condition _conditionToAdd;
        //

        // public variables
        public XtrnlConditionsModel XtrnlConditionsModel
        {
            get => _xtrnlConditionsModel;
            set => Update(ref _xtrnlConditionsModel, value);
        }
        public ConditionsModel ConditionsModel
        {
            get => _conditionsModel;
            set => Update(ref _conditionsModel, value);
        }

        public Condition ConditionToAdd
        {
            get => _conditionToAdd;
            set
            {
                //Update(ref _conditionToAdd, value);

                // NOTE: Because we need a cloned object, we do it manually instead of calling the Update utility function.
                _conditionToAdd = value.Clone();
                OnPropertyChanged();
            }
        }
        //

        // constructor
        public ConditionsViewModel(XtrnlConditionsModel? xtrnlConditionsModel, ConditionsModel? conditionsModel)
        {
            _xtrnlConditionsModel = xtrnlConditionsModel;
            _conditionsModel = conditionsModel;
            ResetConditionToAdd();

            // Bind a function to the command so it executes every time the command is sent 
            OpenConditionsModalWindowCommand = new RelayCommand(OpenConditionsModalWindow);
            AddConditionCommand = new RelayCommand(AddCondition);
            ResetSelectedConditionCommand = new RelayCommand(ResetSelectedCondition);
            RemoveConditionCommand = new RelayCommand(RemoveCondition);
            SwitchVisibilityCommand = new RelayCommand(SwitchVisibility);
            //
        }
        //

        // Commands
        // Open Modal window command
        public RelayCommand OpenConditionsModalWindowCommand { get;private set; }
        private void OpenConditionsModalWindow(object _ = null)
        {
            var window = new ConditionsWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X - 140;
            window.Top = mousePoint.Y - 100;

            window.ShowDialog();
        }
        //

        // Add condition to ConditionsModel command
        public RelayCommand AddConditionCommand { get;private set; }
        private void AddCondition(object _ = null)
        {
            if(ConditionToAdd != null)
                ConditionsModel.Conditions.Add(ConditionToAdd.Clone());
            ResetConditionToAdd();
        }
        //

        // Remove condition from ConditionsModel command
        public RelayCommand RemoveConditionCommand {  get;private set; }
        private void RemoveCondition(object param)
        {
            if(param is Condition conditionToRemove)
            {
                ConditionsModel.Conditions.Remove(conditionToRemove);
            }
        }
        //

        // Reset ConditionToAdd command
        public RelayCommand ResetSelectedConditionCommand { get; private set; }
        private void ResetSelectedCondition(object _ = null)
        {
            ResetConditionToAdd();
        }
        //

        // Switch Visibility command (visible <-> collapsed)
        public RelayCommand SwitchVisibilityCommand { get; private set; }
        public void SwitchVisibility(object param)
        {
            if (param is Condition condition)
            {
                if (condition.IsReadOnly == false)
                    return;

                condition.DescriptionVisibility = condition.DescriptionVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        //
        //

        // Methods
        private void ResetConditionToAdd()
        {
            ConditionToAdd = XtrnlConditionsModel.Conditions.FirstOrDefault(x => x.BaseValue.Name.Contains("Custom"));
            if(ConditionToAdd == null)
            {
                throw new Exception($"ConditionToAdd was null.");
            }
        }
        //
    }
}
