using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlAidModel : ModelBase
    {
        private static string aidPropertiesPath = "Resources/Spreadsheets/chem_properties.csv";
        private static string chemsPath = "Resources/Spreadsheets/chems.csv";
        private static string medicinePath = "Resources/Spreadsheets/medicine.csv";

        // constructor
        public XtrnlAidModel()
        {
            AidItems = new ObservableCollection<Aid>();
            AidProperties = new ObservableCollection<AidProperty>();

            Utils.UpdateCSVFilesIdFields(aidPropertiesPath);

            // Chems
            var chemPropertiesLines = File.ReadAllLines(aidPropertiesPath);
            foreach (var line in chemPropertiesLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                Utils.IdFromString(parts[2], out Guid id);

                AidProperty chemProperty = new AidProperty(
                    id,
                    name: parts[0],
                    value: parts[1]
                    );
                AidProperties.Add(chemProperty);
            }

            var chemLines = File.ReadAllLines(chemsPath);
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

                chem.ConstructNote();

                AidItems.Add(chem);
            }
            //

            // Medicine
            var medicineLines = File.ReadAllLines(medicinePath);
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
        public static ObservableCollection<Aid> AidItems {  get; set; }
        public static ObservableCollection<AidProperty> AidProperties { get; set; }
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
            Description = new LabeledString("Description", description);

            SubscribeToPropertiesCollectionChange();
        }

        protected Aid(Aid other) : base(other)
        {
            Properties = new ObservableCollection<AidProperty>(other.Properties);
            Description = other.Description.Clone();

            SubscribeToPropertiesCollectionChange();
        }

        public Aid(AidDTO dto)
        {
            Properties = new ObservableCollection<AidProperty>();
            FromDto(dto);
        }
        //

        // methods
        public void SubscribeToPropertiesCollectionChange()
        {
            Description.PropertyChanged += Description_PropertyChanged;
            Properties.CollectionChanged += Properties_CollectionChanged;
        }

        public override void ConstructNote()
        {
            string note = Description.Value;
            if(note != "")
            {
                note += "\n\n";
            }
            foreach(AidProperty property in Properties)
            {
                note += property.Name + ". ";
            }
            Name.Note = note;
        }

        public void AddProperty(object obj)
        {
            if (obj is AidProperty propertyToAdd)
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
            if (obj is AidProperty propertyToRemove)
            {
                Properties.Remove(propertyToRemove);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public Aid Clone() => new Aid(this);

        private void Description_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ConstructNote();
        }

        private void Properties_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConstructNote();
        }

        public override ItemDTO ToDto()
        {
            AidDTO result = new AidDTO();

            UpdateItemDTO(result);

            result.Description = Description;

            foreach(AidProperty property in Properties)
            {
                result.PropertiesIds.Add(property.Id);
            }

            return result;
        }

        public override void FromDto(ItemDTO dto, bool versionMismatch = false)
        {
            var typedDto = Utils.EnsureDtoType<AidDTO>(dto);

            base.FromDto(dto, versionMismatch);

            Description = typedDto.Description;

            foreach(Guid id in typedDto.PropertiesIds)
            {
                AidProperty property = XtrnlAidModel.AidProperties.FirstOrDefault(x => x.Id == id);
                if (property != null)
                {
                    Properties.Add(property);
                }
                else
                {
                    Properties.Add(new AidProperty(Guid.Empty, Globals.INVALID_PROPERTY, Globals.INVALID_ATTRIBUTE_DESCRIPTION));
                    //throw new ArgumentException("Couldn't find this aid property in the master list");
                }
            }

            SubscribeToPropertiesCollectionChange();
        }
        //

        // members
        private LabeledString _description;
        public LabeledString Description
        {
            get => _description;
            set => Update(ref _description, value);
        }

        public ObservableCollection<AidProperty> Properties { get; set; }
        //
    }

    class AidProperty : ItemAttribute
    {
        // constructor
        public AidProperty(Guid id, string name = "NewAidProperty", string value = "") : base(id, name, value) { }
        //
    }
}
