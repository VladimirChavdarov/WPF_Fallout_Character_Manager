using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public class ModIntSkill : ModInt
    {
        // constructor
        public ModIntSkill(string name, int value, ObservableCollection<SPECIAL> skillModifiers, bool isBaseValueReadOnly = false, string hint = "") : base(name, value, isBaseValueReadOnly, hint)
        {
            _isTagged = false;
            SkillModifiers = skillModifiers;
            //SelectedModifier = SkillModifiers.FirstOrDefault();
            SelectedModifier = SkillModifiers.LastOrDefault();
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
                _isTagged = value;

                if (value)
                    AddModifier(new LabeledInt(TaggedModifierName, 2, "You are proficient in this skill.", true));
                else
                {
                    LabeledInt modifierToRemove = (LabeledInt)Modifiers.FirstOrDefault(x => x.Name == TaggedModifierName);
                    modifierToRemove.IsReadOnly = false;
                    RemoveModifier(modifierToRemove);
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
