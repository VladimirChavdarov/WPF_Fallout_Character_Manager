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
    class XtrnlExplosivesModel : ModelBase
    {
        private static readonly string explosivePropertiesPath = "Resources/Spreadsheets/explosives_properties.csv";
        private static readonly string explosivesThrownPath = "Resources/Spreadsheets/explosives_thrown.csv";
        private static readonly string explosivesPlacedPath = "Resources/Spreadsheets/explosives_placed.csv";

        // constructor
        public XtrnlExplosivesModel()
        {
            Explosives = new ObservableCollection<Explosive>();
            ExplosiveProperties = new ObservableCollection<ExplosiveProperty>();
            ExplosiveTypes = new ObservableCollection<string>() { "Thrown Explosive", "Placed Explosive" };

            Utils.UpdateCSVFilesIdFields(explosivePropertiesPath);

            var explosivePropertiesLines = File.ReadAllLines(explosivePropertiesPath);
            foreach (var line in explosivePropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                Utils.IdFromString(parts[2], out Guid id);

                ExplosiveProperty explosiveProperty = new ExplosiveProperty(
                    id,
                    name: parts[0],
                    value: parts[1]
                    );
                ExplosiveProperties.Add(explosiveProperty);
            }

            var explosivesPlacedLines = File.ReadAllLines(explosivesPlacedPath);
            foreach (var line in explosivesPlacedLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 8)
                    continue;

                Explosive explosive = new Explosive(
                    name: parts[0],
                    type: ExplosiveTypes[1],
                    cost: Int32.Parse(parts[1]),
                    ap: Int32.Parse(parts[2]),
                    damage: parts[3],
                    armDC: Int32.Parse(parts[4]),
                    areaOfEffect: parts[5],
                    load: (float)Int32.Parse(parts[7])
                    );

                SetPropertiesOfExplosive(explosive, parts[6]);

                explosive.ConstructNote();

                Explosives.Add(explosive);
            }

            var explosivesThrownLines = File.ReadAllLines(explosivesThrownPath);
            foreach (var line in explosivesThrownLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 8)
                    continue;

                Explosive explosive = new Explosive(
                    name: parts[0],
                    type: ExplosiveTypes[0],
                    cost: Int32.Parse(parts[1]),
                    ap: Int32.Parse(parts[2]),
                    damage: parts[3],
                    armDC: 0,
                    range: parts[4],
                    areaOfEffect: parts[5],
                    load: (float)Int32.Parse(parts[7])
                    );

                SetPropertiesOfExplosive(explosive, parts[6]);

                explosive.ConstructNote();

                Explosives.Add(explosive);
            }
        }
        //

        // methods
        void SetPropertiesOfExplosive(Explosive explosive, string propertyLine)
        {
            string[] properties = propertyLine.Split(".");
            foreach (string property in properties.SkipLast(1))
            {
                string trimmedProperty = property.Trim();

                ExplosiveProperty newProperty = ExplosiveProperties.FirstOrDefault(x => x.Name == trimmedProperty);
                if(newProperty == null)
                {
                    Utils.IdFromString("", out Guid id);
                    newProperty = new ExplosiveProperty(id, trimmedProperty, "This specifies another property the weapon has.");
                    string csvPath = "";
                    if(explosive.Type == ExplosiveTypes[0])
                    {
                        csvPath = explosivesThrownPath;
                    }
                    else if(explosive.Type == ExplosiveTypes[1])
                    {
                        csvPath = explosivesPlacedPath;
                    }
                    Utils.AddCSVLine(csvPath, newProperty.Name + ";" + newProperty.Value + ";" + id.ToString());
                    ExplosiveProperties.Add(newProperty);
                }

                if (newProperty == null)
                    throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                explosive.Properties.Add(newProperty);
            }
        }
        //

        // data
        public static ObservableCollection<Explosive> Explosives { get; set; }
        public static ObservableCollection<ExplosiveProperty> ExplosiveProperties { get; set; }
        public static ObservableCollection<string> ExplosiveTypes { get; set; }
        //
    }

    class Explosive : Item
    {
        // constructor
        public Explosive(string name = "NewExplosive", string type = "", int cost = 0, int ap = 0, string damage = "None", int armDC = 0, string range = "0ft.", string areaOfEffect = "0ft. radius", float load = 0.0f)
        {
            Properties = new ObservableCollection<ExplosiveProperty>();

            Name = new ModString("Name", name, true);
            Type = type;
            Cost = new ModInt("Cost", cost, true);
            AP = new ModInt("AP", ap, true);
            Damage = new ModString("Damage", damage, true);
            ArmDC = new ModInt("Arm DC", armDC, true);
            Range = new ModString("Range", range, true);
            AreaOfEffect = new ModString("Area of Effect", areaOfEffect, true);
            Load = new ModFloat("Load", load, true);

            SubscribeToCollectionChanged();
        }

        protected Explosive(Explosive other) : base(other)
        {
            Properties = new ObservableCollection<ExplosiveProperty>(other.Properties);

            this.Type = other.Type;
            AP = other.AP.Clone();
            Damage = other.Damage.Clone();
            ArmDC = other.ArmDC.Clone();
            Range = other.Range.Clone();
            AreaOfEffect = other.AreaOfEffect.Clone();

            SubscribeToCollectionChanged();
        }

        public Explosive(ExplosiveDTO dto)
        {
            Properties = new ObservableCollection<ExplosiveProperty>();

            FromDto(dto);
        }
        //

        // members
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                ConstructNote();
            }
        }

        private ModInt _ap;
        public ModInt AP
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

        private ModInt _armDC;
        public ModInt ArmDC
        {
            get => _armDC;
            set => Update(ref _armDC, value);
        }

        private ModString _range;
        public ModString Range
        {
            get => _range;
            set => Update(ref _range, value);
        }

        private ModString _areaOfEffect;
        public ModString AreaOfEffect
        {
            get => _areaOfEffect;
            set => Update(ref _areaOfEffect, value);
        }

        public ObservableCollection<ExplosiveProperty> Properties { get; set; }
        //

        // methods
        public Explosive Clone() => new Explosive(this);

        public void SubscribeToCollectionChanged()
        {
            Properties.CollectionChanged -= Properties_CollectionChanged;
            Properties.CollectionChanged += Properties_CollectionChanged;
        }

        public override void ConstructNote()
        {
            string note = Type + "\n";
            foreach (ExplosiveProperty property in Properties)
            {
                note += property.Name + ". ";
            }
            Name.Note = note;
        }

        public void AddProperty(object obj)
        {
            if (obj is ExplosiveProperty propertyToAdd)
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
            if (obj is ExplosiveProperty propertyToRemove)
            {
                Properties.Remove(propertyToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        private void Properties_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConstructNote();
        }

        public override ItemDTO ToDto()
        {
            ExplosiveDTO result = new ExplosiveDTO();

            UpdateItemDTO(result);

            result.Type = Type;
            result.AP = AP.ToDto();
            result.Damage = Damage.ToDto();
            result.ArmDC = ArmDC.ToDto();
            result.Range = Range.ToDto();
            result.AreaOfEffect = AreaOfEffect.ToDto();

            foreach(var property in Properties)
            {
                result.PropertyIds.Add(property.Id);
            }

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            var typedDto = Utils.EnsureDtoType<ExplosiveDTO>(dto);

            base.FromDto(dto, versionMismatch);

            Type = typedDto.Type;
            AP = new ModInt(typedDto.AP);
            Damage = new ModString(typedDto.Damage);
            ArmDC = new ModInt(typedDto.ArmDC);
            Range = new ModString(typedDto.Range);
            AreaOfEffect = new ModString(typedDto.AreaOfEffect);

            foreach (Guid id in typedDto.PropertyIds)
            {
                ExplosiveProperty property = XtrnlExplosivesModel.ExplosiveProperties.FirstOrDefault(x => x.Id == id);
                if (property != null)
                {
                    Properties.Add(property);
                }
                else
                {
                    throw new ArgumentException("Couldn't find this explosive property in the master list");
                }
            }

            SubscribeToCollectionChanged();
        }
        //
    }

    class ExplosiveProperty : ItemAttribute
    {
        // constructor
        public ExplosiveProperty(Guid id, string name = "NewExplosiveProperty", string value = "") : base(id, name, value) { }
        //
    }
}
