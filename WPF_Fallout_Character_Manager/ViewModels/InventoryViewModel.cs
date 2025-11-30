using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class InventoryViewModel : ViewModelBase
    {
        // local variables
        private InventoryModel _inventoryModel;

        private XtrnlChemsModel _xtrnlChemsModel;
        //

        // public variables
        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
            set => Update(ref _inventoryModel, value);
        }

        public XtrnlChemsModel XtrnlChemsModel
        {
            get => _xtrnlChemsModel;
            set => Update(ref _xtrnlChemsModel, value);
        }
        //

        // constructor
        public InventoryViewModel(InventoryModel inventoryModel, XtrnlChemsModel xtrnlChemsModel)
        {
            _inventoryModel = inventoryModel;
            _xtrnlChemsModel = xtrnlChemsModel;
        }
        //
    }
}
