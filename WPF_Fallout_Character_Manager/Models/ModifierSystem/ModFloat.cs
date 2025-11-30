using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public class ModFloat : ModValue<float>
    {
        // constructor
        public ModFloat(string name = "NewModFloat", float value = 0.0f, bool isbaseValueReadOnly = false, string hint = "No Hint")
            : base(name, value, isbaseValueReadOnly, hint) { }
        //

        // methods
        public override void UpdateTotal()
        {
            float sum = BaseValueObject.Value;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += Modifiers[i].Value;
            }
            Total = sum;
        }
        //
    }

    public sealed class LabeledFloat : LabeledValue<float>
    {
        // constructor
        public LabeledFloat(string name = "NewFloatValue", float value = 0.0f, string note = "", bool isReadOnly = false)
            : base(name, value, note, isReadOnly) { }
        //
    }
}
