using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.Inventory
{
    //enum Category
    //{
    //    Weapon,
    //    Armor,
    //    Ammo,
    //    Aid,
    //    Explosives,
    //    Nourishment,
    //    Gear,
    //    Junk,
    //    None
    //}

    class Item : ModTypeBase, ISerializable<ItemDTO>
    {
        // constructors
        public Item(string name = "ItemName", int cost = 0, int amount = 0, float load = 0.0f, string description = "")
        {
            if (description == "")
                description = "No Hint";

            _name = new ModString("Name", name, false, description);
            _cost = new ModInt("Cost", cost);
            _amount = new ModInt("Amount", amount);
            _load = new ModFloat("Load", load);

            CanBeEdited = false;

            SubscribeToChildPropertyChanges();
        }

        public Item(Item other)
        {
            Name = other.Name.Clone();
            Cost = other.Cost.Clone();
            Amount = other.Amount.Clone();
            Load = other.Load.Clone();

            CanBeEdited = other.CanBeEdited;

            SubscribeToChildPropertyChanges();
        }

        public Item(ItemDTO dto)
        {
            FromDto(dto);

            SubscribeToChildPropertyChanges();
        }
        //

        // members
        private ModString _name;
        public ModString Name
        {
            get => _name;
            set
            {
                if (_name != null)
                    _name.PropertyChanged -= Child_PropertyChanged;

                Update(ref _name, value);

                if(value != null)
                    _name.PropertyChanged += Child_PropertyChanged;

                OnPropertyChanged(nameof(NameAmount));
                OnPropertyChanged(nameof(NameString));
            }
        }

        private ModInt _cost;
        public ModInt Cost
        {
            get => _cost;
            set
            {
                if (_cost != null)
                    _cost.PropertyChanged -= Child_PropertyChanged;

                Update(ref _cost, value);

                if (_cost != null)
                    _cost.PropertyChanged += Child_PropertyChanged;

                OnPropertyChanged(nameof(TotalCost));
            }
        }

        private ModFloat _load;
        public ModFloat Load
        {
            get => _load;
            set
            {
                if (_load != null)
                    _load.PropertyChanged -= Child_PropertyChanged;

                Update(ref _load, value);

                if (_load != null)
                    _load.PropertyChanged += Child_PropertyChanged;

                OnPropertyChanged(nameof(TotalLoad));
            }
        }

        // Instead of creating instances of items and storing them in data structures, I decided to create singular objects and modify their amount.
        // This makes armor and weapon handling a bit tricky when it comes to decay. If you have 10 Assault Rifles and want to decay one of them, you will
        // decay the whole stack. Because of that, weapons and armor is best kept in separate stacks of 1.
        private ModInt _amount;
        public ModInt Amount
        {
            get => _amount;
            set
            {
                if (_amount != null)
                    _amount.PropertyChanged -= Child_PropertyChanged;

                Update(ref _amount, value);

                if (_amount != null)
                    _amount.PropertyChanged += Child_PropertyChanged;

                OnPropertyChanged(nameof(TotalLoad));
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(NameAmount));
            }
        }

        private bool _canBeEdited;
        public virtual bool CanBeEdited
        {
            get => _canBeEdited;
            set => Update(ref _canBeEdited, value);
        }

        // by default, all items come from a spreadsheet. The ones that don't are the ones the user has created via the app.
        // this variable is used for the Catalogue items, specifically during its serialization.
        private bool _isFromSpreadsheet = true;
        public bool IsFromSpreadsheet
        {
            get => _isFromSpreadsheet;
            set => Update(ref _isFromSpreadsheet, value);
        }

        public int TotalLoad => (int)(Amount.Total * Load.Total);
        public int TotalCost => Amount.Total * Cost.Total;
        public virtual string NameAmount => $"({Amount.Total}) {Name.Total}";
        public string NameString => Name.Total;
        //

        // methods
        public virtual void ConstructNote()
        {

        }

        private void SubscribeToChildPropertyChanges()
        {
            Name.PropertyChanged += Child_PropertyChanged;
            Amount.PropertyChanged += Child_PropertyChanged;
            Cost.PropertyChanged += Child_PropertyChanged;
            Load.PropertyChanged += Child_PropertyChanged;
        }

        private void Child_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(sender == Name && e.PropertyName == nameof(ModInt.Total))
            {
                OnPropertyChanged(nameof(NameAmount));
                OnPropertyChanged(nameof(NameString));
            }
            else if (sender == Amount && e.PropertyName == nameof(ModInt.Total))
            {
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(TotalLoad));
                OnPropertyChanged(nameof(NameAmount));
            }
            else if (sender == Cost && e.PropertyName == nameof(ModInt.Total))
            {
                OnPropertyChanged(nameof(TotalCost));
            }
            else if (sender == Load && e.PropertyName == nameof(ModFloat.Total))
            {
                OnPropertyChanged(nameof(TotalLoad));
            }
        }

        protected void ApplyDecay<T>(ModValue<T> modValue, int decay, T newValue) where T : IComparable, IConvertible, IEquatable<T>
        {
            LabeledValue<T> decayModifier = modValue.Modifiers.FirstOrDefault(x => x.Name == "Decay");

            if (decay == 0 && decayModifier != null) // remove the modifier if the weapon is in pristine condition
            {
                decayModifier.IsReadOnly = false;
                modValue.RemoveModifier(decayModifier);
            }

            if (decay != 0 && decayModifier == null) // add the decay modifier if it doesn't exist
            {
                decayModifier = new LabeledValue<T>("Decay", default, "This modifier automatically updates as Decay level changes.", true);
                modValue.AddModifier(decayModifier);
            }

            if(decayModifier != null)
            {
                decayModifier.IsReadOnly = false;
                decayModifier.Value = newValue;
                decayModifier.IsReadOnly = true;
            }
        }

        public virtual ItemDTO ToDto()
        {
            return new ItemDTO
            {
                Name = Name.ToDto(),
                Cost = Cost.ToDto(),
                Amount = Amount.ToDto(),
                Load = Load.ToDto(),
                CanBeEdited = CanBeEdited
            };
        }

        public virtual void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            Name = new ModString(dto.Name);
            Cost = new ModInt(dto.Cost);
            Amount = new ModInt(dto.Amount);
            Load = new ModFloat(dto.Load);
            CanBeEdited = dto.CanBeEdited;

            OnPropertyChanged(nameof(TotalCost));
            OnPropertyChanged(nameof(TotalLoad));
            OnPropertyChanged(nameof(NameAmount));
            OnPropertyChanged(nameof(NameString));
        }
    }
}
