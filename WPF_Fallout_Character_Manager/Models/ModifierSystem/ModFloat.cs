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
    public class ModFloat : ModTypeBase
    {
        // constructor
        public ModFloat(string name = "NewModFloat", float value = 0, bool isBaseValueReadOnly = false, string hint = "No Hint")
        {
            _baseValueObject = new LabeledFloat(name, value, hint);
            _baseValueObject.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = isBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledFloat>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            UpdateTotal();
        }

        private void BaseValue_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledFloat.Value))
            {
                UpdateTotal();
            }
        }

        //

        //
        // TODO: Find someone and ask if this is too much redundant calls of UpdateTotal().
        // Maybe not actually. UpdateTotal() gets called when the size of the ObservableCollection changes
        // or when a LabeledFloat's value changes.
        protected void Modifiers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LabeledFloat mod in e.NewItems)
                {
                    mod.PropertyChanged += Modifiers_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (LabeledFloat mod in e.OldItems)
                {
                    mod.PropertyChanged -= Modifiers_PropertyChanged;
                }
            }
            UpdateTotal();
        }

        protected void Modifiers_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledFloat.Value))
            {
                UpdateTotal();
            }
        }
        //

        // helpers
        public void UpdateTotal()
        {
            float sum = BaseValueObject.Value;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += Modifiers[i].Value;
            }
            Total = sum;
        }

        public void AddModifier(LabeledFloat newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }
        //

        public void RemoveModifier(LabeledFloat modifierToRemove)
        {
            Modifiers.Remove(modifierToRemove);
        }

        public void RemoveModifier(string modifierName)
        {
            LabeledFloat modifierToRemove = Modifiers?.FirstOrDefault(m => m.Name == modifierName);
            if (modifierToRemove != null)
                Modifiers.Remove(modifierToRemove);
            else
                throw new Exception($"Modifier with label '{modifierName}' not found.");
        }

        // Data
        protected float _total;
        public float Total
        {
            get => _total;
            // This shouldn't be set explicitly. It's always calculated via UpdateTotal().
            // NOTE: This conflicts with the TwoWay binding so I will comment it out for now.
            // When you display the total in a View field, MAKE IT READ-ONLY.
            private set => Update(ref _total, value);
        }

        protected LabeledFloat _baseValueObject;
        public LabeledFloat BaseValueObject
        {
            get => _baseValueObject;
            set
            {
                if (_baseValueObject != value)
                {
                    _baseValueObject = value;
                }
                //Update(ref _baseValueObject, value);
                UpdateTotal();
            }
        }
        // QoL
        public string Name
        {
            get => _baseValueObject.Name;
            set
            {
                if (_baseValueObject.Name != value)
                {
                    _baseValueObject.Name = value;
                }
            }
        }
        public float BaseValue
        {
            get => _baseValueObject.Value;
            set
            {
                if (_baseValueObject.Value != value)
                {
                    _baseValueObject.Value = value;
                }
                UpdateTotal();
            }
        }
        public string Note
        {
            get => _baseValueObject.Note;
            set
            {
                if (_baseValueObject.Note != value)
                {
                    _baseValueObject.Note = value;
                }
            }
        }
        //

        protected bool _isBaseValueReadOnly;
        public bool IsBaseValueReadOnly
        {
            get => _isBaseValueReadOnly;
        }


        public ObservableCollection<LabeledFloat>? Modifiers { get; }
        //
    }

    public sealed class LabeledFloat : ModTypeBase
    {
        // constructor
        public LabeledFloat(string name = "NewNumericValue", float value = 0, string note = "")
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

        private float _value;
        public float Value
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
