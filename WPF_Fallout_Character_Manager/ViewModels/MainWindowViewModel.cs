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
    internal class MainWindowViewModel : ViewModelBase
    {
        // External Data Models
        public XtrnlLevelModel XtrnlLevelModel { get; } = new XtrnlLevelModel();
        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel { get; } = new XtrnlLimbConditionsModel();
        public XtrnlConditionsModel XtrnlConditionsModel { get; } = new XtrnlConditionsModel();
        public XtrnlAmmoModel XtrnlAmmoModel { get; } = new XtrnlAmmoModel();
        public XtrnlWeaponsModel XtrnlWeaponsModel { get; } // set in constructor
        public XtrnlArmorModel XtrnlArmorModel { get; } = new XtrnlArmorModel();
        public XtrnlAidModel XtrnlChemsModel { get; } = new XtrnlAidModel();
        public XtrnlExplosivesModel XtrnlExplosivesModel { get; } = new XtrnlExplosivesModel();
        public XtrnlNourishmentModel XtrnlNourishmentModel { get; } = new XtrnlNourishmentModel();
        public XtrnlGearModel XtrnlGearModel { get; } = new XtrnlGearModel();
        //

        // All Models
        public SPECIALModel SPECIALModel { get; } = new SPECIALModel();
        public SurvivalModel SurvivalModel { get; } = new SurvivalModel();
        public CombatModel CombatModel { get; } = new CombatModel();
        public SkillModel SkillModel { get; } = new SkillModel();
        public LimbConditionsModel LimbConditionsModel { get; } = new LimbConditionsModel(); 
        public ConditionsModel ConditionsModel { get; } = new ConditionsModel();
        public AmmoModel AmmoModel { get; } = new AmmoModel();
        public InventoryModel InventoryModel { get; } = new InventoryModel();

        public BioModel BioModel { get; }
        public WeaponsModel WeaponsModel { get; }
        public ArmorModel ArmorModel { get; }
        //

        // All ViewModels
        public BioViewModel? BioViewModel { get; }
        public SPECIALViewModel? SPECIALViewModel { get; }
        public SurvivalViewModel? SurvivalViewModel { get; }
        public CombatViewModel CombatViewModel { get; }
        public SkillViewModel SkillViewModel { get; }
        public LimbConditionsViewModel LimbConditionsViewModel { get; }
        public ConditionsViewModel ConditionsViewModel { get; }
        public WeaponsViewModel WeaponsViewModel { get; }
        public ArmorViewModel ArmorViewModel { get; }
        public EquippableViewModel EquippableViewModel { get; }
        public InventoryViewModel InventoryViewModel { get; }
        //

        // Constructor
        public MainWindowViewModel()
        {
            XtrnlWeaponsModel = new XtrnlWeaponsModel(XtrnlAmmoModel);

            BioModel = new BioModel(XtrnlLevelModel);
            WeaponsModel = new WeaponsModel(XtrnlWeaponsModel);
            ArmorModel = new ArmorModel(XtrnlArmorModel);

            BioViewModel = new BioViewModel(BioModel);
            SPECIALViewModel = new SPECIALViewModel(SPECIALModel);
            SurvivalViewModel = new SurvivalViewModel(SurvivalModel, SPECIALModel);
            CombatViewModel = new CombatViewModel(CombatModel, SPECIALModel, BioModel, ArmorModel);
            SkillViewModel = new SkillViewModel(SkillModel, SPECIALModel, BioModel);
            LimbConditionsViewModel = new LimbConditionsViewModel(XtrnlLimbConditionsModel, LimbConditionsModel);
            ConditionsViewModel = new ConditionsViewModel(XtrnlConditionsModel, ConditionsModel);
            WeaponsViewModel = new WeaponsViewModel(XtrnlWeaponsModel, WeaponsModel, XtrnlAmmoModel, AmmoModel, SkillModel);
            ArmorViewModel = new ArmorViewModel(XtrnlArmorModel, ArmorModel);
            EquippableViewModel = new EquippableViewModel(WeaponsViewModel, ArmorViewModel);
            InventoryViewModel = new InventoryViewModel(
                                     InventoryModel,
                                     XtrnlChemsModel,
                                     XtrnlExplosivesModel,
                                     XtrnlNourishmentModel,
                                     XtrnlGearModel);
        }
        //
    }
}
