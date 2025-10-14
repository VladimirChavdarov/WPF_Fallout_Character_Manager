using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

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
            _load = new ModFloat("Cost", load);
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
                Update(ref _cost, value);
                OnPropertyChanged(nameof(TotalCost)); // TODO: Make sure this actually works. If not, copy the approach with ModInt.Total
            }
        }

        private ModFloat _load;
        public ModFloat Load
        {
            get => _load;
            set
            {
                Update(ref _load, value);
                OnPropertyChanged(nameof(TotalLoad));
            }
        }

        private ModInt _amount;
        public ModInt Amount
        {
            get => _amount;
            set
            {
                Update(ref _amount, value);
                OnPropertyChanged(nameof(TotalLoad)); // TODO: Make sure this actually works. If not, copy the approach with ModInt.Total
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(NameAmount));
            }
        }

        public int TotalLoad => (int)(Amount.Total * Load.Total);
        public int TotalCost => (int)(Amount.Total * Cost.Total);
        public string NameAmount => ("(" + Amount.Total + ") " + Name.Total);
        //
    }
}
