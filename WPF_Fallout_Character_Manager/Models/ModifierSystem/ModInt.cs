using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public sealed class ModInt : ModTypeBase
    {
        // constructor
        public ModInt(string name, int value)
        {
            _name = name;
            _baseValue = value;
            Modifiers = new ObservableCollection<LabeledInt>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;
            UpdateTotal();
        }
        //

        //
        // TODO: Find someone and ask if this is too much redundant calls of UpdateTotal().
        // Maybe not actually. UpdateTotal() gets called when the size of the ObservableCollection changes
        // or when a LabeledInt's value changes.
        private void Modifiers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach(LabeledInt mod in  e.NewItems)
                {
                    mod.PropertyChanged += Modifiers_PropertyChanged;
                }
            }
            if(e.OldItems != null)
            {
                foreach(LabeledInt mod in e.OldItems)
                {
                    mod.PropertyChanged -= Modifiers_PropertyChanged;
                }
            }
            UpdateTotal();
        }

        private void Modifiers_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(LabeledInt.Value))
            {
                UpdateTotal();
            }
        }
        //

        // helpers
        public void UpdateTotal()
        {
            int sum = BaseValue;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += Modifiers[i].Value;
            }
            Total = sum;
        }

        public void AddModifier(LabeledInt newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }
        //

        public void RemoveModifier(LabeledInt modifierToRemove)
        {
            Modifiers.Remove(modifierToRemove);
        }

        // Data
        private int _total;
        public int Total
        {
            get => _total;
            // This shouldn't be set explicitly. It's always calculated via UpdateTotal().
            // NOTE: This conflicts with the TwoWay binding so I will comment it out for now.
            // When you display the total in a View field, MAKE IT READ-ONLY.
            /*private*/ set => Update(ref _total, value);
        }

        private int _baseValue;
        public int BaseValue
        {
            get => _baseValue;
            set
            {
                if (_baseValue != value)
                {
                    _baseValue = value;
                }
                //Update(ref _baseValue, value);
                UpdateTotal();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            // We don't need a setter for the name because the names of the parameters won't change dynamically. This value should only be set
            // on construction.
            //set => Update(ref _name, value);
        }

        public ObservableCollection<LabeledInt>? Modifiers { get; }
        //
    }

    public sealed class LabeledInt : ModTypeBase
    {
        // constructor
        public LabeledInt()
        {
            _name = "NewNumericValue";
            _value = 0;
            _note = "";
        }
        //

        // Data
        private string _name;
        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        private int _value;
        public int Value
        {
            get => _value;
            set => Update(ref _value, value);
        }

        private string _note;
        public string Note
        {
            get => _note;
            set => Update(ref _note, value);
        }
        //
    }
}
