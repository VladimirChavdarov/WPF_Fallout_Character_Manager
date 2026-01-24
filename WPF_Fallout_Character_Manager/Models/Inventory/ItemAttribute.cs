using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Models.Inventory
{
    // The children classes of this one are usually stuff like Properties and Upgrades. They are unique within the memory of the app,
    // the items simply reference the master list.
    // Because of the simplicity of this class and its parent, it can be serialized without a designated DTO.
    public class ItemAttribute : LabeledValue<string>
    {
        // constructor
        public ItemAttribute(Guid id, string name = "NewItemAttribute", string value = "") : base(name, value, value)
        {
            Id = id;
            //if(generateIdOnInit)
            //{
            //    Id = Guid.NewGuid();
            //}
        }
        //

        // members
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => Update(ref _id, value); // NOTE: This has a public setter only because of serialization. DON'T SET MANUALLY!
        }

        private bool _isFromSpreadsheet = true;
        public bool IsFromSpreadsheet
        {
            get => _isFromSpreadsheet;
            set => Update(ref _isFromSpreadsheet, value);
        }
        //

        // methods
        
        //
    }
}
