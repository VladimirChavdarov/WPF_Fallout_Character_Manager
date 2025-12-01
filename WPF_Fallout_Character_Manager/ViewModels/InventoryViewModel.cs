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

        private XtrnlAidModel _xtrnlChemsModel;
        private XtrnlExplosivesModel _xtrnlExplosivesModel;
        private XtrnlNourishmentModel _xtrnlNourishmentModel;
        private XtrnlGearModel _xtrnlGearModel;
        //

        // public variables
        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
            set => Update(ref _inventoryModel, value);
        }

        public XtrnlAidModel XtrnlChemsModel
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

        public XtrnlGearModel XtrnlGearModel
        {
            get => _xtrnlGearModel;
            set => Update(ref _xtrnlGearModel, value);
        }
        //

        // constructor
        public InventoryViewModel(
            InventoryModel inventoryModel,
            XtrnlAidModel xtrnlChemsModel,
            XtrnlExplosivesModel xtrnlExplosivesModel,
            XtrnlNourishmentModel xtrnlNourishmentModel,
            XtrnlGearModel xtrnlGearModel)
        {
            _inventoryModel = inventoryModel;
            _xtrnlChemsModel = xtrnlChemsModel;
            _xtrnlExplosivesModel = xtrnlExplosivesModel;
            _xtrnlExplosivesModel = xtrnlExplosivesModel;
            _xtrnlNourishmentModel = xtrnlNourishmentModel;
            _xtrnlGearModel = xtrnlGearModel;
        }
        //
    }
}
