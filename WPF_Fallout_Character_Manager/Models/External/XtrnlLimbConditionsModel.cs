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
using WPF_Fallout_Character_Manager.Models.Serialization;

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
    public sealed class LimbCondition : ModTypeBase, ISerializable<LimbConditionDTO>
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

        public LimbCondition(LimbConditionDTO dto)
        {
            FromDto(dto);
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
            BaseValue = this.BaseValue.Clone(),
            Target = this.Target,
            APCost = this.APCost,
            Modifier = this.Modifier,
            Effects = this.Effects,

            SelectedExternalCondition = this.SelectedExternalCondition,
        };

        public LimbConditionDTO ToDto()
        {
            return new LimbConditionDTO
            {
                BaseValue = BaseValue,
                Target = Target,
                APCost = APCost,
                Modifier = Modifier,
                Effects = Effects,
            };
        }

        public void FromDto(LimbConditionDTO dto, bool versionMismatch = false)
        {
            BaseValue = dto.BaseValue.Clone();
            Target = dto.Target;
            APCost = dto.APCost;
            Modifier = dto.Modifier;
            Effects = dto.Effects;
        }
        //
    }
}
