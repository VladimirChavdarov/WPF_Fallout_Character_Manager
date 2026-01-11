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
    class PerksViewModel : ViewModelBase
    {
        private XtrnlPerksModel _xtrnlPerksModel;
        public XtrnlPerksModel XtrnlPerksModel
        {
            get => _xtrnlPerksModel;
        }

        private PerksModel _perksModel;
        public PerksModel PerksModel
        {
            get => _perksModel;
        }

        // constructor
        public PerksViewModel(XtrnlPerksModel xtrnlPerksModel, PerksModel perksModel)
        {
            _xtrnlPerksModel = xtrnlPerksModel;
            _perksModel = perksModel;
        }
        //

        // methods

        //

        // commands

        //
    }
}
