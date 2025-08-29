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
    class ConditionsViewModel : ViewModelBase
    {
        // local variables
        private XtrnlLimbConditionsModel _xtrnlLimbConditions;
        private LimbConditionsModel _limbConditions;
        //

        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel
        {
            get => _xtrnlLimbConditions;
            set => Update(ref _xtrnlLimbConditions, value);
        }

        public LimbConditionsModel LimbConditionsModel
        {
            get => _limbConditions;
            set => Update(ref _limbConditions, value);
        }

        // constructor
        public ConditionsViewModel(XtrnlLimbConditionsModel? xtrnlLimbConditionsModel, LimbConditionsModel limbConditions)
        {
            _xtrnlLimbConditions = xtrnlLimbConditionsModel;
            LimbConditionsModel = limbConditions;
        }
        //
    }
}
