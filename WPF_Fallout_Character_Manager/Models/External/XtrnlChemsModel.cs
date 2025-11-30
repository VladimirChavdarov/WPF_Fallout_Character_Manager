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
    class XtrnlChemsModel : ModelBase
    {
        // constructor
        public XtrnlChemsModel()
        {
            Chems = new ObservableCollection<Chem>();
            ChemProperties = new ObservableCollection<ChemProperty>();

            var chemPropertiesLines = File.ReadAllLines("Resources/Spreadsheets/chem_properties.csv");
            foreach (var line in chemPropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                ChemProperty chemProperty = new ChemProperty(
                    name: parts[0],
                    value: parts[1]
                    );
                ChemProperties.Add(chemProperty);
            }

            var chemLines = File.ReadAllLines("Resources/Spreadsheets/chems.csv");
            foreach (var line in chemLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                Chem chem = new Chem(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: (float)Int32.Parse(parts[3]) / 10.0f
                );

                // chem properties
                string[] properties = parts[2].Split(".");
                foreach(string property in properties.SkipLast(1))
                {
                    string trimmedProperty = property.Trim();

                    ChemProperty newProperty = ChemProperties.FirstOrDefault(x => x.Name.Contains(trimmedProperty));
                    if (newProperty == null)
                        throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                    chem.Properties.Add(newProperty);
                }

                Chems.Add(chem);
            }
        }
        //

        // data
        ObservableCollection<Chem> Chems {  get; set; }
        ObservableCollection<ChemProperty> ChemProperties { get; set; }
        //
    }

    class Chem : Item
    {
        // constructor
        public Chem(string name = "NewChem", int cost = 0, float load = 0.0f)
        {
            Properties = new ObservableCollection<ChemProperty>();

            Name = new ModString("Name", name);
            Cost = new ModInt("Cost", cost);
            Load = new ModFloat("Load", load);
        }

        protected Chem(Chem other) : base(other)
        {
            Properties = new ObservableCollection<ChemProperty>(other.Properties);
        }
        //

        // members
        public ObservableCollection<ChemProperty> Properties { get; set; }
        //

        // methods
        public Chem Clone() => new Chem(this);
        //public Chem Clone => new Chem
        //{
        //    Name = this.Name,
        //    Cost = this.Cost,
        //    Load = this.Load,
        //    Properties = new ObservableCollection<ChemProperty>(this.Properties),
        //};
        //
    }

    class ChemProperty : LabeledString
    {
        // constructor
        public ChemProperty(string name = "NewChemProperty", string value = "") : base(name, value, value) { }
        //
    }
}
