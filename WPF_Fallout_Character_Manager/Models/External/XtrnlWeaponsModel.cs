using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public XtrnlWeaponsModel(XtrnlAmmoModel xtrnlAmmoModel)
        {

        }
        //

        // data
        
        //
    }

    class Weapon : Item
    {
        // constructor
        public Weapon(
            string name = "NewWeapon",
            string type = "NoType",
            int cost = 0,
            int ap = 0,
            ModString damage = null,
            ValueTuple<int, int> rangeMultiplier = default,
            int critChance = 20,
            string critDamage = "None",
            Ammo ammoType = null,
            int ammoCapacity = 1,
            int strRequirement = 0,
            int decay = 0
            )
        {
            Name = name;
            Type = type;
            Cost = cost;
            AP = ap;
            if (damage != null)
                Damage = damage;
            else
                Damage = new ModString("Damage", "None", false);
            RangeMultiplier = rangeMultiplier;
            CritChance = critChance;
            CritDamage = critDamage;
            if (ammoType != null)
                AmmoType = ammoType;
            else
                AmmoType = new Ammo("Invalid Ammo", 0, 0, 0.0f);
            AmmoCapacity = ammoCapacity;
            StrRequirement = strRequirement;
            Decay = decay;
        }
        //

        // members
        private string _type;
        public string Type
        {
            get => _type;
            set => Update(ref _type, value);
        }

        private int _ap;
        public int AP
        {
            get => _ap;
            set => Update(ref _ap, value);
        }

        private ModString _damage;
        public ModString Damage
        {
            get => _damage;
            set => Update(ref _damage, value);
        }

        private ValueTuple<int, int> _rangeMultiplier;
        public ValueTuple<int, int> RangeMultiplier
        {
            get => _rangeMultiplier;
            set => Update(ref _rangeMultiplier, value);
        }

        private int _critChance;
        public int CritChance
        {
            get => _critChance;
            set => Update(ref _critChance, value);
        }

        public string _critDamage;
        public string CritDamage
        {
            get => _critDamage;
            set => Update(ref _critDamage, value);
        }

        private Ammo _ammoType;
        public Ammo AmmoType
        {
            get => _ammoType;
            set => Update(ref _ammoType, value);
        }

        private int _ammoCapacity;
        public int AmmoCapacity
        {
            get => _ammoCapacity;
            set => Update(ref _ammoCapacity, value);
        }

        private int _strRequirement;
        public int StrRequirement
        {
            get => _strRequirement;
            set => Update(ref _strRequirement, value);
        }

        private int _decay;
        public int Decay
        {
            get => _decay;
            set => Update(ref _decay, value);
        }

        public ObservableCollection<WeaponProperty> Properties;
        public ObservableCollection<WeaponUpgrade> Upgrades;
        //

        // methods
        public Weapon Clone() => new Weapon
        {
            Name = this.Name,
            Type = this.Type,
            Cost = this.Cost,
            AP = this.AP,
            Damage = this.Damage,
            RangeMultiplier = new ValueTuple<int, int>(this.RangeMultiplier.Item1, this.RangeMultiplier.Item2),
            CritChance = this.CritChance,
            CritDamage = this.CritDamage,
            AmmoType = this.AmmoType.Clone(),
            AmmoCapacity = this.AmmoCapacity,
            StrRequirement = this.StrRequirement,
            Decay = this.Decay,
            Properties = new ObservableCollection<WeaponProperty>(this.Properties.Select(p => p.Clone())),
            Upgrades = new ObservableCollection<WeaponUpgrade>(this.Upgrades.Select(u => u.Clone())),
        };
        //
    }

    // Just a wrapper of LabeledString
    class WeaponProperty : LabeledString
    {
        // constructor
        public WeaponProperty(string name = "NewProperty", string value = "") : base(name, value) { }
        //

        // methods
        public WeaponProperty Clone() => new WeaponProperty
        {
            Name = this.Name,
            Value = this.Value,
            Note = this.Note
        };
        //
    }

    class WeaponUpgrade : LabeledString
    {
        // constructor
        public WeaponUpgrade(string name="NewUpgrade", string costMultiplier="x1.0", string timeToEquip="", string value="", string equipRequirement="")
        {
            Name = name;
            CostMultiplier = costMultiplier;
            TimeToEquip = timeToEquip;
            Value = value;
            EquipRequirement = equipRequirement;
        }
        //

        // members
        private string _costMultiplier;
        public string CostMultiplier
        {
            get => _costMultiplier;
            set => Update(ref _costMultiplier, value);
        }

        private string _timeToEquip;
        public string TimeToEquip
        {
            get => _timeToEquip;
            set => Update(ref _timeToEquip, value);
        }

        // TODO: I will just show this as text for now and let the player read instead of applying automatic filters.
        // The requirements cover too many unique cases. I may do it later but if you see this comment, it's not done.
        private string _equipRequirement;
        public string EquipRequirement
        {
            get => _equipRequirement;
            set => Update(ref _equipRequirement, value);
        }
        //

        // methods
        public WeaponUpgrade Clone() => new WeaponUpgrade
        {
            Name = this.Name,
            CostMultiplier = this.CostMultiplier,
            TimeToEquip = this.TimeToEquip,
            Value = this.Value,
            EquipRequirement = this.EquipRequirement,
        };
        //
    }
}
