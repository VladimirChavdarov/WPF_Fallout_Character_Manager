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
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public abstract class ModValue<T> : ModTypeBase, ICloneable where T : IComparable, IConvertible, IEquatable<T>
    {
        // constructor
        public ModValue(string name = "NewModValue", T value = default, bool isBaseValueReadOnly = false, string hint = "No Hint")
        {
            DefaultInit(name, value, isBaseValueReadOnly, hint);
        }

        protected ModValue(ModValueDTO<T> dto, ModValue<T> fallback = null)
        {
            if(dto == null)
            {
                if(fallback == null)
                {
                    DefaultInit("INVALID MOD VALUE", default, true, "If you encounter this, it means a value couldn't be loaded from the json and a fallback wasn't specified. If you encounter this and haven't touched the code yourself, contact the author of the app.");
                }
                else
                {
                    InitFromClone(fallback);
                }
            }
            else
            {
                InitFromDto(dto);
            }
        }

        protected ModValue(ModValue<T> other)
        {
            InitFromClone(other);
        }
        //

        private void DefaultInit(string name = "NewModValue", T value = default, bool isBaseValueReadOnly = false, string hint = "No Hint")
        {
            _baseValueObject = new LabeledValue<T>(name, value, hint);
            _baseValueObject.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = isBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledValue<T>>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            UpdateTotal();
        }

        private void InitFromDto(ModValueDTO<T> dto)
        {
            _baseValueObject = new LabeledValue<T>(dto.BaseValueObject.Name, dto.BaseValueObject.Value, dto.BaseValueObject.Note);
            _baseValueObject.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = dto.IsBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledValue<T>>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;
            Modifiers.Clear();
            foreach (LabeledValue<T> mod in dto.Modifiers)
            {
                AddModifier((LabeledValue<T>)mod.Clone());
            }

            UpdateTotal();
        }

        private void InitFromClone(ModValue<T> other)
        {
            _baseValueObject = new LabeledValue<T>(other.Name, other.BaseValue, other.Note);
            _baseValueObject.PropertyChanged += BaseValue_PropertyChanged;

            _isBaseValueReadOnly = other.IsBaseValueReadOnly;
            Modifiers = new ObservableCollection<LabeledValue<T>>();
            Modifiers.CollectionChanged += Modifiers_CollectionChanged;

            foreach (LabeledValue<T> mod in other.Modifiers)
            {
                AddModifier((LabeledValue<T>)mod.Clone());
            }

            UpdateTotal();
        }

        private void BaseValue_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LabeledValue<T>.Value))
            {
                UpdateTotal();
                OnPropertyChanged(nameof(BaseValue));
            }
            else if (e.PropertyName == nameof(LabeledValue<T>.Note))
            {
                OnPropertyChanged(nameof(Note));
            }
            else if (e.PropertyName == nameof(LabeledValue<T>.Name))
            {
                OnPropertyChanged(nameof(Name));
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
        public abstract void UpdateTotal();
        public abstract object Clone();


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

        // serialization
        public ModValueDTO<T> ToDto()
        {
            return new ModValueDTO<T>
            {
                BaseValueObject = _baseValueObject,
                IsBaseValueReadOnly = _isBaseValueReadOnly,
                Modifiers = Modifiers.ToList()
            };
        }
        //

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
        protected LabeledValue<T> BaseValueObject
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
            set => Update(ref _isBaseValueReadOnly, value);
        }


        public ObservableCollection<LabeledValue<T>>? Modifiers { get; private set; }
        //
    }

    public class LabeledValue<T> : ModTypeBase, ICloneable
    {
        // constructor
        public LabeledValue(string name = "NewModdableValue", T value = default, string note = "", bool isReadOnly = false)
        {
            Name = name;
            Value = value;
            Note = note;
            IsReadOnly = isReadOnly;
        }

        protected LabeledValue(LabeledValue<T> other)
        {
            Name = other.Name;
            Value = other.Value;
            Note = other.Note;
            IsReadOnly = other.IsReadOnly;
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

        // methods
        public virtual object Clone() => new LabeledValue<T>(this);
        //public object Clone()
        //{
        //    LabeledValue<T> clone = new LabeledValue<T>(this.Name, this.Value, this.Note, this.IsReadOnly);
           
        //    return clone;
        //}
        //
    }
}
