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
    public class ModValueViewModel<T> : ViewModelBase where T : IComparable, IConvertible, IEquatable<T>
    {
        // local variables
        private ModValue<T>? _modValue;
        //

        public ModValue<T>? ModValue
        {
            get { return _modValue; }
            set
            {
                _modValue = value;
                Update(ref _modValue, value);
            }
        }

        // Commands
        public RelayCommand AddModifierCommand { get; }
        public RelayCommand RemoveModifierCommand { get; }
        //

        //constructor
        public ModValueViewModel(ModValue<T> modValue)
        {
            _modValue = modValue;
            AddModifierCommand = new RelayCommand(_ =>
            {
                LabeledValue<T> newModifier;

                if (typeof(T) == typeof(float))
                    newModifier = new LabeledValue<T>("NewFloatModifier", (T)(object)5.5f);
                else if (typeof(T) == typeof(int))
                    newModifier = new LabeledValue<T>("NewIntModifier", (T)(object)0);
                else if (typeof(T) == typeof(string))
                    newModifier = new LabeledValue<T>("NewStringModifier", (T)(object)string.Empty);
                else
                    newModifier = new LabeledValue<T>();

                ModValue.AddModifier(newModifier);
            });

            RemoveModifierCommand = new RelayCommand(
                param =>
                {
                    if (param is LabeledValue<T> labeledValue)
                    {
                        ModValue.RemoveModifier(labeledValue);
                    }
                });
        }
        //
    }
    public class ModIntViewModel : ModValueViewModel<int>
    {
        public ModIntViewModel(ModInt modInt) : base(modInt) { }
    }

    public class ModFloatViewModel : ModValueViewModel<float>
    {
        public ModFloatViewModel(ModFloat modFloat) : base(modFloat) { }
    }

    public class ModStringViewModel : ModValueViewModel<string>
    {
        public ModStringViewModel(ModString modString) : base(modString) { }
    }
}
