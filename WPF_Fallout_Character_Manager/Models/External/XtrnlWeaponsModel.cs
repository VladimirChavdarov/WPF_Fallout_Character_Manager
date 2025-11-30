using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Accessibility;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlWeaponsModel : ModelBase
    {
        // constructor
        public XtrnlWeaponsModel(XtrnlAmmoModel xtrnlAmmoModel/*, AmmoModel ammoModel*/)
        {
            Weapons = new ObservableCollection<Weapon>();
            WeaponProperties = new ObservableCollection<WeaponProperty>();
            WeaponUpgrades = new ObservableCollection<WeaponUpgrade>();

            // upload melee weapon properties
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
                WeaponProperties.Add(property);
            }
            //
            // upload ranged weapon properties
            weaponPropLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons_properties.csv");
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
                WeaponProperties.Add(property);
            }
            //

            // upload melee weapon upgrades
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
                WeaponUpgrades.Add(upgrade);
            }
            //
            // upload ranged weapon upgrades
            weaponUpgLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons_upgrades.csv");
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
                WeaponUpgrades.Add(upgrade);
            }
            //
            //

            // upload melee weapons
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
                    damage: new ModString("Damage", parts[4], true),
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
                        ProcessAmmo(weapon, ammoProperty, xtrnlAmmoModel);
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

                Weapons.Add(weapon);
            }
            //
            // upload ranged weapons
            weaponLines = File.ReadAllLines("Resources/Spreadsheets/ranged_weapons.csv");
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
                    damage: new ModString("Damage", parts[4], true),
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
                ProcessAmmo(weapon, parts[7], xtrnlAmmoModel);

                // properties
                string[] properties = parts[8].Split(".");
                foreach (string property in properties.SkipLast(1))
                {
                    string trimmedProperty = property.Trim();

                    WeaponProperty newProperty = WeaponProperties.FirstOrDefault(x => trimmedProperty.Contains(x.Name) && x.WeaponType == WeaponType.Ranged);
                    if (newProperty == null)
                        throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                    if(newProperty.Name != trimmedProperty)
                    {
                        // add a copy with the specific name taken from the weapons table.
                        // NOTE: I don't know how much I like the idea of some properties being copies, and others references.
                        // with the current setup, there will be duplicates. Take "Automatic" for example. A weapon will have a
                        // property "Automatic" (reference) and a property "Automatic: 2" (copy)
                        WeaponProperty propertyToAdd = newProperty.Clone;
                        propertyToAdd.Name = trimmedProperty;
                        weapon.Properties.Add(propertyToAdd);
                    }
                    // add a direct reference to the master list.
                    // NOTE: maybe put in an else.
                    weapon.Properties.Add(newProperty);
                }
                //

                Weapons.Add(weapon);
            }
            //
        }
        //

        // methods
        private void ProcessAmmo(Weapon weapon, string ammoStringUnprocessed, XtrnlAmmoModel xtrnlAmmoModel)
        {
            // process string
            string[] ammoField = ammoStringUnprocessed.Split(",");
            for (int i = 0; i < ammoField.Length; i++)
                ammoField[i] = ammoField[i].Trim();

            // ammo type
            Ammo ammoToAdd = xtrnlAmmoModel.Ammos.FirstOrDefault(x => x.Type.Contains(ammoField[0], StringComparison.InvariantCultureIgnoreCase));
            if (ammoToAdd == null)
            {
                // add the unusual ammo types to the master list. We do this because of the junk jet and solar scorcher.
                ammoToAdd = new Ammo(ammoField[0], ammoField[0], 0, 0, 0.0f);
                xtrnlAmmoModel.Ammos.Add(ammoToAdd);
                xtrnlAmmoModel.AmmoTypes.Add(ammoField[0]);
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
        //

        // data
        public ObservableCollection<Weapon> Weapons { get; set; }
        public ObservableCollection<WeaponProperty> WeaponProperties { get; set; }
        public ObservableCollection<WeaponUpgrade> WeaponUpgrades { get; set; }
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
            Name = new ModString("Name", name);
            Type = type;
            Cost = new ModInt("Cost", cost, true);
            AP = new ModInt("AP", ap, true);
            ToHit = new ModInt("To Hit", 0, true);
            if (damage != null)
                Damage = damage;
            else
                Damage = new ModString("Damage", "None", true);
            RangeMultiplier = new ModString("Range", rangeMultiplier, true);
            CritChance = new ModInt("Crit Chance", critChance, true);
            CritDamage = new ModString("Crit Damage", critDamage, true);
            AmmoType = ammoType;
            AmmoCapacity = new ModInt("Ammo Capacity", ammoCapacity, true);
            AmmoPerAttack = new ModFloat("Ammo per Attack", ammoPerAttack, true);
            NumberOfAttacks = new ModInt("Number of Attacks", numberOfAttacks, true);
            UsedAmmoFirepower = 0.0f;
            Load = new ModFloat("Load", load, true);
            StrRequirement = new ModInt("Strength Requirement", strRequirement, true);
            Decay = new ModInt("Decay", decay, false);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots, true);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0, true);
            Equipped = false;

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }

        public Weapon(Weapon other, AmmoModel ammoModel) : base(other)
        {
            Properties = new ObservableCollection<WeaponProperty>(other.Properties);
            Upgrades = new ObservableCollection<WeaponUpgrade>(other.Upgrades);
            //CompatibleAmmos = new ObservableCollection<Ammo>(); // handle this one separately
            BulletSlots = new ObservableCollection<TypeWrap<bool>>(); // we assume a new weapon always comes with an empty magazine

            CompatibleAmmos = new ObservableCollection<Ammo>();
            foreach (Ammo ammo in other.CompatibleAmmos)
            {
                Ammo ammoInModel = ammoModel.Ammos.FirstOrDefault(x => x == ammo);
                // this means the weapon probably comes from the XtrnlWeaponModel and we need to add the ammo to its Model alongside the weapon
                if (ammoInModel == null)
                {
                    ammoInModel = ammo.Clone();
                    ammoInModel.Amount.BaseValue = 300; // TODO: remove later, this is testing code. Right now, every newly added weapon will add 300 of its base ammo type.
                    ammoModel.Ammos.Add(ammoInModel);
                }

                CompatibleAmmos.Add(ammoInModel);
            }

            WeaponType = other.WeaponType;
            Type = other.Type;
            AP = other.AP.Clone();
            ToHit = other.ToHit.Clone();
            if (other.Damage != null)
                Damage = other.Damage.Clone();
            else
                Damage = new ModString("Damage", "None", true);
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

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;

            InitializeBulletSlots();
            ScaleWeaponWithDecay();
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

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<WeaponProperty> Properties { get; set; }
        public ObservableCollection<WeaponUpgrade> Upgrades { get; set; }
        public ObservableCollection<Ammo> CompatibleAmmos { get; set; }

        public ObservableCollection<TypeWrap<bool>> BulletSlots { get; set; }
        //

        // methods
        public Weapon Clone(AmmoModel ammoModel) => new Weapon(this, ammoModel);

        public void InitializeBulletSlots()
        {
            BulletSlots.Clear();
            for (int i = 0; i < NumberOfAttacks.BaseValue; i++)
            {
                BulletSlots.Add(false);
            }
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
            TakenUpgradeSlots.BaseValue = Upgrades.Count;
        }

        private void TakenUpgradeSlots_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(UpgradeSlotVisualization));
        }
        //
    }


    class WeaponProperty : LabeledString
    {
        // constructor
        public WeaponProperty(WeaponType weaponType, string name = "NewProperty", string value = "") : base(name, value, value)
        {
            _weaponType = weaponType;
        }
        //

        // members
        private WeaponType _weaponType;
        public WeaponType WeaponType
        {
            get => _weaponType;
            set => Update(ref _weaponType, value);
        }
        //

        public WeaponProperty Clone => new WeaponProperty(this.WeaponType, this.Name, this.Value);
    }

    class WeaponUpgrade : LabeledString
    {
        // constructor
        public WeaponUpgrade(WeaponType weaponType, string name="NewUpgrade", string costMultiplier="x1.0", string timeToEquip="", int slotCost = 0, string value="", string equipRequirement="")
        {
            WeaponType= weaponType;
            Name = name;
            CostMultiplier = costMultiplier;
            TimeToEquip = timeToEquip;
            SlotCost = slotCost;
            Value = value;
            Note = value;
            EquipRequirement = equipRequirement;
        }
        //

        // members
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
    }
}
