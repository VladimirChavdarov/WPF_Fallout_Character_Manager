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
            _name = new LabeledString(name, "", description);
            _cost = cost;
            _amount = amount;
            _load = load;
        }
        //

        // members
        private LabeledString _name;
        public LabeledString Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                Update(ref _cost, value);
                OnPropertyChanged(nameof(TotalCost)); // TODO: Make sure this actually works. If not, copy the approach with ModInt.Total
            }
        }

        private float _load;
        public float Load
        {
            get => _load;
            set
            {
                Update(ref _load, value);
                OnPropertyChanged(nameof(TotalLoad));
            }
        }

        private int _amount;
        public int Amount
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

        public int TotalLoad => (int)(Amount * Load);
        public int TotalCost => (int)(Amount * Cost);
        public string NameAmount => ("(" + Amount + ") " + Name.Name);
        //
    }
}
