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
        private XtrnlExplosivesModel _xtrnlExplosivesModel;
        private XtrnlNourishmentModel _xtrnlNourishmentModel;
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

        public XtrnlExplosivesModel XtrnlExplosivesModel
        {
            get => _xtrnlExplosivesModel;
            set => Update(ref _xtrnlExplosivesModel, value);
        }

        public XtrnlNourishmentModel XtrnlNourishmentModel
        {
            get => _xtrnlNourishmentModel;
            set => Update(ref _xtrnlNourishmentModel, value);
        }
        //

        // constructor
        public InventoryViewModel(
            InventoryModel inventoryModel,
            XtrnlChemsModel xtrnlChemsModel,
            XtrnlExplosivesModel xtrnlExplosivesModel,
            XtrnlNourishmentModel xtrnlNourishmentModel)
        {
            _inventoryModel = inventoryModel;
            _xtrnlChemsModel = xtrnlChemsModel;
            _xtrnlExplosivesModel = xtrnlExplosivesModel;
            _xtrnlExplosivesModel = xtrnlExplosivesModel;
            _xtrnlNourishmentModel = xtrnlNourishmentModel;
        }
        //
    }
}
