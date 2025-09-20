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
            Ammos = new ObservableCollection<Ammo>();
            AmmoEffects = new ObservableCollection<AmmoEffect>();

            // upload standard ammo
            var ammoLines = File.ReadAllLines("Resources/Spreadsheets/ammo.csv");
            foreach(var line in ammoLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                Ammo ammo = new Ammo(
                    name: parts[0],
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
                var ammoTypes = parts[3].Split(", ");
                foreach(var ammoType in ammoTypes)
                {
                    effect.AddCompatibleAmmoType(ammoType);
                }
                //

                AmmoEffects.Add(effect);
            }
            //
        }
        //

        // data
        public ObservableCollection<Ammo>? Ammos { get; set; }
        public ObservableCollection<AmmoEffect>? AmmoEffects { get; set; }
        //
    }

    class Ammo : Item
    {
        // constructors
        public Ammo() : base()
        {
            Effects = new ObservableCollection<AmmoEffect>();
            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
        }

        public Ammo(AmmoEffect effect) : base()
        {
            Effects = new ObservableCollection<AmmoEffect>();
            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
            Effects.Add(effect);
        }
        public Ammo(string name = "NewAmmo", int cost = 0, int amount = 0, float load = 0.0f) : base(name, cost, amount, load)
        {
            Effects = new ObservableCollection<AmmoEffect>();
            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
        }
        public Ammo(string name = "NewAmmo", int cost = 0, int amount = 0, float load = 0.0f, AmmoEffect effect = null) : base(name, cost, amount, load)
        {
            Effects = new ObservableCollection<AmmoEffect>();
            Effects.CollectionChanged += Effects_CollectionChanged;
            PropertyChanged += Ammo_PropertyChanged;
            if (effect != null)
                Effects.Add(effect);
        }
        //

        // members
        public ObservableCollection<AmmoEffect> Effects { get; set; }

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
        public Ammo Clone() => new Ammo
        {
            Name = this.Name,
            Cost = this.Cost,
            Load = this.Load,
            Amount = this.Amount,
            Effects = new ObservableCollection<AmmoEffect>(this.Effects),
            CustomName = this.CustomName,
        };

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
            CostMultiplier = costMultiplier;
            IsReadOnly = true;
            _compatibleAmmo = new ObservableCollection<string>();
            CompatibleAmmo = new ReadOnlyObservableCollection<string>(_compatibleAmmo);
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

        private ObservableCollection<string> _compatibleAmmo;
        public ReadOnlyObservableCollection<string> CompatibleAmmo { get; }
        //

        // methods
        public void AddCompatibleAmmoType(string type)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Cannot edit AmmoEffect.CompatibleAmmo when in read-only mode");
            _compatibleAmmo.Add(type);
        }
        public void RemoveCompatibleAmmoType(string type)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Cannot edit AmmoEffect.CompatibleAmmo when in read-only mode");
            _compatibleAmmo.Remove(type);
        }
        public void EditCompatibleAmmoType(string typeToEdit, string newType)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Cannot edit AmmoEffect.CompatibleAmmo when in read-only mode");

            int index = _compatibleAmmo.IndexOf(typeToEdit);
            if(index >= 0)
                _compatibleAmmo[index] = newType;
        }
        //
    }
}
