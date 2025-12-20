using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlArmorModel : ModelBase
    {
        // constructor
        public XtrnlArmorModel()
        {
            Armors = new ObservableCollection<Armor>();
            ArmorUpgrades = new ObservableCollection<ArmorUpgrade>();
            PowerArmors = new ObservableCollection<PowerArmor>();
            PowerArmorUpgrades = new ObservableCollection<ArmorUpgrade>();

            // upgrades
            var armorUpgradesLines = File.ReadAllLines("Resources/Spreadsheets/armor_upgrades.csv");
            foreach(var line in armorUpgradesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                for(int i = 0; i < 3; i++)
                {
                    string name = parts[0] + " (Rank " + (i+1) + ")";
                    ArmorUpgrade newUpgrade = new ArmorUpgrade(
                        name: name,
                        cost: parts[1],
                        rank: i + 1,
                        value: parts[2 + i],
                        slotCost: 1
                        );

                    if (newUpgrade.Value == "-")
                        continue;

                    ArmorUpgrades.Add(newUpgrade);
                }
            }
            var powerArmorUpgradeLines = File.ReadAllLines("Resources/Spreadsheets/power_armor_upgrades.csv");
            foreach (var line in powerArmorUpgradeLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                for (int i = 0; i < 3; i++)
                {
                    string name = parts[0] + " (Rank " + (i + 1) + ")";
                    ArmorUpgrade newUpgrade = new ArmorUpgrade(
                        name: name,
                        cost: parts[1],
                        rank: i + 1,
                        value: parts[2 + i],
                        slotCost: 1
                        );

                    if (newUpgrade.Value == "-")
                        continue;

                    PowerArmorUpgrades.Add(newUpgrade);
                }
            }
            //

            // armors
            var armorLines = File.ReadAllLines("Resources/Spreadsheets/armor.csv");
            foreach (var line in armorLines.Skip(1))
            {
                var parts = line.Split(";");

                Armor newArmor = new Armor(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    ac: Int32.Parse(parts[2]),
                    dt: Int32.Parse(parts[3]),
                    availableUpgradeSlots: Int32.Parse(parts[4]),
                    load: Utils.FloatFromString(parts[5]),
                    strRequirement: Int32.Parse(parts[6]),
                    decay: 0
                    );

                newArmor.Name.Note = parts[7];

                Armors.Add(newArmor);
            }
            //

            // power armors
            var powerArmorsLines = File.ReadAllLines("Resources/Spreadsheets/power_armor.csv");
            foreach (var line in powerArmorsLines.Skip(1))
            {
                var parts = line.Split(";");

                PowerArmor newPowerArmor = new PowerArmor(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    ac: Int32.Parse(parts[2]),
                    dp: Int32.Parse(parts[3]),
                    availableUpgradeSlots: Int32.Parse(parts[4]),
                    load: 100.0f,
                    repairDC: Int32.Parse(parts[5]),
                    allottedTime: Int32.Parse(parts[6]),
                    decay: 0
                    );

                newPowerArmor.Name.Note = "Power Armor is heavy duty, hydraulically engineered, full body armor. Power Armor operates as an extremely complicated piece of machinery that opens from the back, allowing its operator to step inside and become enveloped by the thick metal plating, acting as a pilot to a tank-like suit. Adding about a foot to their height, and extending their limbs by about half a foot. Even the lower end models of Power Armor provide immense protection and power to its wearer.";

                PowerArmors.Add(newPowerArmor);
            }
            //
        }
        //

        // data
        public ObservableCollection<Armor> Armors { get; set; }
        public ObservableCollection<ArmorUpgrade> ArmorUpgrades { get; set; }
        public ObservableCollection<PowerArmor> PowerArmors { get; set; }
        public ObservableCollection<ArmorUpgrade> PowerArmorUpgrades { get; set; }
        //
    }

    class Armor : Item
    {
        // constructor
        public Armor(
            string name = "NewArmor",
            int cost = 0,
            int ac = 0,
            int dt = 0,
            int availableUpgradeSlots = 0,
            float load = 0.0f,
            int strRequirement = 0,
            int decay = 0
            )
        {
            Upgrades = new ObservableCollection<ArmorUpgrade>();

            Name = new ModString("Name", name);
            Cost = new ModInt("Cost", cost);
            AC = new ModInt("AC", ac);
            DT = new ModInt("DT", dt);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0);
            Load = new ModFloat("Load", load);
            StrRequirement = new ModInt("Strength Requirement", strRequirement);
            Decay = new ModInt("Decay", decay, false);
            Equipped = false;

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }

        protected Armor(Armor other) : base(other)
        {
            Upgrades = new ObservableCollection<ArmorUpgrade>(other.Upgrades);
            AC = other.AC.Clone();
            DT = other.DT.Clone();
            AvailableUpgradeSlots = other.AvailableUpgradeSlots.Clone();
            TakenUpgradeSlots = other.TakenUpgradeSlots.Clone();
            StrRequirement = other.StrRequirement.Clone();
            Decay = other.Decay.Clone();
            Equipped = other.Equipped;

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }
        //

        // members
        private ModInt _ac;
        public ModInt AC
        {
            get => _ac;
            set => Update(ref _ac, value);
        }

        private ModInt _dt;
        public ModInt DT
        {
            get => _dt;
            set => Update(ref _dt, value);
        }

        private ModInt _availableUpgradeSlots;
        public ModInt AvailableUpgradeSlots
        {
            get => _availableUpgradeSlots;
            set => Update(ref _availableUpgradeSlots, value);
        }

        private ModInt _takenUpgradeSlots;
        public ModInt TakenUpgradeSlots
        {
            get => _takenUpgradeSlots;
            set => Update(ref _takenUpgradeSlots, value);
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

        private bool _equipped;
        public bool Equipped
        {
            get => _equipped;
            set
            {
                Update(ref _equipped, value);
                Load.BaseValue = Equipped ? Load.BaseValue / 2.0f : Load.BaseValue * 2.0f;
                OnPropertyChanged(nameof(EquippedNameAmount));
            }
        }

        public string EquippedNameAmount
        {
            get
            {
                if (Equipped)
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
                foreach (ArmorUpgrade upgrade in Upgrades)
                {
                    result += " (" + upgrade.Name + ")";
                }

                return result;
            }
        }

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<ArmorUpgrade> Upgrades { get; set; }
        //

        // methods
        public Armor Clone() => new Armor(this);

        public void AddUpgrade(object obj)
        {
            if (obj is ArmorUpgrade upgradeToAdd)
            {
                if (TakenUpgradeSlots.Total + upgradeToAdd.SlotCost > AvailableUpgradeSlots.Total)
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
            if (obj is ArmorUpgrade upgradeToRemove)
            {
                Upgrades.Remove(upgradeToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        private void Decay_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ScaleArmorWithDecay();
        }

        private void ScaleArmorWithDecay()
        {
            int decay = Decay.Total;
            int halfDecay = decay / 2;

            if(AC.BaseValue - halfDecay >= 10)
            {
                ApplyDecay(AC, decay, -halfDecay);
            }

            if (DT.BaseValue - halfDecay >= 0)
            {
                ApplyDecay(DT, decay, -halfDecay);
            }

            float newCost = -Cost.BaseValue * (float)(decay / 10.0f);
            ApplyDecay(Cost, decay, (int)newCost);
        }

        private void Upgrades_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TakenUpgradeSlots.BaseValue = Upgrades.Count;
            OnPropertyChanged(nameof(NameAmount));
            OnPropertyChanged(nameof(EquippedNameAmount));
        }

        private void TakenUpgradeSlots_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(UpgradeSlotVisualization));
        }
        //
    }

    class PowerArmor: Item
    {
        // constructor
        public PowerArmor(
            string name = "NewPowerArmor",
            int cost = 0,
            int ac = 0,
            int dp = 0,
            int availableUpgradeSlots = 0,
            float load = 0.0f,
            int repairDC = 0,
            int allottedTime = 0,
            int decay = 0
            )
        {
            Upgrades = new ObservableCollection<ArmorUpgrade>();

            Name = new ModString("Name", name);
            Cost = new ModInt("Cost", cost);
            AC = new ModInt("AC", ac);
            DP = new ModInt("DP", dp);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0);
            Load = new ModFloat("Load", load);
            RepairDC = new ModInt("Repair DC", repairDC);
            AllottedTime = new ModInt("Allotted Time", allottedTime);
            Decay = new ModInt("Decay", decay, false);
            Equipped = false;

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }

        protected PowerArmor(PowerArmor other) : base(other)
        {
            Upgrades = new ObservableCollection<ArmorUpgrade>(other.Upgrades);

            AC = other.AC.Clone();
            DP = other.DP.Clone();
            AvailableUpgradeSlots = other.AvailableUpgradeSlots.Clone();
            TakenUpgradeSlots = other.TakenUpgradeSlots.Clone();
            RepairDC = other.RepairDC.Clone();
            AllottedTime = other.AllottedTime.Clone();
            Decay = other.Decay.Clone();
            Equipped = other.Equipped;

            _decay.PropertyChanged += Decay_PropertyChanged;
            Upgrades.CollectionChanged += Upgrades_CollectionChanged;
            TakenUpgradeSlots.PropertyChanged += TakenUpgradeSlots_PropertyChanged;
        }
        //

        // members
        private ModInt _ac;
        public ModInt AC
        {
            get => _ac;
            set => Update(ref _ac, value);
        }

        private ModInt _dp;
        public ModInt DP
        {
            get => _dp;
            set => Update(ref _dp, value);
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

        private ModInt _repairDC;
        public ModInt RepairDC
        {
            get => _repairDC;
            set => Update(ref _repairDC, value);
        }

        private ModInt _allottedTime;
        public ModInt AllottedTime
        {
            get => _allottedTime;
            set => Update(ref _allottedTime, value);
        }

        private ModInt _decay;
        public ModInt Decay
        {
            get => _decay;
            set => Update(ref _decay, value);
        }

        private bool _equipped;
        public bool Equipped
        {
            get => _equipped;
            set
            {
                Update(ref _equipped, value);

                Load.BaseValue = Equipped ? 0.0f : 100.0f;

                OnPropertyChanged(nameof(EquippedNameAmount));
            }
        }

        public string EquippedNameAmount
        {
            get
            {
                if (Equipped)
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
                foreach (ArmorUpgrade upgrade in Upgrades)
                {
                    result += " (" + upgrade.Name + ")";
                }

                return result;
            }
        }

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<ArmorUpgrade> Upgrades { get; set; }
        //

        // methods
        public PowerArmor Clone() => new PowerArmor(this);

        public void AddUpgrade(object obj)
        {
            if (obj is ArmorUpgrade upgradeToAdd)
            {
                if (TakenUpgradeSlots.Total + upgradeToAdd.SlotCost > AvailableUpgradeSlots.Total)
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
            if (obj is ArmorUpgrade upgradeToRemove)
            {
                Upgrades.Remove(upgradeToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        private void Decay_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ScalePowerArmorWithDecay();
        }

        private void ScalePowerArmorWithDecay()
        {
            int decay = Decay.Total;
            int halfDecay = decay / 2;

            if (AC.BaseValue - halfDecay >= 10)
            {
                ApplyDecay(AC, decay, -halfDecay);
            }

            float costReduction = -Cost.BaseValue * (float)(decay / 10.0f);
            ApplyDecay(Cost, decay, (int)costReduction);
        }

        private void Upgrades_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            TakenUpgradeSlots.BaseValue = Upgrades.Count;
            OnPropertyChanged(nameof(NameAmount));
            OnPropertyChanged(nameof(EquippedNameAmount));
        }

        private void TakenUpgradeSlots_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(UpgradeSlotVisualization));
        }
        //
    }

    class ArmorUpgrade : LabeledString
    {
        // constructor
        public ArmorUpgrade(string name="NewUpgrade", string cost = "c0", int rank = 0, string value = "", int slotCost = 0)
        {
            Name = name;
            Cost = cost;
            Rank = rank;
            Value = value;
            Note = value;
            SlotCost = slotCost;
        }
        //

        // members
        private string _cost;
        public string Cost
        {
            get => _cost;
            set => Update(ref _cost, value);
        }

        private int _rank;
        public int Rank
        {
            get => _rank;
            set => Update(ref _rank, value);
        }

        private int _slotCost;
        public int SlotCost
        {
            get => _slotCost;
            set => Update(ref _slotCost, value);
        }
        //
    }
}
