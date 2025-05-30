using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public sealed class ModInt
    {
        public ModInt(int value)
        {
            BaseValue = value;
            Modifiers = new ObservableCollection<Modifier>();
            UpdateTotal();
        }

        public void UpdateTotal()
        {
            Total = BaseValue;
            for(int i = 0; i < Modifiers.Count; i++)
            {
                Total += Modifiers[i].Value;
            }
        }

        public void AddModifier(Modifier newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }

        public void RemoveModifier(Modifier modifierToRemove)
        {
            Modifiers.Remove(modifierToRemove);
        }

        public int Total { get; set; }
        public int BaseValue { get; set; }
        public ObservableCollection<Modifier>? Modifiers { get; set; }
    }
}
