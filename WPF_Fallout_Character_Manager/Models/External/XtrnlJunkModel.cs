using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
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

                string note = componentItem.Load.Total + " load\n";
                foreach (JunkComponent component in componentItem.Components)
                {
                    note += "x" + component.Amount.Total + " " + component.Name.Total + ", ";
                }
                if (note != "")
                {
                    note = note.Remove(note.Length - 2);
                }
                componentItem.Name.Note = note;

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

                string note = junk.Load.Total + " load\n";
                foreach(JunkComponent component in junk.Components)
                {
                    note += "x" + component.Amount.Total + " " + component.Name.Total + ", ";
                }
                if(note != "")
                {
                    note = note.Remove(note.Length - 2);
                }
                junk.Name.Note = note;

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

                JunkComponent componentRef = JunkComponents.FirstOrDefault(x => x.Name.BaseValue.Contains(componentName, StringComparison.InvariantCultureIgnoreCase));
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
        }

        public Junk(Junk other) : base(other)
        {
            Components = new ObservableCollection<JunkComponent>();
            foreach(JunkComponent otherComponent in other.Components)
            {
                Components.Add(otherComponent.Clone());
            }
        }
        //

        // methods
        public Junk Clone() => new Junk(this);
        //

        // members
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
