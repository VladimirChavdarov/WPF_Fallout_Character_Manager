using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels.ModifierSystem
{
    public class ModIntViewModel : ViewModelBase
    {
        // local variables
        private ModInt? _modInt;
        //

        public ModInt? ModInt
        {
            get { return _modInt; }
            set
            {
                _modInt = value;
                Update(ref _modInt, value);
            }
        }

        // Commands
        public RelayCommand AddModifierCommand { get; }
        public RelayCommand RemoveModifierCommand { get; }
        //

        //constructor
        public ModIntViewModel(ModInt modInt)
        {
            _modInt = modInt;
            AddModifierCommand = new RelayCommand(_ => ModInt.AddModifier(new LabeledInt()));
            RemoveModifierCommand = new RelayCommand(
                param =>
                {
                    if (param is LabeledInt labeledInt)
                    {
                        ModInt.RemoveModifier(labeledInt);
                    }
                });
        }
        //
    }
}
