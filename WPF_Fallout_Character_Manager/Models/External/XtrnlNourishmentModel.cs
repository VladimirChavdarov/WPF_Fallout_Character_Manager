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
    class XtrnlNourishmentModel : ModelBase
    {
        // constructor
        public XtrnlNourishmentModel()
        {
            Nourishments = new ObservableCollection<Nourishment>();
            NourishmentProperties = new ObservableCollection<NourishmentProperty>();


            var nourishmentPropertiesLines = File.ReadAllLines("Resources/Spreadsheets/foods_drinks_properties.csv");
            foreach (var line in nourishmentPropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                NourishmentProperty nourishmentProperty = new NourishmentProperty(
                    name: parts[0],
                    value: parts[1]
                    );
                NourishmentProperties.Add(nourishmentProperty);
            }

            var nourishmentLines = File.ReadAllLines("Resources/Spreadsheets/foods_drinks.csv");
            foreach (var line in nourishmentLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                Nourishment nourishment = new Nourishment(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: (float)Int32.Parse(parts[3])
                    );

                SetPropertiesOfNourishment(nourishment, parts[2]);

                nourishment.ConstructNote();

                Nourishments.Add(nourishment);
            }
        }
        //

        // methods
        void SetPropertiesOfNourishment(Nourishment nourishment, string propertyLine)
        {
            string[] properties = propertyLine.Split('.');
            foreach(string property in properties.SkipLast(1))
            {
                string trimmedProperty = property.Trim();

                NourishmentProperty newProperty = NourishmentProperties.FirstOrDefault(x => x.Name.Contains(trimmedProperty));

                if (newProperty == null)
                    throw new Exception($"Cannot find property in master list. Property: {trimmedProperty}");
                nourishment.Properties.Add(newProperty);
            }
        }
        //

        // data
        public ObservableCollection<Nourishment> Nourishments { get; set; }
        public ObservableCollection<NourishmentProperty> NourishmentProperties { get; set; }
        //
    }

    // encompases foods and drinks
    class Nourishment : Item
    {
        // constructor
        public Nourishment(string name = "NewNourishment", int cost = 0, float load = 0.0f) : base(name, cost, 0, load)
        {
            Properties = new ObservableCollection<NourishmentProperty>();

            Properties.CollectionChanged += Properties_CollectionChanged;
        }

        protected Nourishment(Nourishment other) : base(other)
        {
            Properties = new ObservableCollection<NourishmentProperty>(other.Properties);

            Properties.CollectionChanged += Properties_CollectionChanged;
        }
        //

        // methods
        public Nourishment Clone() => new Nourishment(this);

        public override void ConstructNote()
        {
            string note = "";
            foreach (NourishmentProperty property in Properties)
            {
                note += property.Name + ". ";
            }
            Name.Note = note;
        }

        public void AddProperty(object obj)
        {
            if (obj is NourishmentProperty propertyToAdd)
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
            if (obj is NourishmentProperty propertyToRemove)
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
        //

        // members
        public ObservableCollection<NourishmentProperty> Properties { get; set; }
        //
    }

    class NourishmentProperty : LabeledString
    {
        // constructor
        public NourishmentProperty(string name = "NewNourishmentProperty", string value = "") : base(name, value, value) { }
        //
    }
}
