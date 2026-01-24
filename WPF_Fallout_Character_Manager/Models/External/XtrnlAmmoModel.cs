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
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlAmmoModel : ModelBase
    {
        private static string ammoEffectsPath = "Resources/Spreadsheets/ammo_special.csv";
        private static string ammoPath = "Resources/Spreadsheets/ammo.csv";

        // constructor
        public XtrnlAmmoModel()
        {
            AmmoTypes = new ObservableCollection<string>();
            Ammos = new ObservableCollection<Ammo>();
            AmmoEffects = new ObservableCollection<AmmoEffect>();

            Utils.UpdateCSVFilesIdFields(ammoEffectsPath);

            // upload standard ammo
            var ammoLines = File.ReadAllLines(ammoPath);
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
            var ammoEffectLines = File.ReadLines(ammoEffectsPath);
            foreach (var line in ammoEffectLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                Utils.IdFromString(parts[4], out Guid id);

                AmmoEffect effect = new AmmoEffect(
                    id,
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
                //

                effect.ConstructNote();
                effect.IsReadOnly = true;

                AmmoEffects.Add(effect);
            }
            //

            Ammos.CollectionChanged += Ammos_CollectionChanged;
        }
        //

        // methods
        private void Ammos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach(Ammo a in e.NewItems)
                {
                    a.Type = a.Name.Total;
                }
            }

            DetermineAmmoTypes();
        }

        private void DetermineAmmoTypes()
        {
            AmmoTypes.Clear();
            foreach (Ammo a in Ammos)
            {
                if (!AmmoTypes.Contains(a.Type))
                {
                    AmmoTypes.Add(a.Type);
                }
            }
        }
        //

        // data
        public static ObservableCollection<string> AmmoTypes { get; set; }
        public static ObservableCollection<Ammo> Ammos { get; set; }
        public static ObservableCollection<AmmoEffect> AmmoEffects { get; set; }
        //
    }

    class Ammo : Item
    {
        // constructor
        public Ammo(string name = "NewAmmo", string type = "NoType", int cost = 0, int amount = 0, float load = 0.0f, AmmoEffect effect = null, string description = "")
            : base(name, cost, amount, load, description)
        {
            Type = type;

            Effects = new ObservableCollection<AmmoEffect>();
            SubscribeToPropertyChanged();
            if (effect != null)
                Effects.Add(effect);
        }

        protected Ammo(Ammo other) : base(other)
        {
            Type = other.Type;
            Effects = new ObservableCollection<AmmoEffect>(other.Effects);

            SubscribeToPropertyChanged();
        }

        public Ammo(AmmoDTO dto)
        {
            Effects = new ObservableCollection<AmmoEffect>();
            FromDto(dto);
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

        public string NameAmountEffects
        {
            get
            {
                string result = NameAmount;
                foreach(AmmoEffect effect in Effects)
                {
                    if(effect.Name != "")
                        result += " (" + effect.Name + ")";
                }
                return result;
            }
        }

        public override string NameAmount
        {
            get
            {
                string result = base.NameAmount;
                foreach (AmmoEffect effect in Effects)
                {
                    if (effect.Name != "")
                        result += " (" + effect.Name + ")";
                }
                return result;
            }
        }
        //

        // methods
        public Ammo Clone() => new Ammo(this);

        public void SubscribeToPropertyChanged()
        {
            Effects.CollectionChanged -= Effects_CollectionChanged;
            Effects.CollectionChanged += Effects_CollectionChanged;
        }

        public void AddProperty(object obj)
        {
            if (obj is AmmoEffect effectToAdd)
            {
                if(effectToAdd.CompatibleAmmo.Contains(Type))
                {
                    Effects.Add(effectToAdd);
                }
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public void RemoveProperty(object obj)
        {
            if (obj is AmmoEffect effectToRemove)
            {
                Effects.Remove(effectToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        private void Effects_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NameAmount));
        }

        public override ItemDTO ToDto()
        {
            AmmoDTO result = new AmmoDTO();

            UpdateItemDTO(result);
            
            result.Type = Type;
            
            foreach(var effect in Effects)
            {
                result.EffectIds.Add(effect.Id);
            }

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            var typedDto = Utils.EnsureDtoType<AmmoDTO>(dto);

            base.FromDto(dto, versionMismatch);

            Type = typedDto.Type;

            foreach(Guid id in typedDto.EffectIds)
            {
                AmmoEffect effect = XtrnlAmmoModel.AmmoEffects.FirstOrDefault(x => x.Id == id);
                if (effect != null)
                {
                    Effects.Add(effect);
                }
                else
                {
                    throw new ArgumentException("Couldn't find this ammo effect in the master list");
                }
            }

            SubscribeToPropertyChanged();
        }
        //
    }

    class AmmoEffect : ItemAttribute
    {
        // constructor
        public AmmoEffect(Guid id, string name = "NewAmmoEffect", string value = "", string costMultiplier = "x1.0")
            : base(id, name, value)
        {
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

        // methods
        public void ConstructNote()
        {
            Note += "\nCompatibility: ";
            foreach (string compatibleAmmo in CompatibleAmmo)
            {
                Note += compatibleAmmo + ". ";
            }
        }
        //
    }
}
