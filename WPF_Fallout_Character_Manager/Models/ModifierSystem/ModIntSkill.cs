using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public class ModIntSkill : ModInt
    {
        // constructor
        public ModIntSkill(string name, int value, ObservableCollection<SPECIAL> skillModifiers, bool isBaseValueReadOnly = false, string hint = "") : base(name, value, isBaseValueReadOnly, hint)
        {
            _isTagged = false;
            SkillModifiers = skillModifiers;
            SelectedModifier = SkillModifiers.LastOrDefault();
        }

        public ModIntSkill(ModIntSkillDTO dto, ModIntSkill fallback = null)
            : base(dto, fallback)
        {
            IsTagged = dto.IsTagged;
            SelectedModifier = dto.SelectedModifier;
            SkillModifiers = new ObservableCollection<SPECIAL>();
            SkillModifiers.Clear();
            foreach (SPECIAL skillMod in dto.SkillModifiers)
            {
                SkillModifiers.Add(skillMod);
            }
        }

        public ModIntSkill(ModIntSkill other) : base(other)
        {
            IsTagged = other.IsTagged;
            SelectedModifier = other.SelectedModifier;
            SkillModifiers = new ObservableCollection<SPECIAL>();
            SkillModifiers.Clear();
            foreach(SPECIAL skillMod in other.SkillModifiers)
            {
                SkillModifiers.Add(skillMod);
            }
        }
        //

        // methods
        public ModIntSkillDTO ToDto()
        {
            return new ModIntSkillDTO
            {
                BaseValueObject = _baseValueObject,
                IsBaseValueReadOnly = _isBaseValueReadOnly,
                Modifiers = Modifiers.ToList(),
                IsTagged = _isTagged,
                SelectedModifier = _selectedModifier,
                SkillModifiers = SkillModifiers.ToList(),
            };
        }
        //

        // Data
        public static string TaggedModifierName = "Tagged";

        private bool _isTagged;
        public bool IsTagged
        {
            get => _isTagged;
            set
            {
                //_isTagged = value;
                Update(ref _isTagged, value);

                if (value)
                {
                    if(!Modifiers.Any(x => x.Name == TaggedModifierName))
                    {
                        //AddModifier(new LabeledInt(TaggedModifierName, 2, "You are proficient in this skill.", true));
                        AddModifier(new LabeledValue<int>(TaggedModifierName, 2, "You are proficient in this skill.", true));
                    }
                }
                else
                {
                    //LabeledInt modifierToRemove = (LabeledInt)Modifiers.FirstOrDefault(x => x.Name == TaggedModifierName);
                    var modifierToRemove = Modifiers.FirstOrDefault(x => x.Name == TaggedModifierName);
                    if(modifierToRemove != null)
                    {
                        modifierToRemove.IsReadOnly = false;
                        RemoveModifier(modifierToRemove);
                    }
                }

                UpdateTotal();
            }
        }

        private SPECIAL _selectedModifier;
        public SPECIAL SelectedModifier
        {
            get => _selectedModifier;
            set
            {
                Update(ref _selectedModifier, value);
            }
        }

        public ObservableCollection<SPECIAL>? SkillModifiers { get; }
        //
    }
}
