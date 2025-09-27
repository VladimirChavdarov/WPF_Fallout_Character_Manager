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
    public class ModInt : ModTypeBase
    {
        // constructor
        public ModInt(string name = "NewModInt", int value = 0, bool isBaseValueReadOnly = false, string description = "No Description")
        {
            _name = name;

            _baseValue = new LabeledInt(description, value);
            _baseValue.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = isBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledInt>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            UpdateTotal();
        }

        private void BaseValue_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(LabeledInt.Value))
            {
                UpdateTotal();
            }
        }

        //

        //
        // TODO: Find someone and ask if this is too much redundant calls of UpdateTotal().
        // Maybe not actually. UpdateTotal() gets called when the size of the ObservableCollection changes
        // or when a LabeledInt's value changes.
        protected void Modifiers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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

        protected void Modifiers_PropertyChanged(object? sender, PropertyChangedEventArgs e)
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
            int sum = BaseValue.Value;
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

        public void RemoveModifier(string modifierName)
        {
            LabeledInt modifierToRemove = Modifiers?.FirstOrDefault(m => m.Name == modifierName);
            if(modifierToRemove != null)
                Modifiers.Remove(modifierToRemove);
            else
                throw new Exception($"Modifier with label '{modifierName}' not found.");
        }

        // Data
        protected int _total;
        public int Total
        {
            get => _total;
            // This shouldn't be set explicitly. It's always calculated via UpdateTotal().
            // NOTE: This conflicts with the TwoWay binding so I will comment it out for now.
            // When you display the total in a View field, MAKE IT READ-ONLY.
            private set => Update(ref _total, value);
        }

        protected LabeledInt _baseValue;
        public LabeledInt BaseValue
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

        protected string _name;
        public string Name
        {
            get => _name;
            // We don't need a setter for the name because the names of the parameters won't change dynamically. This value should only be set
            // on construction.
            //set => Update(ref _name, value);
        }

        protected bool _isBaseValueReadOnly;
        public bool IsBaseValueReadOnly
        {
            get => _isBaseValueReadOnly;
        }


        public ObservableCollection<LabeledInt>? Modifiers { get; }
        //
    }

    public sealed class LabeledInt : ModTypeBase
    {
        // constructor
        public LabeledInt(string name = "NewNumericValue", int value = 0, string note = "")
        {
            _name = name;
            _value = value;
            _note = note;
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
