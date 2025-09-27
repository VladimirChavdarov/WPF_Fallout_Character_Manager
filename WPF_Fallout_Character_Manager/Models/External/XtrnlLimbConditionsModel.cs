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
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models.External
{
    public class XtrnlLimbConditionsModel : ModelBase
    {
        // Constructor
        public XtrnlLimbConditionsModel()
        {
            LimbConditions = new ObservableCollection<LimbCondition>();

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

                LimbConditions.Add(condition);
            }
            Console.WriteLine("Limb Conditions uploaded");
        }
        //

        // Data
        public ObservableCollection<LimbCondition>? LimbConditions { get; }
        //
    }

    // Technically ModTypeBase should only be used for Values that can have attached modifiers but it's just an IPropertyChanged interface so I'll
    // use it here as well.
    public sealed class LimbCondition : ModTypeBase
    {
        // constructor
        public LimbCondition(
            string name = "NewLimbCondition",
            string target = "None",
            string apCost = "None",
            string modifier = "None",
            string effects = "None",
            string description = "No Description")
        {
            _baseValue = new LabeledString(name, description, description);
            _target = target;
            _apCost = apCost;
            _modifier = modifier;
            _effects = effects;
        }
        //

        // members
        private LabeledString _baseValue;
        public LabeledString BaseValue
        {
            get => _baseValue;
            set => Update(ref _baseValue, value);
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

        private LimbCondition _selectedExternalCondition;
        public LimbCondition SelectedExternalCondition
        {
            get => _selectedExternalCondition;
            set => Update(ref _selectedExternalCondition, value);
        }
        //

        // methods
        public LimbCondition Clone() => new LimbCondition
        {
            BaseValue = new LabeledString(this.BaseValue.Name, this.BaseValue.Value, this.BaseValue.Note),
            Target = this.Target,
            APCost = this.APCost,
            Modifier = this.Modifier,
            Effects = this.Effects,

            SelectedExternalCondition = this.SelectedExternalCondition,
        };
        //
    }
}
