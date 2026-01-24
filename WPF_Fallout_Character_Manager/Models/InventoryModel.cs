using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models
{
    // This model should contain all of the player's items except Weapons, Armor, Power Armor and Ammo, because they have separate Models and ViewModels
    // to handle more complex logic.
    class InventoryModel : ModelBase, ISerializable<InventoryModelDTO>
    {
        // constructor
        public InventoryModel()
        {
            CarryLoad = new ModFloat("Carry Load", 0.0f, true, "Your Carry Load is a measurement of how much your character can physically carry. It is a mixture of weight and space. Your maximum Carry Load is equal to your Strength Score x 10. If you are carrying more than your maximum Carry Load, you gain the encumbered condition. If you are carrying more than double your maximum Carry Load, you gain the heavily encumbered condition.");
            CurrentLoad = new ModFloat("Current Load", 0.0f, true, "Your Carry Load is a measurement of how much your character can physically carry. It is a mixture of weight and space. Your maximum Carry Load is equal to your Strength Score x 10. If you are carrying more than your maximum Carry Load, you gain the encumbered condition. If you are carrying more than double your maximum Carry Load, you gain the heavily encumbered condition.");
            Caps = new ModInt("Caps", 0, false, "");

            AidItems = new ObservableCollection<Aid>();
            Explosives = new ObservableCollection<Explosive>();
            Nourishment = new ObservableCollection<Nourishment>();
            GearItems = new ObservableCollection<Gear>();
            JunkItems = new ObservableCollection<Junk>();

            CarryLoad.PropertyChanged += (s, e) => OnPropertyChanged(nameof(IsOverencumbered));
            CurrentLoad.PropertyChanged += (s, e) => OnPropertyChanged(nameof(IsOverencumbered));
        }
        //

        // methods
        public InventoryModelDTO ToDto()
        {
            InventoryModelDTO result = new InventoryModelDTO();
            result.Caps = Caps.ToDto();
            result.CarryLoad = CarryLoad.ToDto();
            result.CurrentLoad = CurrentLoad.ToDto();

            foreach(Aid aid in AidItems)
            {
                var typedDto = Utils.EnsureDtoType<AidDTO>(aid.ToDto());
                result.AidItems.Add(typedDto);
            }
            foreach (Explosive explosive in Explosives)
            {
                var typedDto = Utils.EnsureDtoType<ExplosiveDTO>(explosive.ToDto());
                result.Explosives.Add(typedDto);
            }

            return result;
        }

        public void FromDto(InventoryModelDTO dto, bool versionMismatch = false)
        {
            Caps = new ModInt(dto.Caps);
            CarryLoad = new ModFloat(dto.CarryLoad);
            CurrentLoad = new ModFloat(dto.CurrentLoad);

            AidItems.Clear();
            foreach(AidDTO aidDto in dto.AidItems)
            {
                AidItems.Add(new Aid(aidDto));
            }
            foreach (ExplosiveDTO explosiveDto in dto.Explosives)
            {
                Explosives.Add(new Explosive(explosiveDto));
            }
        }
        //

        // data
        private ModInt _caps;
        public ModInt Caps
        {
            get => _caps;
            set => Update(ref  _caps, value);
        }

        private ModFloat _carryLoad;
        public ModFloat CarryLoad
        {
            get => _carryLoad;
            set
            {
                Update(ref _carryLoad, value);
            }
        }

        private ModFloat _currentLoad;
        public ModFloat CurrentLoad
        {
            get => _currentLoad;
            set
            {
                Update(ref _currentLoad, value);
            }
        }

        public bool IsOverencumbered => CurrentLoad.Total > CarryLoad.Total;

        public ObservableCollection<Aid> AidItems { get; set; }
        public ObservableCollection<Explosive> Explosives { get; set; }
        public ObservableCollection<Nourishment> Nourishment { get; set; }
        public ObservableCollection<Gear> GearItems { get; set; }
        public ObservableCollection<Junk> JunkItems { get; set; }
        //
    }
}
