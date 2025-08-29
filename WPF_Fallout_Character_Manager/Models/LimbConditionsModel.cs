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
                case "Arm":
                    ArmLimbConditions.Add(limbCondition);
                    break;
                case "Torso":
                    TorsoLimbConditions.Add(limbCondition);
                    break;
                case "Groin":
                    GroinLimbConditions.Add(limbCondition);
                    break;
                case "Leg":
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
