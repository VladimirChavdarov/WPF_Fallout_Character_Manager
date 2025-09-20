using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class AmmoViewModel : ViewModelBase
    {
        // local variables
        XtrnlAmmoModel _xtrnlAmmoModel;
        //

        // public variables
        XtrnlAmmoModel XtrnlAmmoModel
        {
            get => _xtrnlAmmoModel;
            set => Update(ref _xtrnlAmmoModel, value);
        }
        //

        // constructor
        public AmmoViewModel(XtrnlAmmoModel xtrnlAmmoModel)
        {
            _xtrnlAmmoModel = xtrnlAmmoModel;
        }
        //
    }
}
