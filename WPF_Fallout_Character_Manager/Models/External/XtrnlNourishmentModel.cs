using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlNourishmentModel : ModelBase
    {
        private static readonly string nourishmentPropertiesPath = "Resources/Spreadsheets/foods_drinks_properties.csv";
        private static readonly string nourishmentPath = "Resources/Spreadsheets/foods_drinks.csv";

        // constructor
        public XtrnlNourishmentModel()
        {
            Nourishments = new ObservableCollection<Nourishment>();
            NourishmentProperties = new ObservableCollection<NourishmentProperty>();

            Utils.UpdateCSVFilesIdFields(nourishmentPropertiesPath);

            var nourishmentPropertiesLines = File.ReadAllLines(nourishmentPropertiesPath);
            foreach (var line in nourishmentPropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                Utils.IdFromString(parts[2], out Guid id);

                NourishmentProperty nourishmentProperty = new NourishmentProperty(
                    id,
                    name: parts[0],
                    value: parts[1]
                    );
                NourishmentProperties.Add(nourishmentProperty);
            }

            var nourishmentLines = File.ReadAllLines(nourishmentPath);
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
        public static ObservableCollection<Nourishment> Nourishments { get; set; }
        public static ObservableCollection<NourishmentProperty> NourishmentProperties { get; set; }
        //
    }

    // encompases foods and drinks
    class Nourishment : Item
    {
        // constructor
        public Nourishment(string name = "NewNourishment", int cost = 0, float load = 0.0f) : base(name, cost, 0, load)
        {
            Properties = new ObservableCollection<NourishmentProperty>();

            SubscribeToPropertyChanged();
        }

        protected Nourishment(Nourishment other) : base(other)
        {
            Properties = new ObservableCollection<NourishmentProperty>(other.Properties);

            SubscribeToPropertyChanged();
        }

        public Nourishment(NourishmentDTO dto)
        {
            Properties = new ObservableCollection<NourishmentProperty>();
            FromDto(dto);

            SubscribeToPropertyChanged();
        }
        //

        // methods
        public Nourishment Clone() => new Nourishment(this);

        public void SubscribeToPropertyChanged()
        {
            Properties.CollectionChanged -= Properties_CollectionChanged;
            Properties.CollectionChanged += Properties_CollectionChanged;
        }

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

        public override ItemDTO ToDto()
        {
            NourishmentDTO result = new NourishmentDTO();

            UpdateItemDTO(result);

            foreach(var property in Properties)
            {
                result.PropertyIds.Add(property.Id);
            }

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            var typedDto = Utils.EnsureDtoType<NourishmentDTO>(dto);

            base.FromDto(dto, versionMismatch);

            foreach (Guid id in typedDto.PropertyIds)
            {
                NourishmentProperty property = XtrnlNourishmentModel.NourishmentProperties.FirstOrDefault(x => x.Id == id);
                if (property != null)
                {
                    Properties.Add(property);
                }
                else
                {
                    throw new ArgumentException("Couldn't find this nourishment property in the master list");
                }
            }

            SubscribeToPropertyChanged();
        }
        //

        // members
        public ObservableCollection<NourishmentProperty> Properties { get; set; }
        //
    }

    class NourishmentProperty : ItemAttribute
    {
        // constructor
        public NourishmentProperty(Guid id, string name = "NewNourishmentProperty", string value = "") : base(id, name, value) { }
        //
    }
}
