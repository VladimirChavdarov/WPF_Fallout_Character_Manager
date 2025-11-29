using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        }
        //

        // methods

        //

        // data

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
        //

        // members
        public ObservableCollection<ChemProperty> Properties { get; set; }
        //

        // methods
        public Chem Clone => new Chem
        {
            Name = this.Name,
            Cost = this.Cost,
            Load = this.Load,
            Properties = new ObservableCollection<ChemProperty>(this.Properties),
        };
        //
    }

    class ChemProperty : LabeledString
    {
        // constructor
        public ChemProperty(string name = "NewChemProperty", string value = "") : base(name, value, value) { }
        //
    }
}
