using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Accessibility;
using WPF_Fallout_Character_Manager.Models.External.Serialization;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlWeaponsModel : ModelBase, ISerializable<XtrnlWeaponsModelDTO>
    {
        // constructor
        public XtrnlWeaponsModel(XtrnlAmmoModel xtrnlAmmoModel)
        {
            WeaponTypes = new ObservableCollection<string>();
            Weapons = new ObservableCollection<Weapon>();
            WeaponProperties = new ObservableCollection<WeaponProperty>();
            WeaponUpgrades = new ObservableCollection<WeaponUpgrade>();

            // upload melee weapon properties
            UploadMeleeWeaponsPropertiesFromCSV();
            //
            // upload ranged weapon properties
            UploadRangedWeaponsPropertiesFromCSV();
            //

            // upload melee weapon upgrades
            UploadMeleeWeaponsUpgradesFromCSV();
            //
            // upload ranged weapon upgrades
            UploadRangedWeaponsUpgradesFromCSV();
            //
            //

            // upload melee weapons
            UploadMeleeWeaponsFromCSV();
            //
            // upload ranged weapons
            UploadRangedWeaponsFromCSV();
            //

            // add weapon types
            DetermineWeaponTypes();
            //
        }
        //

        // methods
        private void UploadMeleeWeaponsPropertiesFromCSV()
        {
            var weaponPropLines = File.ReadAllLines("Resources/Spreadsheets/melee_weapons_properties.csv");
            foreach (var line in weaponPropLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                WeaponProperty property = new WeaponProperty(
                    WeaponType.Melee,
                    parts[0],
                    parts[1]
                    );
                property.SetId(Guid.NewGuid());
                WeaponProperties.Add(property);
            }
        }

        private void UploadRangedWeaponsPropertiesFromCSV()
        {
            var weaponPropLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons_properties.csv");
            foreach (var lines in weaponPropLines.Skip(1))
            {
                var parts = lines.Split(";");
                if (parts.Length < 2)
                    continue;

                WeaponProperty property = new WeaponProperty(
                    weaponType: WeaponType.Ranged,
                    name: parts[0],
                    value: parts[1]
                    );
                property.SetId(Guid.NewGuid());
                WeaponProperties.Add(property);
            }
        }

        private void UploadMeleeWeaponsUpgradesFromCSV()
        {
            var weaponUpgLines = File.ReadAllLines("Resources/Spreadsheets/melee_weapons_upgrades.csv");
            foreach (var line in weaponUpgLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                WeaponUpgrade upgrade = new WeaponUpgrade(
                    weaponType: WeaponType.Melee,
                    name: parts[0],
                    costMultiplier: parts[1],
                    timeToEquip: "5 minutes.",
                    slotCost: Int32.Parse(parts[2]),
                    value: parts[3],
                    equipRequirement: "Any melee weapon."
                    );
                upgrade.SetId(Guid.NewGuid());
                WeaponUpgrades.Add(upgrade);
            }
        }

        private void UploadRangedWeaponsUpgradesFromCSV()
        {
            var weaponUpgLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons_upgrades.csv");
            foreach (var line in weaponUpgLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                WeaponUpgrade upgrade = new WeaponUpgrade(
                    weaponType: WeaponType.Ranged,
                    name: parts[0],
                    costMultiplier: parts[1],
                    timeToEquip: parts[2],
                    slotCost: Int32.Parse(parts[3]),
                    value: parts[4],
                    equipRequirement: parts[5]
                    );
                upgrade.SetId(Guid.NewGuid());
                WeaponUpgrades.Add(upgrade);
            }
        }

        private void UploadMeleeWeaponsFromCSV()
        {
            var weaponLines = File.ReadAllLines("Resources/Spreadsheets/melee_weapons.csv");
            foreach (var line in weaponLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 9)
                    continue;

                string[] critField = parts[5].Split(",");
                foreach (string critValue in critField)
                    critValue.Trim();

                Weapon weapon = new Weapon(
                    weaponType: WeaponType.Melee,
                    name: parts[0],
                    type: parts[1],
                    cost: Int32.Parse(parts[2]),
                    ap: Int32.Parse(parts[3]),
                    damage: new ModString("Damage", parts[4]),
                    rangeMultiplier: "x0/x0",
                    critChance: Int32.Parse(critField[0]),
                    critDamage: critField[1],
                    ammoCapacity: 0,
                    load: Utils.FloatFromString(parts[7]),
                    strRequirement: Int32.Parse(parts[8]),
                    decay: 0,
                    availableUpgradeSlots: 1
                    );

                // melee properties
                string[] properties = parts[6].Split(".");
                foreach (string property in properties.SkipLast(1))
                {
                    string trimmedProperty = property.Trim();
                    if (trimmedProperty.Contains("Thrown"))
                    {
                        // update rangeMultiplier
                        string thrown = trimmedProperty.Replace("Thrown ", ""); // remove the word, leave only the values.
                        thrown = thrown.Replace(".", ""); // remove dot at the end.
                        weapon.RangeMultiplier.BaseValue = thrown;
                    }

                    if (trimmedProperty.Contains("Ammo"))
                    {
                        string ammoProperty = trimmedProperty.Replace("Ammo: ", "");
                        ProcessAmmo(weapon, ammoProperty);
                    }

                    if (trimmedProperty.Contains("Depleted"))
                    {
                        // TODO: When we add this to the ModString, we also need to make sure this modifier is always last for aesthetic purposes.
                        // I still haven't added sorting of modifiers.
                        string depleted = trimmedProperty.Replace("Depleted: ", "");
                        weapon.Damage.AddModifier(new LabeledString("Depleted Damage", "| Depleted: " + depleted + "|", "Use this damage if the weapon has no ammo left."));
                    }

                    // TODO: complete this after adding the Weapon Properties first.
                    WeaponProperty newProperty = WeaponProperties.FirstOrDefault(x => trimmedProperty.Contains(x.Name) && x.WeaponType == WeaponType.Melee);
                    if (newProperty == null)
                        throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                    weapon.Properties.Add(newProperty);
                }
                //

                // Note
                weapon.ConstructNote();
                //

                Weapons.Add(weapon);
            }
        }

        private void UploadRangedWeaponsFromCSV()
        {
            var weaponLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons.csv");
            foreach (var line in weaponLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 11)
                    continue;

                string[] rangeField = parts[5].Split("/");
                for (int i = 0; i < rangeField.Length; i++)
                    rangeField[i] = rangeField[i].Trim();

                string[] critField = parts[6].Split(",");
                for (int i = 0; i < critField.Length; i++)
                    critField[i] = critField[i].Trim();

                Weapon weapon = new Weapon(
                    weaponType: WeaponType.Ranged,
                    name: parts[0],
                    type: parts[1],
                    cost: Int32.Parse(parts[2]),
                    ap: Int32.Parse(parts[3]),
                    damage: new ModString("Damage", parts[4]),
                    rangeMultiplier: parts[5],
                    critChance: Int32.Parse(critField[0]),
                    critDamage: critField[1],
                    ammoCapacity: 0,
                    ammoPerAttack: 0,
                    numberOfAttacks: 0,
                    load: Utils.FloatFromString(parts[9]),
                    strRequirement: Int32.Parse(parts[10]),
                    decay: 0,
                    availableUpgradeSlots: 6
                    );

                // set ammo type, capacity, and ammo per attack
                ProcessAmmo(weapon, parts[7]);

                // properties
                string[] properties = parts[8].Split(".");
                foreach (string property in properties.SkipLast(1))
                {
                    string trimmedProperty = property.Trim();

                    WeaponProperty newProperty = WeaponProperties.FirstOrDefault(x => trimmedProperty == x.Name && x.WeaponType == WeaponType.Ranged);
                    if(newProperty == null)
                    {
                        newProperty = new WeaponProperty(WeaponType.Ranged, trimmedProperty, "This specifies another property the weapon has.");
                        newProperty.SetId(Guid.NewGuid());
                        WeaponProperties.Add(newProperty);
                    }
                    weapon.Properties.Add(newProperty);
                }
                //

                // Note
                weapon.ConstructNote();
                //

                Weapons.Add(weapon);
            }
        }

        private void DetermineWeaponTypes()
        {
            WeaponTypes.Clear();
            foreach (Weapon w in Weapons)
            {
                if (!WeaponTypes.Contains(w.Type))
                {
                    WeaponTypes.Add(w.Type);
                }
            }
        }

        private void ProcessAmmo(Weapon weapon, string ammoStringUnprocessed)
        {
            // process string
            string[] ammoField = ammoStringUnprocessed.Split(",");
            for (int i = 0; i < ammoField.Length; i++)
                ammoField[i] = ammoField[i].Trim();

            // ammo type
            Ammo ammoToAdd = XtrnlAmmoModel.Ammos.FirstOrDefault(x => x.Type.Contains(ammoField[0], StringComparison.InvariantCultureIgnoreCase));
            if (ammoToAdd == null)
            {
                // add the unusual ammo types to the master list. We do this because of the junk jet and solar scorcher.
                ammoToAdd = new Ammo(ammoField[0], ammoField[0], 0, 0, 0.0f);
                XtrnlAmmoModel.Ammos.Add(ammoToAdd);
                XtrnlAmmoModel.AmmoTypes.Add(ammoField[0]);
            }
            weapon.CompatibleAmmos.Add(ammoToAdd);
            weapon.AmmoType = ammoToAdd.Type;
            if (weapon.CompatibleAmmos.FirstOrDefault() == null)
                throw new Exception($"Weapon's ammo is null. Ammo name: {ammoField[0]}");

            // ammo capacity
            ammoField[1] = ammoField[1].Replace(" rounds", "");
            ammoField[1] = ammoField[1].Replace(" round", "");
            weapon.AmmoCapacity.BaseValue = Int32.Parse(ammoField[1]);

            // ammo per attack. We do this for the energy weapons which have x attacks per energy cell and for the minigun which expends 10 rounds per attack.
            ammoField[2] = ammoField[2].Replace(" attacks per ammo", "");
            weapon.AmmoPerAttack.BaseValue = 1.0f / Utils.FloatFromString(ammoField[2]);
            weapon.NumberOfAttacks.BaseValue = (int)(weapon.AmmoCapacity.BaseValue / weapon.AmmoPerAttack.BaseValue);
            weapon.InitializeBulletSlots();
        }

        public XtrnlWeaponsModelDTO ToDto()
        {
            XtrnlWeaponsModelDTO result = new XtrnlWeaponsModelDTO();
            foreach(WeaponProperty property in WeaponProperties)
            {
                result.Properties.Add(property);
            }
            foreach(WeaponUpgrade upgrade in WeaponUpgrades)
            {
                result.Upgrades.Add(upgrade);
            }
            foreach(Weapon weapon in Weapons)
            {
                if(weapon.ToDto() is not WeaponDTO wDto)
                    throw new InvalidOperationException("Expected WeaponDTO");

                result.Weapons.Add(wDto);
            }

            return result;
        }

        public void FromDto(XtrnlWeaponsModelDTO dto, bool versionMismatch = false)
        {
            Weapons.Clear();
            WeaponProperties.Clear();
            WeaponUpgrades.Clear();

            foreach (WeaponProperty property in dto.Properties)
            {
                WeaponProperties.Add(property);
            }
            foreach (WeaponUpgrade upgrade in dto.Upgrades)
            {
                WeaponUpgrades.Add(upgrade);
            }
            foreach(WeaponDTO weaponDto in dto.Weapons)
            {
                Weapons.Add(new Weapon(weaponDto));
            }

            DetermineWeaponTypes();
        }
        //

        // data
        public static ObservableCollection<string> WeaponTypes { get; set; }
        public static ObservableCollection<Weapon> Weapons { get; set; }
        public static ObservableCollection<WeaponProperty> WeaponProperties { get; set; }
        public static ObservableCollection<WeaponUpgrade> WeaponUpgrades { get; set; }
        //
    }
    enum WeaponType
    {
        Melee,
        Ranged
    }

    class Weapon : Item
    {
        // constructor

        // the main constructor is usually used when filling the external Models from the spreadsheets.
        public Weapon(
            WeaponType weaponType = WeaponType.Melee,
            string name = "NewWeapon",
            string description = "",
            string type = "NoType",
            int cost = 0,
            int ap = 0,
            ModString damage = null,
            string rangeMultiplier = "x0/x0",
            int critChance = 20,
            string critDamage = "None",
            string ammoType = "None",
            int ammoCapacity = 1,
            float ammoPerAttack = 1.0f,
            int numberOfAttacks = 0,
            float load = 0.0f,
            int strRequirement = 0,
            int decay = 0,
            int availableUpgradeSlots = 0
            )
        {
            Properties = new ObservableCollection<WeaponProperty>();
            Upgrades = new ObservableCollection<WeaponUpgrade>();
            CompatibleAmmos = new ObservableCollection<Ammo>();
            BulletSlots = new ObservableCollection<TypeWrap<bool>>();

            WeaponType = weaponType;
            Name = new ModString("Name", name, false, description);
            Type = type;
            Cost = new ModInt("Cost", cost);
            AP = new ModInt("AP", ap);
            ToHit = new ModInt("To Hit", 0);
            if (damage != null)
                Damage = damage;
            else
                Damage = new ModString("Damage", "None");
            RangeMultiplier = new ModString("Range", rangeMultiplier);
            CritChance = new ModInt("Crit Chance", critChance);
            CritDamage = new ModString("Crit Damage", critDamage);
            AmmoType = ammoType;
            AmmoCapacity = new ModInt("Ammo Capacity", ammoCapacity);
            AmmoPerAttack = new ModFloat("Ammo per Attack", ammoPerAttack);
            NumberOfAttacks = new ModInt("Number of Attacks", numberOfAttacks);
            UsedAmmoFirepower = 0.0f;
            Load = new ModFloat("Load", load);
            StrRequirement = new ModInt("Strength Requirement", strRequirement);
            Decay = new ModInt("Decay", decay, false);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0);
            Equipped = false;

            SubscribeToPropertyChanged();
        }

        protected Weapon(Weapon other) : base(other)
        {
            Properties = new ObservableCollection<WeaponProperty>(other.Properties);
            Upgrades = new ObservableCollection<WeaponUpgrade>(other.Upgrades);
            BulletSlots = new ObservableCollection<TypeWrap<bool>>(); // we assume a new weapon always comes with an empty magazine
            CompatibleAmmos = new ObservableCollection<Ammo>();

            WeaponType = other.WeaponType;
            Type = other.Type;
            AP = other.AP.Clone();
            ToHit = other.ToHit.Clone();
            if (other.Damage != null)
                Damage = other.Damage.Clone();
            else
                Damage = new ModString("Damage", "None");
            RangeMultiplier = other.RangeMultiplier.Clone();
            CritChance = other.CritChance.Clone();
            CritDamage = other.CritDamage.Clone();
            AmmoType = other.AmmoType;
            AmmoCapacity = other.AmmoCapacity.Clone();
            AmmoPerAttack = other.AmmoPerAttack.Clone();
            NumberOfAttacks = other.NumberOfAttacks.Clone();
            UsedAmmoFirepower = other.UsedAmmoFirepower;
            StrRequirement = other.StrRequirement.Clone();
            Decay = other.Decay.Clone();
            AvailableUpgradeSlots = other.AvailableUpgradeSlots.Clone();
            TakenUpgradeSlots = other.TakenUpgradeSlots.Clone();
            Equipped = other.Equipped;

            SubscribeToPropertyChanged();

            InitializeBulletSlots();
            ScaleWeaponWithDecay();
        }

        public Weapon(WeaponDTO dto)
        {
            Properties = new ObservableCollection<WeaponProperty>();
            Upgrades = new ObservableCollection<WeaponUpgrade>();
            BulletSlots = new ObservableCollection<TypeWrap<bool>>(); // we assume a new weapon always comes with an empty magazine
            CompatibleAmmos = new ObservableCollection<Ammo>();

            FromDto(dto);
        }
        //

        // members
        private string _type; // see if this won't need to be turned to a ModString later
        public string Type
        {
            get => _type;
            set => Update(ref _type, value);
        }

        private ModInt _ap;
        public ModInt AP
        {
            get => _ap;
            set => Update(ref _ap, value);
        }

        private ModInt _toHit;
        public ModInt ToHit
        {
            get => _toHit;
            set => Update(ref _toHit, value);
        }

        private ModString _damage;
        public ModString Damage
        {
            get => _damage;
            set => Update(ref _damage, value);
        }

        private ModString _rangeMultiplier;
        public ModString RangeMultiplier
        {
            get => _rangeMultiplier;
            set => Update(ref _rangeMultiplier, value);
        }

        private ModInt _critChance;
        public ModInt CritChance
        {
            get => _critChance;
            set => Update(ref _critChance, value);
        }

        private ModString _critDamage;
        public ModString CritDamage
        {
            get => _critDamage;
            set => Update(ref _critDamage, value);
        }

        private string _ammoType;
        public string AmmoType
        {
            get => _ammoType;
            set => Update(ref _ammoType, value);
        }

        private ModInt _ammoCapacity;
        public ModInt AmmoCapacity
        {
            get => _ammoCapacity;
            set => Update(ref _ammoCapacity, value);
        }

        private ModFloat _ammoPerAttack;
        public ModFloat AmmoPerAttack
        {
            get => _ammoPerAttack;
            set => Update(ref _ammoPerAttack, value);
        }

        private ModInt _numberOfAttacks;
        public ModInt NumberOfAttacks
        {
            get => _numberOfAttacks;
            set => Update(ref _numberOfAttacks, value);
        }

        private float _usedAmmoFirepower; // accumulates ammoPerAttack and resets when it reaches >= 1.
        public float UsedAmmoFirepower
        {
            get => _usedAmmoFirepower;
            set => Update(ref _usedAmmoFirepower, value);
        }

        private ModInt _strRequirement;
        public ModInt StrRequirement
        {
            get => _strRequirement;
            set => Update(ref _strRequirement, value);
        }

        private ModInt _decay;
        public ModInt Decay
        {
            get => _decay;
            set => Update(ref _decay, value);
        }

        private WeaponType _weaponType;
        public WeaponType WeaponType
        {
            get => _weaponType;
            set => Update(ref _weaponType, value);
        }

        private ModInt _availableUpgradeSlots;
        public ModInt AvailableUpgradeSlots
        {
            get => _availableUpgradeSlots;
            set => Update(ref _availableUpgradeSlots, value);
        }

        private ModInt takenUpgradeSlots;
        public ModInt TakenUpgradeSlots
        {
            get => takenUpgradeSlots;
            set => Update(ref takenUpgradeSlots, value);
        }

        private bool _equipped;
        public bool Equipped
        {
            get => _equipped;
            set
            {
                Update(ref _equipped, value);
                OnPropertyChanged(nameof(EquippedNameAmount));
            }
        }

        public string EquippedNameAmount
        {
            get
            {
                if(Equipped)
                {
                    return "[X] " + NameAmount;
                }
                else
                {
                    return "[  ] " + NameAmount;
                }
            }
        }

        public override string NameAmount
        {
            get
            {
                string result = base.NameAmount;
                foreach(WeaponUpgrade upgrade in Upgrades)
                {
                    result += " (" + upgrade.Name + ")";
                }

                return result;
            }
        }

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<WeaponProperty> Properties { get; set; }
        public ObservableCollection<WeaponUpgrade> Upgrades { get; set; }
        public ObservableCollection<Ammo> CompatibleAmmos { get; set; }

        public ObservableCollection<TypeWrap<bool>> BulletSlots { get; set; }
        //

        // methods
        public Weapon Clone() => new Weapon(this);

        public void SubscribeToPropertyChanged()
        {
            Name.PropertyChanged -= NoteComponents_PropertyChanged;
            Name.PropertyChanged += NoteComponents_PropertyChanged;
            Properties.CollectionChanged -= Properties_CollectionChanged;
            Properties.CollectionChanged += Properties_CollectionChanged;
            _decay.PropertyChanged -= Decay_PropertyChanged;
            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged -= Upgrades_CollectionChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged -= TakenUpgradeSlots_PropertyChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }

        public override void ConstructNote()
        {
            string note = "Type: " + Type;
            note += "\nAmmo: " + AmmoType;
            note += "\nProperties: ";
            foreach (WeaponProperty property in Properties)
            {
                note += property.Name + ". ";
            }
            Name.Note = note;
        }

        public void InitializeBulletSlots()
        {
            BulletSlots.Clear();
            for (int i = 0; i < NumberOfAttacks.BaseValue; i++)
            {
                BulletSlots.Add(false);
            }
        }

        public void AddProperty(object obj)
        {
            if(obj is  WeaponProperty propertyToAdd)
            {
                Properties.Add(propertyToAdd);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public void RemoveProperty(object obj)
        {
            if (obj is WeaponProperty propertyToRemove)
            {
                Properties.Remove(propertyToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public void AddUpgrade(object obj)
        {
            if (obj is WeaponUpgrade upgradeToAdd)
            {
                if(TakenUpgradeSlots.Total + upgradeToAdd.SlotCost > AvailableUpgradeSlots.Total)
                {
                    return;
                }

                Upgrades.Add(upgradeToAdd);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public void RemoveUpgrade(object obj)
        {
            if (obj is WeaponUpgrade upgradeToRemove)
            {
                Upgrades.Remove(upgradeToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        // a field that is relevant to the information in displayed in the note has been changed.
        private void NoteComponents_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ConstructNote();
        }
        private void Properties_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConstructNote();
        }

        private void Decay_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ScaleWeaponWithDecay();
        }

        private void ScaleWeaponWithDecay()
        {
            int decay = Decay.Total;

            ApplyDecay(ToHit, decay, -decay);
            ApplyDecay(Damage, decay, "-" + decay.ToString());
            float costReduction =  -Cost.BaseValue * (float)(decay / 10.0f);
            ApplyDecay(Cost, decay, (int)costReduction);
        }

        private void Upgrades_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TakenUpgradeSlots.BaseValue = 0;
            foreach (WeaponUpgrade upgrade in Upgrades)
            {
                TakenUpgradeSlots.BaseValue += upgrade.SlotCost;
            }

            OnPropertyChanged(nameof(NameAmount));
            OnPropertyChanged(nameof(EquippedNameAmount));
        }

        private void TakenUpgradeSlots_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(UpgradeSlotVisualization));
        }

        public override ItemDTO ToDto()
        {
            WeaponDTO result = new WeaponDTO();

            result.Name = Name.ToDto();
            result.Cost = Cost.ToDto();
            result.Amount = Amount.ToDto();
            result.Load = Load.ToDto();

            result.Type = Type;
            result.AP = AP.ToDto();
            result.ToHit = ToHit.ToDto();
            result.Damage = Damage.ToDto();
            result.RangeMultiplier = RangeMultiplier.ToDto();
            result.CritChance = CritChance.ToDto();
            result.CritDamage = CritDamage.ToDto();
            result.AmmoType = AmmoType;
            result.AmmoCapacity = AmmoCapacity.ToDto();
            result.AmmoPerAttack = AmmoPerAttack.ToDto();
            result.NumberOfAttacks = NumberOfAttacks.ToDto();
            result.UsedAmmoFirepower = UsedAmmoFirepower;
            result.StrRequirement = StrRequirement.ToDto();
            result.Decay = Decay.ToDto();
            result.WeaponType = WeaponType;
            result.AvailableUpgradeSlots = AvailableUpgradeSlots.ToDto();
            result.TakenUpgradeSlots = TakenUpgradeSlots.ToDto();
            result.Equipped = Equipped;
            
            foreach(var property in Properties)
            {
                result.PropertyIds.Add(property.Id);
            }
            foreach(var upgrade in Upgrades)
            {
                result.UpgradeIds.Add(upgrade.Id);
            }

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            if (dto is not WeaponDTO wDto)
                throw new InvalidOperationException("Expected WeaponDTO");

            base.FromDto(dto);

            Type = wDto.Type;
            AP = new ModInt(wDto.AP);
            ToHit = new ModInt(wDto.ToHit);
            Damage = new ModString(wDto.Damage);
            RangeMultiplier = new ModString(wDto.RangeMultiplier);
            CritChance = new ModInt(wDto.CritChance);
            CritDamage = new ModString(wDto.CritDamage);
            AmmoType = wDto.AmmoType;
            AmmoCapacity = new ModInt(wDto.AmmoCapacity);
            AmmoPerAttack = new ModFloat(wDto.AmmoPerAttack);
            NumberOfAttacks = new ModInt(wDto.NumberOfAttacks);
            UsedAmmoFirepower = wDto.UsedAmmoFirepower;
            StrRequirement = new ModInt(wDto.StrRequirement);
            Decay = new ModInt(wDto.Decay);
            WeaponType = wDto.WeaponType;
            AvailableUpgradeSlots = new ModInt(wDto.AvailableUpgradeSlots);
            TakenUpgradeSlots = new ModInt(wDto.TakenUpgradeSlots);
            Equipped = wDto.Equipped;

            Properties.Clear();
            foreach(Guid id in wDto.PropertyIds)
            {
                WeaponProperty property = XtrnlWeaponsModel.WeaponProperties.FirstOrDefault(x => x.Id == id);
                if(property != null)
                {
                    Properties.Add(property);
                }
            }

            Upgrades.Clear();
            foreach (Guid id in wDto.UpgradeIds)
            {
                WeaponUpgrade upgrade = XtrnlWeaponsModel.WeaponUpgrades.FirstOrDefault(x => x.Id == id);
                if (upgrade != null)
                {
                    Upgrades.Add(upgrade);
                }
            }

            SubscribeToPropertyChanged();

            InitializeBulletSlots();
            ScaleWeaponWithDecay();
        }
        //
    }


    class WeaponProperty : LabeledString
    {
        // constructor
        public WeaponProperty(WeaponType weaponType, string name = "NewProperty", string value = "") : base(name, value, value)
        {
            //Id = Guid.NewGuid();
            _weaponType = weaponType;
        }
        //

        // members
        public Guid Id { get; set; } // NOTE: This has a public setter only because of serialization. DON'T SET MANUALLY!

        private WeaponType _weaponType;
        public WeaponType WeaponType
        {
            get => _weaponType;
            set => Update(ref _weaponType, value);
        }
        //

        // methods
        public void SetId(Guid id)
        {
            Id = id;
        }
        //
    }

    class WeaponUpgrade : LabeledString
    {
        // constructor
        public WeaponUpgrade(WeaponType weaponType, string name="NewUpgrade", string costMultiplier="x1.0", string timeToEquip="", int slotCost = 0, string value="", string equipRequirement="")
        {
            //Id = Guid.NewGuid();
            WeaponType= weaponType;
            Name = name;
            CostMultiplier = costMultiplier;
            TimeToEquip = timeToEquip;
            SlotCost = slotCost;
            Value = value;
            Note = value;
            EquipRequirement = equipRequirement;

            Note += "\nRequirements: " + EquipRequirement;
        }
        //

        // members
        public Guid Id { get; set; } // NOTE: This has a public setter only because of serialization. DON'T SET MANUALLY!

        private WeaponType _weaponType;
        public WeaponType WeaponType
        {
            get => _weaponType;
            set => Update(ref _weaponType, value);
        }

        private int _slotCost;
        public int SlotCost
        {
            get => _slotCost;
            set => Update(ref _slotCost, value);
        }

        private string _costMultiplier;
        public string CostMultiplier
        {
            get => _costMultiplier;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit WeaponUpgrade.CostMultiplier when in read-only mode");
                Update(ref _costMultiplier, value);
            }
        }

        private string _timeToEquip;
        public string TimeToEquip
        {
            get => _timeToEquip;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit WeaponUpgrade.TimeToEquip when in read-only mode");
                Update(ref _timeToEquip, value);
            }
        }

        // TODO: I will just show this as text for now and let the player read instead of applying automatic filters.
        // The requirements cover too many unique cases. I may do it later but if you see this comment, it's not done.
        private string _equipRequirement;
        public string EquipRequirement
        {
            get => _equipRequirement;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit WeaponUpgrade.EquipRequirement when in read-only mode");
                Update(ref _equipRequirement, value);
            }
        }
        //

        // methods
        public void SetId(Guid id)
        {
            Id = id;
        }
        //
    }
}
