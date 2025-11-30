using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlExplosivesModel : ModelBase
    {
        // constructor
        public XtrnlExplosivesModel()
        {
            Explosives = new ObservableCollection<Explosive>();
            ExplosiveProperties = new ObservableCollection<ExplosiveProperty>();

            var explosivePropertiesLines = File.ReadAllLines("Resources/Spreadsheets/explosives_properties.csv");
            foreach (var line in explosivePropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                ExplosiveProperty explosiveProperty = new ExplosiveProperty(
                    name: parts[0],
                    value: parts[1]
                    );
                ExplosiveProperties.Add(explosiveProperty);
            }

            var explosivesPlacedLines = File.ReadAllLines("Resources/Spreadsheets/explosives_placed.csv");
            foreach (var line in explosivesPlacedLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 8)
                    continue;

                Explosive explosive = new Explosive(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    ap: Int32.Parse(parts[2]),
                    damage: parts[3],
                    armDC: Int32.Parse(parts[4]),
                    areaOfEffect: parts[5],
                    load: (float)Int32.Parse(parts[7])
                    );

                SetPropertiesOfExplosive(explosive, parts[6]);

                Explosives.Add(explosive);
            }

            var explosivesThrownLines = File.ReadAllLines("Resources/Spreadsheets/explosives_thrown.csv");
            foreach (var line in explosivesThrownLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 8)
                    continue;

                Explosive explosive = new Explosive(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    ap: Int32.Parse(parts[2]),
                    damage: parts[3],
                    armDC: 0,
                    range: parts[4],
                    areaOfEffect: parts[5],
                    load: (float)Int32.Parse(parts[7])
                    );

                SetPropertiesOfExplosive(explosive, parts[6]);

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

                ExplosiveProperty newProperty = ExplosiveProperties.FirstOrDefault(x => x.Name.Contains(trimmedProperty));
                if(newProperty == null)
                {
                    newProperty = ExplosiveProperties.FirstOrDefault(x => trimmedProperty.Contains(x.Name));
                }

                if(newProperty.Name != trimmedProperty)
                {
                    ExplosiveProperty propertyToAddToMasterList = new ExplosiveProperty(trimmedProperty, newProperty.Value);
                    newProperty = propertyToAddToMasterList;
                    ExplosiveProperties.Add(propertyToAddToMasterList);
                }

                if (newProperty == null)
                    throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                explosive.Properties.Add(newProperty);
            }
        }
        //

        // data
        ObservableCollection<Explosive> Explosives { get; set; }
        ObservableCollection<ExplosiveProperty> ExplosiveProperties { get; set; }
        //
    }

    class Explosive : Item
    {
        // constructor
        public Explosive(string name = "NewExplosive", int cost = 0, int ap = 0, string damage = "None", int armDC = 0, string range = "0ft.", string areaOfEffect = "0ft. radius", float load = 0.0f)
        {
            Properties = new ObservableCollection<ExplosiveProperty>();

            Name = new ModString("Name", name, true);
            Cost = new ModInt("Cost", cost, true);
            AP = new ModInt("AP", ap, true);
            Damage = new ModString("Damage", damage, true);
            ArmDC = new ModInt("Arm DC", armDC, true);
            Range = new ModString("Range", range, true);
            AreaOfEffect = new ModString("Area of Effect", areaOfEffect, true);
            Load = new ModFloat("Load", load, true);
        }

        protected Explosive(Explosive other) : base(other)
        {
            Properties = new ObservableCollection<ExplosiveProperty>(other.Properties);

            AP = other.AP.Clone();
            Damage = other.Damage.Clone();
            ArmDC = other.ArmDC.Clone();
            Range = other.Range.Clone();
            AreaOfEffect = other.AreaOfEffect.Clone();
        }
        //

        // members
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
        //
    }

    class ExplosiveProperty : LabeledString
    {
        // constructor
        public ExplosiveProperty(string name = "NewExplosiveProperty", string value = "") : base(name, value, value) { }
        //
    }
}
