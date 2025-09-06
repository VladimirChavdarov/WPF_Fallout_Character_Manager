using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    public class LimbConditionsModel : ModelBase
    {
        // Constructor
        public LimbConditionsModel()
        {
            LimbConditions = new ObservableCollection<LimbCondition>();
        }
        //

        // Helpers
        public void AddLimbCondition(LimbCondition limbCondition)
        {
            LimbConditions.Add(limbCondition);
        }
        //

        // TODO: This might need reworking depending on how the drop-down for choosing a condition will work.
        public void AddEmptyLimbCondition(string target)
        {
            LimbCondition newLimbCondition = new LimbCondition();
            newLimbCondition.Target = target;
            LimbConditions.Add(newLimbCondition);
        }

        public void RemoveCondition(LimbCondition conditionToRemove)
        {
            LimbConditions.Remove(conditionToRemove);
        }

        public void RemoveCondition(string name, string target)
        {
            LimbCondition limbConditionToRemove = LimbConditions.FirstOrDefault(m => m.Name == name && m.Target == target);
            if (limbConditionToRemove != null)
                LimbConditions.Remove(limbConditionToRemove);
            else
                throw new Exception($"Limb condition with label '{name}' and target '{target}' not found.");
        }

        public void ReplaceLimbCondition(LimbCondition oldCondition, LimbCondition newCondition)
        {
            if (LimbConditions is null)
                return;

            int index = LimbConditions.IndexOf(oldCondition);
            if (index >= 0)
            {
                LimbConditions[index] = newCondition;
            }
        }

        // Data
        public ObservableCollection<LimbCondition>? LimbConditions { get; }
        //
    }
}
