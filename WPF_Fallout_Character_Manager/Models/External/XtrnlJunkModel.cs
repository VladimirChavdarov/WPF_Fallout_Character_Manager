using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlJunkModel : ModelBase
    {
        // constructor
        public XtrnlJunkModel()
        {
            JunkItems = new ObservableCollection<Junk>();
            JunkComponents = new ObservableCollection<JunkComponent>();

            var junkComponentLines = File.ReadAllLines("Resources/Spreadsheets/junk_components.csv");
            foreach (var line in junkComponentLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                JunkComponent junkComponent = new JunkComponent(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: (float)Int32.Parse(parts[2]) / 10.0f
                    );

                JunkComponents.Add(junkComponent);

                Junk componentItem = new Junk(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: (float)Int32.Parse(parts[2]) / 10.0f
                    );
                SetComponentsOfJunk(componentItem, "x1 " + junkComponent.Name.Total);

                componentItem.ConstructNote();

                JunkItems.Add(componentItem);
            }

            var junkLines = File.ReadAllLines("Resources/Spreadsheets/junk.csv");
            foreach (var line in junkLines.Skip(1))
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                    continue;

                Junk junk = new Junk(
                    name: parts[0],
                    cost: Int32.Parse(parts[1]),
                    load: float.Parse(parts[2], CultureInfo.InvariantCulture.NumberFormat),
                    description: parts[4]
                    );

                SetComponentsOfJunk(junk, parts[3]);

                junk.ConstructNote();

                JunkItems.Add(junk);
            }
        }
        //

        // methods
        public void SetComponentsOfJunk(Junk junk, string componentsLine)
        {
            string[] componentStrings = componentsLine.Split(", ");
            foreach (string s in componentStrings)
            {
                string componentString = s;
                componentString = componentString.Replace("x", "");
                string amountString = Utils.Between(componentString, "", " ");
                componentString = componentString.Replace(amountString, "");
                string componentName = componentString.Trim();

                //JunkComponent componentRef = JunkComponents.FirstOrDefault(x => x.Name.BaseValue.Contains(componentName, StringComparison.InvariantCultureIgnoreCase));
                JunkComponent componentRef = JunkComponents.FirstOrDefault(x => x.Name.BaseValue.Equals(componentName, StringComparison.InvariantCultureIgnoreCase));
                if (componentRef != null)
                {
                    JunkComponent componentToAdd = componentRef.Clone();
                    componentToAdd.Amount.BaseValue = Int32.Parse(amountString);
                    junk.Components.Add(componentToAdd);
                }
            }
        }
        //

        // data
        public ObservableCollection<Junk> JunkItems { get; set; }
        public ObservableCollection<JunkComponent> JunkComponents { get; set; }
        //
    }

    class Junk : Item
    {
        // constructor
        public Junk(string name = "NewJunkItem", int cost = 0, float load = 0.0f, string description = "") : base(name, cost, 0, load, description)
        {
            Components = new ObservableCollection<JunkComponent>();

            Components.CollectionChanged += Components_CollectionChanged;
            foreach(JunkComponent component in Components)
            {
                component.PropertyChanged += Component_PropertyChanged;
            }
        }

        public Junk(Junk other) : base(other)
        {
            Components = new ObservableCollection<JunkComponent>();
            foreach(JunkComponent otherComponent in other.Components)
            {
                Components.Add(otherComponent.Clone());
            }
            foreach (JunkComponent component in Components)
            {
                component.PropertyChanged += Component_PropertyChanged;
            }

            Components.CollectionChanged += Components_CollectionChanged;
            ConstructNote();
        }
        //

        // methods
        public Junk Clone() => new Junk(this);

        public override void ConstructNote()
        {
            string note = "";
            foreach (JunkComponent component in Components)
            {
                note += "x" + component.Amount.Total + " " + component.Name.Total + ", ";
            }
            if (note != "")
            {
                note = note.Remove(note.Length - 2);
            }
            Name.Note = note;
        }

        // NOTE: This method doesn't delete the junk item. If you want this, you need to handle it separately.
        public List<Junk> ScrapJunkItem()
        {
            List<Junk> result = new List<Junk>();
            foreach (JunkComponent junkComponent in Components)
            {
                Junk newJunkItem = new Junk(junkComponent.Name.Total, junkComponent.Cost.Total, junkComponent.Load.Total);
                newJunkItem.Amount = junkComponent.Amount.Clone();
                newJunkItem.Amount.BaseValue *= this.Amount.Total;
                JunkComponent component = junkComponent.Clone();
                component.Amount.BaseValue = 1;
                newJunkItem.Components.Add(component);
                result.Add(newJunkItem);
            }

            return result;
        }

        public void AddProperty(object obj)
        {
            if (obj is JunkComponent componentToAdd)
            {
                JunkComponent c = componentToAdd.Clone();
                c.PropertyChanged += Component_PropertyChanged;
                c.CanBeEdited = true;
                Components.Add(c);
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        public void RemoveProperty(object obj)
        {
            if (obj is JunkComponent component)
            {
                JunkComponent componentToRemove = Components.First(x => x.NameString == component.NameString);
                if(componentToRemove != null)
                {
                    componentToRemove.PropertyChanged -= Component_PropertyChanged;
                    Components.Remove(componentToRemove);
                }
            }
            else
            {
                throw new ArgumentException("The argument cannot be cast to the correct type");
            }
        }

        private void Components_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConstructNote();
        }

        private void Component_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ConstructNote();
        }
        //

        // members
        public override bool CanBeEdited
        {
            get => base.CanBeEdited;
            set
            {
                base.CanBeEdited = value;
                if(Components != null)
                {
                    foreach(JunkComponent component in Components)
                    {
                        component.CanBeEdited = value;
                    }
                }
            }
        }

        public ObservableCollection<JunkComponent> Components { get; set; }
        //
    }

    class JunkComponent : Item
    {
        // constructor
        public JunkComponent(string name = "NewJunkComponent", int cost = 0, float load = 0.0f) : base(name, cost, 0, load) { }
        
        public JunkComponent(JunkComponent other) : base(other) { }
        //

        // methods
        public JunkComponent Clone() => new JunkComponent(this);
        //
    }
}
