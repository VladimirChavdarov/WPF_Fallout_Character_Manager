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
    // Everything this class has is moved to the WeaponsViewModel because Ammo and Weapons are closely related to each-other
    //class AmmoViewModel : ViewModelBase
    //{
    //    // local variables
    //    XtrnlAmmoModel _xtrnlAmmoModel;
    //    AmmoModel _ammoModel;
    //    //

    //    // public variables
    //    XtrnlAmmoModel XtrnlAmmoModel
    //    {
    //        get => _xtrnlAmmoModel;
    //        set => Update(ref _xtrnlAmmoModel, value);
    //    }

    //    AmmoModel AmmoModel
    //    {
    //        get => _ammoModel;
    //        set => Update(ref _ammoModel, value);
    //    }
    //    //

    //    // constructor
    //    public AmmoViewModel(XtrnlAmmoModel xtrnlAmmoModel, AmmoModel ammoModel)
    //    {
    //        _xtrnlAmmoModel = xtrnlAmmoModel;
    //        AmmoModel = ammoModel;

    //        // testing code
    //        //Ammo xtrnlA1 = xtrnlAmmoModel.Ammos.FirstOrDefault(x => x.Name.BaseValue == ".308");
    //        //AmmoEffect xtrnlAE1 = xtrnlAmmoModel.AmmoEffects.FirstOrDefault(x => x.Name == "Armor Piercing");
    //        //xtrnlA1.Effects.Add(xtrnlAE1);
    //        //Ammo a1 = xtrnlA1.Clone();
    //        //AmmoEffect ae1 = xtrnlAmmoModel.AmmoEffects.FirstOrDefault(x => x.Name == "Match");
    //        //a1.Effects.Add(ae1);
    //        //a1.Amount.BaseValue += 10;
    //        //
    //    }
    //    //
    //}
}
