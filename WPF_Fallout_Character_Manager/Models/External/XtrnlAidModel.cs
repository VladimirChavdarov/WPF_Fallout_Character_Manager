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
    class XtrnlAidModel : ModelBase
    {
        // constructor
        public XtrnlAidModel()
        {
            AidItems = new ObservableCollection<Aid>();
            AidProperties = new ObservableCollection<AidProperty>();

            // Chems
            var chemPropertiesLines = File.ReadAllLines("Resources/Spreadsheets/chem_properties.csv");
            foreach (var line in chemPropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                AidProperty chemProperty = new AidProperty(
                    name: parts[0],
                    value: parts[1]
                    );
                AidProperties.Add(chemProperty);
            }

            var chemLines = File.ReadAllLines("Resources/Spreadsheets/chems.csv");
            foreach (var line in chemLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                Aid chem = new Aid(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: (float)Int32.Parse(parts[3]) / 10.0f
                );

                // chem properties
                string[] properties = parts[2].Split(".");
                foreach(string property in properties.SkipLast(1))
                {
                    string trimmedProperty = property.Trim();

                    AidProperty newProperty = AidProperties.FirstOrDefault(x => x.Name.Contains(trimmedProperty));
                    if (newProperty == null)
                        throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                    chem.Properties.Add(newProperty);
                }

                AidItems.Add(chem);
            }
            //

            // Medicine
            var medicineLines = File.ReadAllLines("Resources/Spreadsheets/medicine.csv");
            foreach (var line in medicineLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                Aid medicine = new Aid(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    description: parts[2],
                    load: (float)Int32.Parse(parts[3]) / 10.0f
                );

                AidItems.Add(medicine);
            }
            //
        }
        //

        // data
        public ObservableCollection<Aid> AidItems {  get; set; }
        public ObservableCollection<AidProperty> AidProperties { get; set; }
        //
    }

    class Aid : Item
    {
        // constructor
        public Aid(string name = "NewAid", int cost = 0, string description = "", float load = 0.0f)
        {
            Properties = new ObservableCollection<AidProperty>();

            Name = new ModString("Name", name, false, description);
            Cost = new ModInt("Cost", cost);
            Load = new ModFloat("Load", load);
        }

        protected Aid(Aid other) : base(other)
        {
            Properties = new ObservableCollection<AidProperty>(other.Properties);
        }
        //

        // members
        public ObservableCollection<AidProperty> Properties { get; set; }
        //

        // methods
        public Aid Clone() => new Aid(this);
        //
    }

    class AidProperty : LabeledString
    {
        // constructor
        public AidProperty(string name = "NewAidProperty", string value = "") : base(name, value, value) { }
        //
    }
}
