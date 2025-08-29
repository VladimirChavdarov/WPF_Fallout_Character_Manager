using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlLimbConditionsModel : ModelBase
    {
        // Constructor
        public XtrnlLimbConditionsModel()
        {
            EyesLimbConditions = new ObservableCollection<LimbCondition>();
            HeadLimbConditions = new ObservableCollection<LimbCondition>();
            ArmLimbConditions = new ObservableCollection<LimbCondition>();
            TorsoLimbConditions = new ObservableCollection<LimbCondition>();
            GroinLimbConditions = new ObservableCollection<LimbCondition>();
            LegLimbConditions = new ObservableCollection<LimbCondition>();
            ObjectLimbConditions = new ObservableCollection<LimbCondition>();

            var lines = File.ReadAllLines("Resources/Spreadsheets/limb_conditions.csv");

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(';');

                // Skip invalid rows
                if (parts.Length < 6)
                    continue;

                LimbCondition condition = new LimbCondition(
                    name: parts[0],
                    target: parts[1],
                    apCost: parts[2],
                    modifier: parts[3],
                    effects: parts[4],
                    description: parts[5]
                    );

                // distribute based on target
                switch(parts[1])
                {
                    case "Eyes":
                        EyesLimbConditions.Add(condition);
                        break;
                    case "Head":
                        HeadLimbConditions.Add(condition);
                        break;
                    case "Arm":
                        ArmLimbConditions.Add(condition);
                        break;
                    case "Torso":
                        TorsoLimbConditions.Add(condition);
                        break;
                    case "Groin":
                        GroinLimbConditions.Add(condition);
                        break;
                    case "Leg":
                        LegLimbConditions.Add(condition);
                        break;
                    case "Held or Carried Object":
                        ObjectLimbConditions.Add(condition);
                        break;
                    default:
                        break;
                }
                //
            }
            Console.WriteLine("Limb Conditions uploaded");
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

    // Technically ModTypeBase should only be used for Values that can have attached modifiers but it's just an IPropertyChanged interface so I'll
    // use it here as well.
    public sealed class LimbCondition : ModTypeBase
    {
        public LimbCondition(
            string name = "NewLimbCondition",
            string target = "None",
            string apCost = "None",
            string modifier = "None",
            string effects = "None",
            string description = "No Description")
        {
            _name = name;
            _target = target;
            _apCost = apCost;
            _modifier = modifier;
            _effects = effects;
            _description = description;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        private string _target;
        public string Target
        {
            get => _target;
            set => Update(ref _target, value);
        }

        private string _apCost;
        public string APCost
        {
            get => _apCost;
            set => Update(ref _apCost, value);
        }

        private string _modifier;
        public string Modifier
        {
            get => _modifier;
            set => Update(ref _modifier, value);
        }

        private string _effects;
        public string Effects
        {
            get => _effects;
            set => Update(ref _effects, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => Update(ref _description, value);
        }
    }
}
