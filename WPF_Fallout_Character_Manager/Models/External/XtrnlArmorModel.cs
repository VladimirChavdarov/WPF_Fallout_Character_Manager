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

            Name = new ModString("Name", name, true);
            Cost = new ModInt("Cost", cost, true);
            AC = new ModInt("AC", ac, true);
            DT = new ModInt("DT", dt, true);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots, true);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0, true);
            Load = new ModFloat("Load", load, true);
            StrRequirement = new ModInt("Strength Requirement", strRequirement, true);
            Decay = new ModInt("Decay", decay, false);
            Equipped = false;

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

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<ArmorUpgrade> Upgrades { get; set; }
        //

        // methods
        public Armor Clone() => new Armor
        {
            Name = this.Name,
            Cost = this.Cost,
            Load = this.Load,
            Amount = this.Amount,
            AC = this.AC,
            DT = this.DT,
            AvailableUpgradeSlots = this.AvailableUpgradeSlots,
            TakenUpgradeSlots = this.TakenUpgradeSlots,
            StrRequirement = this.StrRequirement,
            Decay = this.Decay,
            Upgrades = new ObservableCollection<ArmorUpgrade>(this.Upgrades),
        };

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

            Name = new ModString("Name", name, true);
            Cost = new ModInt("Cost", cost, true);
            AC = new ModInt("AC", ac, true);
            DP = new ModInt("DT", dp, true);
            AvailableUpgradeSlots = new ModInt("Available Upgrade Slots", availableUpgradeSlots, true);
            TakenUpgradeSlots = new ModInt("Taken Upgrade Slots", 0, true);
            RepairDC = new ModInt("Repair DC", repairDC);
            AllottedTime = new ModInt("Allotted Time", allottedTime, true);
            Decay = new ModInt("Decay", decay, false);
            Equipped = false;

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

        public string UpgradeSlotVisualization => TakenUpgradeSlots.BaseValue.ToString() + "/" + AvailableUpgradeSlots.BaseValue.ToString(); // probably doesn't update properly. See EquippedNameAmount.

        public ObservableCollection<ArmorUpgrade> Upgrades { get; set; }
        //

        // methods
        public PowerArmor Clone() => new PowerArmor
        {
            Name = this.Name,
            Cost = this.Cost,
            Load = this.Load,
            Amount = this.Amount,
            AC = this.AC,
            DP = this.DP,
            AvailableUpgradeSlots = this.AvailableUpgradeSlots,
            TakenUpgradeSlots = this.TakenUpgradeSlots,
            RepairDC = this.RepairDC,
            AllottedTime = this.AllottedTime,
            Decay = this.Decay,
            Upgrades = new ObservableCollection<ArmorUpgrade>(this.Upgrades),
        };

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

        private void TakenUpgradeSlots_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            TakenUpgradeSlots.BaseValue = Upgrades.Count;
        }

        private void Upgrades_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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
