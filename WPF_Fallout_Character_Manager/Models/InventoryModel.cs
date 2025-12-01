using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    // This model should contain all of the player's items except Weapons, Armor, Power Armor and Ammo, because they have separate Models and ViewModels
    // to handle more complex logic.
    class InventoryModel : ModelBase
    {
        // constructor
        public InventoryModel()
        {
            AidItems = new ObservableCollection<Aid>();
            Explosives = new ObservableCollection<Explosive>();
            Nourishment = new ObservableCollection<Nourishment>();
            GearItems = new ObservableCollection<Gear>();
        }
        //

        // data
        public ObservableCollection<Aid> AidItems { get; set; }
        public ObservableCollection<Explosive> Explosives { get; set; }
        public ObservableCollection<Nourishment> Nourishment { get; set; }
        public ObservableCollection<Gear> GearItems { get; set; }
        //
    }
}
