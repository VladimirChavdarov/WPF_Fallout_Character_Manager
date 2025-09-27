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
        public ModIntSkill(string name, int value, ObservableCollection<SPECIAL> skillModifiers, bool isBaseValueReadOnly = false, string description = "") : base(name, value, isBaseValueReadOnly, description)
        {
            _isTagged = false;
            SkillModifiers = skillModifiers;
            //SelectedModifier = SkillModifiers.FirstOrDefault();
            SelectedModifier = SkillModifiers.LastOrDefault();
        }
        //

        // Data
        private bool _isTagged;
        public bool IsTagged
        {
            get => _isTagged;
            set
            {
                _isTagged = value;

                if (value)
                    AddModifier(new LabeledInt("Tagged", 2, "(PLEASE DON'T RENAME THIS MODIFIER) You are proficient in this skill."));
                else
                    RemoveModifier("Tagged");

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
