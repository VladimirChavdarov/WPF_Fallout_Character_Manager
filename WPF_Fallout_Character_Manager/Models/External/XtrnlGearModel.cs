using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlGearModel : ModelBase
    {
        private static readonly string gearPath = "Resources/Spreadsheets/gear.csv";
        private static readonly string magazinesPath = "Resources/Spreadsheets/magazine.csv";

        // constructor
        public XtrnlGearModel()
        {
            GearItems = new ObservableCollection<Gear>();

            // gear
            var gearLines = File.ReadAllLines(gearPath);
            foreach (var line in gearLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                Gear gear = new Gear(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    description: parts[2],
                    load: (float)Int32.Parse(parts[3]),
                    loadEquippedOrFull: (float)Int32.Parse(parts[4])
                    );

                GearItems.Add(gear);
            }
            //

            // magazines
            var magazineLines = File.ReadAllLines(magazinesPath);
            foreach (var line in magazineLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                Gear gear = new Gear(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    description: parts[2],
                    load: (float)Int32.Parse(parts[3]),
                    loadEquippedOrFull: (float)Int32.Parse(parts[3])
                    );

                GearItems.Add(gear);
            }
            //
        }
        //

        // data
        public ObservableCollection<Gear> GearItems { get; set; }
        //
    }

    class Gear : Item
    {
        // constructor
        public Gear(string name = "NewGearItem", int cost = 0, string description = "", float load = 0.0f, float loadEquippedOrFull = 0.0f) : base(name, cost, 0, load, description)
        {
            LoadEquippedOrFull = new ModFloat("Load When Equipped/Full", loadEquippedOrFull);
            LoadUnequippedOrEmpty = new ModFloat("Load When Unequipped/Empty", load);
            CanBeEquippedOrFilled = false;
            if(load != loadEquippedOrFull)
            {
                CanBeEquippedOrFilled = true;
            }
            EquippedOrFull = false;
        }

        public Gear(Gear other) : base(other)
        {
            LoadEquippedOrFull = other.LoadEquippedOrFull.Clone();
            LoadUnequippedOrEmpty = other.LoadUnequippedOrEmpty.Clone();
            CanBeEquippedOrFilled = other.CanBeEquippedOrFilled;
            EquippedOrFull = other.EquippedOrFull;
        }

        public Gear(GearDTO dto)
        {
            FromDto(dto);
        }
        //

        // methods
        public Gear Clone() => new Gear(this);

        public override ItemDTO ToDto()
        {
            GearDTO result = new GearDTO();

            UpdateItemDTO(result);

            result.LoadEquippedOrFull = LoadEquippedOrFull.ToDto();
            result.LoadUnequippedOrEmpty = LoadUnequippedOrEmpty.ToDto();
            result.CanBeEquippedOrFilled = CanBeEquippedOrFilled;
            result.EquippedOrFull = EquippedOrFull;

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            var typedDto = Utils.EnsureDtoType<GearDTO>(dto);

            base.FromDto(dto, versionMismatch);

            LoadEquippedOrFull = new ModFloat(typedDto.LoadEquippedOrFull);
            LoadUnequippedOrEmpty = new ModFloat(typedDto.LoadUnequippedOrEmpty);
            CanBeEquippedOrFilled = typedDto.CanBeEquippedOrFilled;
            EquippedOrFull = typedDto.EquippedOrFull;
        }
        //

        // members
        private ModFloat _loadEquippedOrFull;
        public ModFloat LoadEquippedOrFull
        {
            get => _loadEquippedOrFull;
            set
            {
                Update(ref _loadEquippedOrFull, value);
                if(LoadUnequippedOrEmpty != null)
                {
                    Load.BaseValue = EquippedOrFull ? LoadEquippedOrFull.Total : LoadUnequippedOrEmpty.Total;
                }
            }
        }

        private ModFloat _loadUnequippedOrEmpty;
        public ModFloat LoadUnequippedOrEmpty
        {
            get => _loadUnequippedOrEmpty;
            set
            {
                Update(ref _loadUnequippedOrEmpty, value);
                if (LoadEquippedOrFull != null)
                {
                    Load.BaseValue = EquippedOrFull ? LoadEquippedOrFull.Total : LoadUnequippedOrEmpty.Total;
                }
            }
        }

        private bool _canBeEquippedOrFilled;
        public bool CanBeEquippedOrFilled
        {
            get => _canBeEquippedOrFilled;
            set
            {
                Update(ref _canBeEquippedOrFilled, value);
            }
        }

        private bool _equippedOrFull;
        public bool EquippedOrFull
        {
            get => _equippedOrFull;
            set
            {
                Update(ref _equippedOrFull, value);
                Load.BaseValue = EquippedOrFull ? LoadEquippedOrFull.Total : LoadUnequippedOrEmpty.Total;
            }
        }
        //

    }
}
