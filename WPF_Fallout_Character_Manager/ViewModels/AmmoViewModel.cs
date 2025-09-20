﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class AmmoViewModel : ViewModelBase
    {
        // local variables
        XtrnlAmmoModel _xtrnlAmmoModel;
        AmmoModel _ammoModel;
        //

        // public variables
        XtrnlAmmoModel XtrnlAmmoModel
        {
            get => _xtrnlAmmoModel;
            set => Update(ref _xtrnlAmmoModel, value);
        }

        AmmoModel AmmoModel
        {
            get => _ammoModel;
            set => Update(ref _ammoModel, value);
        }
        //

        // constructor
        public AmmoViewModel(XtrnlAmmoModel xtrnlAmmoModel, AmmoModel ammoModel)
        {
            _xtrnlAmmoModel = xtrnlAmmoModel;
            AmmoModel = ammoModel;
        }
        //
    }
}
