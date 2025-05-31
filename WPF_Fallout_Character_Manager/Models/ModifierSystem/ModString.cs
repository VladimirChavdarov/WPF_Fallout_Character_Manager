using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public sealed class ModString
    {
        public ModString(string value)
        {
            BaseValue = value;
            Modifiers = new ObservableCollection<LabeledString>();
            UpdateTotal();
        }

        public void UpdateTotal()
        {
            Total = BaseValue;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Total += Modifiers[i].Value;
            }
        }

        public void AddModifier(LabeledString newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }

        public void RemoveModifier(LabeledString modifierToRemove)
        {
            Modifiers.Remove(modifierToRemove);
        }

        public string Total { get; set; }
        public string BaseValue { get; set; }
        public ObservableCollection<LabeledString>? Modifiers { get; set; }
    }

    public sealed class LabeledString
    {
        public LabeledString()
        {
            Name = "NewStringValue";
            Value = "";
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
