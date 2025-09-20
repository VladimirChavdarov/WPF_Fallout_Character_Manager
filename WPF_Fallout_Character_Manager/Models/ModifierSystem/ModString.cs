using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public sealed class ModString : ModTypeBase
    {
        // constructor
        public ModString(string name, string value, bool isBaseValueReadOnly = false)
        {
            _name = name;
            _baseValue = value;
            _isBaseValueReadOnly = isBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledString>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            UpdateTotal();
        }
        //

        //
        // TODO: Find someone and ask if this is too much redundant calls of UpdateTotal().
        // Maybe not actually. UpdateTotal() gets called when the size of the ObservableCollection changes
        // or when a LabeledString's value changes.
        protected void Modifiers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LabeledString mod in e.NewItems)
                {
                    mod.PropertyChanged += Modifiers_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (LabeledString mod in e.OldItems)
                {
                    mod.PropertyChanged -= Modifiers_PropertyChanged;
                }
            }
            UpdateTotal();
        }

        protected void Modifiers_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledString.Value))
            {
                UpdateTotal();
            }
        }
        //

        // helpers
        public void UpdateTotal()
        {
            string sum = BaseValue;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                sum += " " + Modifiers[i].Value;
            }
            Total = sum;
        }

        public void AddModifier(LabeledString newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }
        //

        public void RemoveModifier(LabeledString modifierToRemove)
        {
            Modifiers.Remove(modifierToRemove);
        }

        public void RemoveModifier(string modifierName)
        {
            LabeledString modifierToRemove = Modifiers?.FirstOrDefault(m => m.Name == modifierName);
            if (modifierToRemove != null)
                Modifiers.Remove(modifierToRemove);
            else
                throw new Exception($"Modifier with label '{modifierName}' not found.");
        }

        // Data
        protected string _total;
        public string Total
        {
            get => _total;
            // This shouldn't be set explicitly. It's always calculated via UpdateTotal().
            // NOTE: This conflicts with the TwoWay binding so I will comment it out for now.
            // When you display the total in a View field, MAKE IT READ-ONLY.
            private set => Update(ref _total, value);
        }

        protected string _baseValue;
        public string BaseValue
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


        public ObservableCollection<LabeledString>? Modifiers { get; }
        //
    }

    public class LabeledString : ModTypeBase
    {
        // constructor
        public LabeledString(string name = "NewStringValue", string value = "", string note = "")
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

        private string _value;
        public string Value
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
