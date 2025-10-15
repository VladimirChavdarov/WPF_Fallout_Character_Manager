﻿using System;
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
    public class ModInt : ModValue<int>
    {
        // constructor
        public ModInt(string name = "NewModInt", int value = 0, bool isbaseValueReadOnly = false, string hint = "No Hint")
            : base(name, value, isbaseValueReadOnly, hint) { }
        //

        // methods
        public override void UpdateTotal()
        {
            int sum = BaseValueObject.Value;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += Modifiers[i].Value;
            }
            Total = sum;
        }
        //
    }

    public sealed class LabeledInt : LabeledValue<int>
    {
        // constructor
        public LabeledInt(string name = "NewIntegerValue", int value = 0, string note = "")
            : base(name, value, note) { }
        //
    }
}
