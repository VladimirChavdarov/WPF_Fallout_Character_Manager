using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Media.Effects;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlAmmoModel : ModelBase
    {
        // constructor
        public XtrnlAmmoModel()
        {
            AmmoTypes = new ObservableCollection<string>();
            Ammos = new ObservableCollection<Ammo>();
            AmmoEffects = new ObservableCollection<AmmoEffect>();

            // upload standard ammo
            var ammoLines = File.ReadAllLines("Resources/Spreadsheets/ammo.csv");
            foreach(var line in ammoLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                AmmoTypes.Add(parts[0]);

                Ammo ammo = new Ammo(
                    name: parts[0],
                    type: parts[0],
                    cost: Int32.Parse(parts[1]),
                    amount: 0,
                    load: (float)Int32.Parse(parts[2]) / 10.0f
                    );
                Ammos.Add(ammo);
            }
            //

            // upload ammo effects (special ammo)
            var ammoEffectLines = File.ReadLines("Resources/Spreadsheets/ammo_special.csv");
            foreach (var line in ammoEffectLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                AmmoEffect effect = new AmmoEffect(
                    name: parts[0],
                    costMultiplier: parts[1],
                    value: parts[2]);

                // compatible ammo types
                effect.IsReadOnly = false;
                var ammoTypes = parts[3].Split(", ");
                foreach(var ammoType in ammoTypes)
                {
                    effect.CompatibleAmmo.Add(ammoType);
                }
                effect.IsReadOnly = true;
                //

                AmmoEffects.Add(effect);
            }
            //
        }
        //

        // data
        public ObservableCollection<string> AmmoTypes { get; set; }
        public ObservableCollection<Ammo> Ammos { get; set; }
        public ObservableCollection<AmmoEffect> AmmoEffects { get; set; }
        //
    }

    class Ammo : Item
    {
        // constructor
        public Ammo(string name = "NewAmmo", string type = "NoType", int cost = 0, int amount = 0, float load = 0.0f, AmmoEffect effect = null, string description = "") : base(name, cost, amount, load, description)
        {
            Type = type;

            Effects = new ObservableCollection<AmmoEffect>();
            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
            if (effect != null)
                Effects.Add(effect);
        }

        protected Ammo(Ammo other) : base(other)
        {
            Type = other.Type;
            Effects = new ObservableCollection<AmmoEffect>(other.Effects);
            CustomName = other.CustomName;

            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
        }
        //

        // members
        public ObservableCollection<AmmoEffect> Effects { get; set; }

        private string _type;
        public string Type
        {
            get => _type;
            set => Update(ref _type, value);
        }

        private bool _customName = false;
        public bool CustomName
        {
            get => _customName;
            set => Update(ref  _customName, value);
        }

        public string NameAmountEffects
        {
            get
            {
                string result = NameAmount;
                if(!CustomName)
                {
                    foreach(AmmoEffect effect in Effects)
                    {
                        if(effect.Name != "")
                            result += " (" + effect.Name + ")";
                    }
                }
                return result;
            }
        }
        //

        // methods
        public Ammo Clone() => new Ammo(this);
        //public Ammo Clone() => new Ammo
        //{
        //    Name = this.Name,
        //    Cost = this.Cost,
        //    Load = this.Load,
        //    Amount = this.Amount,
        //    Effects = new ObservableCollection<AmmoEffect>(this.Effects),
        //    CustomName = this.CustomName,
        //};

        private void Effects_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NameAmountEffects));
        }

        private void Ammo_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NameAmount)) // update NameAmountEffects if NameAmount changes
            {
                OnPropertyChanged(nameof(NameAmountEffects));
            }
        }
        //
    }

    class AmmoEffect : LabeledString
    {
        // constructor
        public AmmoEffect(string name = "NewAmmoEffect", string value = "", string costMultiplier = "x1.0")
        {
            Name = name;
            Value = value;
            Note = value;
            CostMultiplier = costMultiplier;
            IsReadOnly = true;
            CompatibleAmmo = new ObservableCollection<string>();
        }
        //

        // members
        private string _costMultiplier;
        public string CostMultiplier
        {
            get => _costMultiplier;
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Cannot edit LabeledString.CostMultiplier when in read-only mode");
                Update(ref _costMultiplier, value);
            }
        }

        public ObservableCollection<string> CompatibleAmmo { get; set; }
        //
    }
}
