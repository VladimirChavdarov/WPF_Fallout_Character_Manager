using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    public class LimbConditionsModel : ModelBase
    {
        // Constructor
        public LimbConditionsModel()
        {
            EyesLimbConditions = new ObservableCollection<LimbCondition>();
            HeadLimbConditions = new ObservableCollection<LimbCondition>();
            ArmLimbConditions = new ObservableCollection<LimbCondition>();
            TorsoLimbConditions = new ObservableCollection<LimbCondition>();
            GroinLimbConditions = new ObservableCollection<LimbCondition>();
            LegLimbConditions = new ObservableCollection<LimbCondition>();
            ObjectLimbConditions = new ObservableCollection<LimbCondition>();

            Console.WriteLine("Active Limb Conditions uploaded");
        }
        //

        // Helpers
        public void AddLimbCondition(LimbCondition limbCondition)
        {
            switch(limbCondition.Target)
            {
                case "Eyes":
                    EyesLimbConditions.Add(limbCondition);
                    break;
                case "Head":
                    HeadLimbConditions.Add(limbCondition);
                    break;
                case "Arms":
                    ArmLimbConditions.Add(limbCondition);
                    break;
                case "Torso":
                    TorsoLimbConditions.Add(limbCondition);
                    break;
                case "Groin":
                    GroinLimbConditions.Add(limbCondition);
                    break;
                case "Legs":
                    LegLimbConditions.Add(limbCondition);
                    break;
                case "Held or Carried Object":
                    ObjectLimbConditions.Add(limbCondition);
                    break;
                default:
                    break;
            }
        }
        //

        public void AddEmptyLimbCondition(string target)
        {
            LimbCondition newLimbCondition = new LimbCondition();
            newLimbCondition.Target = target;
            switch (target)
            {
                case "Eyes":
                    EyesLimbConditions.Add(newLimbCondition);
                    break;
                case "Head":
                    HeadLimbConditions.Add(newLimbCondition);
                    break;
                case "Arms":
                    ArmLimbConditions.Add(newLimbCondition);
                    break;
                case "Torso":
                    TorsoLimbConditions.Add(newLimbCondition);
                    break;
                case "Groin":
                    GroinLimbConditions.Add(newLimbCondition);
                    break;
                case "Legs":
                    LegLimbConditions.Add(newLimbCondition);
                    break;
                case "Held or Carried Object":
                    ObjectLimbConditions.Add(newLimbCondition);
                    break;
                default:
                    break;
            }
        }

        public void RemoveCondition(LimbCondition conditionToRemove)
        {
            switch (conditionToRemove.Target)
            {
                case "Eyes":
                    EyesLimbConditions.Remove(conditionToRemove);
                    break;
                case "Head":
                    HeadLimbConditions.Remove(conditionToRemove);
                    break;
                case "Arms":
                    ArmLimbConditions.Remove(conditionToRemove);
                    break;
                case "Torso":
                    TorsoLimbConditions.Remove(conditionToRemove);
                    break;
                case "Groin":
                    GroinLimbConditions.Remove(conditionToRemove);
                    break;
                case "Legs":
                    LegLimbConditions.Remove(conditionToRemove);
                    break;
                case "Held or Carried Object":
                    ObjectLimbConditions.Remove(conditionToRemove);
                    break;
                default:
                    break;
            }
        }

        public void ReplaceLimbCondition(LimbCondition oldCondition, LimbCondition newCondition)
        {
            switch (oldCondition.Target)
            {
                case "Eyes":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Head":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Arms":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Torso":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Groin":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Legs":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                case "Held or Carried Object":
                    ReplaceConditionInCollection(EyesLimbConditions, oldCondition, newCondition);
                    break;
                default:
                    break;
            }
        }

        private void ReplaceConditionInCollection(
            ObservableCollection<LimbCondition>? collection,
            LimbCondition oldCondition,
            LimbCondition newCondition)
        {
            if (collection is null)
                return;

            int index = collection.IndexOf(oldCondition);
            if (index >= 0)
            {
                collection[index] = newCondition;
            }
        }

        // Data
        public ObservableCollection<LimbCondition>? EyesLimbConditions { get; }
        public ObservableCollection<LimbCondition>? HeadLimbConditions { get; }
        public ObservableCollection<LimbCondition>? ArmLimbConditions { get; }
        public ObservableCollection<LimbCondition>? TorsoLimbConditions { get; }
        public ObservableCollection<LimbCondition>? GroinLimbConditions { get; }
        public ObservableCollection<LimbCondition>? LegLimbConditions { get; }
        public ObservableCollection<LimbCondition>? ObjectLimbConditions { get; }
        //
    }
}
