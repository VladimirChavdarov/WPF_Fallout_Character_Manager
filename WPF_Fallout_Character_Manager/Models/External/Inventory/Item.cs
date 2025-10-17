using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External.Inventory
{
    class Item : ModTypeBase
    {
        // constructors
        public Item(string name = "ItemName", int cost = 0, int amount = 0, float load = 0.0f, string description = "")
        {
            _name = new ModString("Name", name, false, description);
            _cost = new ModInt("Cost", cost);
            _amount = new ModInt("Amount", amount);
            _load = new ModFloat("Load", load);

            SubscribeToChildPropertyChanges();
        }
        //

        // members
        private ModString _name;
        public ModString Name
        {
            get => _name;
            set => Update(ref _name, value);
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

        public int TotalLoad => (int)(Amount.Total * Load.Total);
        public int TotalCost => (int)(Amount.Total * Cost.Total);
        public string NameAmount => $"({Amount.Total}) {Name.Total}";
        //

        // methods
        private void SubscribeToChildPropertyChanges()
        {
            // Subscribe to Amount, Cost, and Load changes
            Amount.PropertyChanged += Child_PropertyChanged;
            Cost.PropertyChanged += Child_PropertyChanged;
            Load.PropertyChanged += Child_PropertyChanged;
        }

        private void Child_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender == Amount && e.PropertyName == nameof(ModInt.Total))
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
        //
    }
}
