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
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public class ModString : ModValue<string>
    {
        // constructor
        public ModString() : base() { }

        public ModString(string name = "NewModString", string value = "None", bool isbaseValueReadOnly = false, string hint = "No Hint")
            : base(name, value, isbaseValueReadOnly, hint) { }

        public ModString(ModValueDTO<string> dto, ModString fallback = null)
            : base(dto, fallback) { }

        protected ModString(ModString other) : base(other) { }
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

        public override ModString Clone() => new ModString(this);
        //
    }

    public class LabeledString : LabeledValue<string>
    {
        // constructor
        public LabeledString(string name = "NewStringValue", string value = "", string note = "", bool isReadOnly = false)
            : base(name, value, note, isReadOnly) { }

        protected LabeledString(LabeledString other) : base(other) { }
        //

        public override LabeledString Clone() => new LabeledString(this);
    }
}
