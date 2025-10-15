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
    public class ModString : ModValue<string>
    {
        // constructor
        public ModString(string name = "NewModString", string value = "None", bool isbaseValueReadOnly = false, string hint = "No Hint")
            : base(name, value, isbaseValueReadOnly, hint) { }
        //

        // methods
        public override void UpdateTotal()
        {
            string sum = BaseValueObject.Value;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += " " + Modifiers[i].Value;
            }
            Total = sum;
        }
        //
    }

    public class LabeledString : LabeledValue<string>
    {
        // constructor
        public LabeledString(string name = "NewStringValue", string value = "", string note = "")
            : base(name, value, note) { }
        //
    }
}
