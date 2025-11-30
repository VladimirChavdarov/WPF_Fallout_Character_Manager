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
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public class ModValue<T> : ModTypeBase where T : IComparable, IConvertible, IEquatable<T>
    {
        // constructor
        public ModValue(string name = "NewModValue", T value = default, bool isBaseValueReadOnly = false, string hint = "No Hint")
        {
            _baseValueObject = new LabeledValue<T>(name, value, hint);
            _baseValueObject.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = isBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledValue<T>>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            UpdateTotal();
        }
        //

        private void BaseValue_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledValue<T>.Value))
            {
                UpdateTotal();
            }
        }

        //
        // TODO: Find someone and ask if this is too much redundant calls of UpdateTotal().
        // Maybe not actually. UpdateTotal() gets called when the size of the ObservableCollection changes
        // or when a LabeledValue's value changes.
        protected void Modifiers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LabeledValue<T> mod in e.NewItems)
                {
                    mod.PropertyChanged += Modifiers_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (LabeledValue<T> mod in e.OldItems)
                {
                    mod.PropertyChanged -= Modifiers_PropertyChanged;
                }
            }
            UpdateTotal();
        }

        protected void Modifiers_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledValue<T>.Value))
            {
                UpdateTotal();
            }
        }
        //

        // helpers
        public virtual void UpdateTotal()
        {
            // override in inherited classes
        }

        public void AddModifier(LabeledValue<T> newModifier)
        {
            Modifiers.Add(newModifier);
        }

        public void RemoveModifier(int indexToRemove)
        {
            Modifiers.RemoveAt(indexToRemove);
        }
        //

        // This method will remove the modifier if it's not set to read-only.
        public void RemoveModifier(LabeledValue<T> modifierToRemove)
        {
            if(!modifierToRemove.IsReadOnly)
                Modifiers.Remove(modifierToRemove);

        }

        // This method will remove the modifier if it's not set to read-only.
        public void RemoveModifier(string modifierName)
        {
            LabeledValue<T> modifierToRemove = Modifiers?.FirstOrDefault(m => m.Name == modifierName);
            if (modifierToRemove != null)
            {
                if (!modifierToRemove.IsReadOnly)
                    Modifiers.Remove(modifierToRemove);
            }
            else
                throw new Exception($"Modifier with label '{modifierName}' not found.");
        }

        // Data
        protected T _total;
        public T Total
        {
            get => _total;
            // This shouldn't be set explicitly. It's always calculated via UpdateTotal().
            // NOTE: This conflicts with the TwoWay binding so I will comment it out for now.
            // When you display the total in a View field, MAKE IT READ-ONLY.
            protected set => Update(ref _total, value);
        }

        protected LabeledValue<T> _baseValueObject;
        public LabeledValue<T> BaseValueObject
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
                BaseValueObject.Name = value;
            }
        }
        public T BaseValue
        {
            get => _baseValueObject.Value;
            set
            {
                BaseValueObject.Value = value;
                //UpdateTotal();
            }
        }
        public string Note
        {
            get => _baseValueObject.Note;
            set
            {
                BaseValueObject.Note = value;
            }
        }
        //

        protected bool _isBaseValueReadOnly;
        public bool IsBaseValueReadOnly
        {
            get => _isBaseValueReadOnly;
        }


        public ObservableCollection<LabeledValue<T>>? Modifiers { get; }
        //
    }

    public class LabeledValue<T> : ModTypeBase
    {
        // constructor
        public LabeledValue(string name = "NewModdableValue", T value = default, string note = "", bool isReadOnly = false)
        {
            _name = name;
            _value = value;
            _note = note;
            _isReadOnly = isReadOnly;
        }
        //

        // Data
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit LabeledValue.Name when in read-only mode");
                Update(ref _name, value);
            }
        }

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit LabeledValue.Value when in read-only mode");
                Update(ref _value, value);
            }
        }

        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit LabeledValue.Note when in read-only mode");
                Update(ref _note, value);
            }
        }

        private bool _isReadOnly = false;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => Update(ref _isReadOnly, value);
        }
        //
    }
}
