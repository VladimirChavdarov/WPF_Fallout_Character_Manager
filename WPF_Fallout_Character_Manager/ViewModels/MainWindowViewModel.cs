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
        // All Models
        public BioModel BioModel { get; } = new BioModel();
        public SPECIALModel SPECIALModel { get; } = new SPECIALModel();
        public SurvivalModel SurvivalModel { get; } = new SurvivalModel();
        public CombatModel CombatModel { get; } = new CombatModel();
        public SkillModel SkillModel { get; } = new SkillModel();
        public LimbConditionsModel LimbConditionsModel { get; } = new LimbConditionsModel(); 
        public ConditionsModel ConditionsModel { get; } = new ConditionsModel();
        public AmmoModel AmmoModel { get; } = new AmmoModel();

        //public WeaponsModel WeaponsModel { get; } = new WeaponsModel();
        public WeaponsModel WeaponsModel { get; }
        //

        // External Data Models
        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel { get; } = new XtrnlLimbConditionsModel();
        public XtrnlConditionsModel XtrnlConditionsModel { get; } = new XtrnlConditionsModel();
        public XtrnlAmmoModel XtrnlAmmoModel { get; } = new XtrnlAmmoModel();
        public XtrnlWeaponsModel XtrnlWeaponsModel { get; } // set in constructor
        //

        // All ViewModels
        public BioViewModel? BioViewModel { get; }
        public SPECIALViewModel? SPECIALViewModel { get; }
        public SurvivalViewModel? SurvivalViewModel { get; }
        public CombatViewModel CombatViewModel { get; }
        public SkillViewModel SkillViewModel { get; }
        public LimbConditionsViewModel LimbConditionsViewModel { get; }
        public ConditionsViewModel ConditionsViewModel { get; }
        public AmmoViewModel AmmoViewModel { get; }
        public WeaponsViewModel WeaponsViewModel { get; }

        public EquippableViewModel EquippableViewModel { get; }
        //

        // Constructor
        public MainWindowViewModel()
        {
            XtrnlWeaponsModel = new XtrnlWeaponsModel(XtrnlAmmoModel, AmmoModel);
            WeaponsModel = new WeaponsModel(XtrnlWeaponsModel);

            BioViewModel = new BioViewModel(BioModel);
            SPECIALViewModel = new SPECIALViewModel(SPECIALModel);
            SurvivalViewModel = new SurvivalViewModel(SurvivalModel, SPECIALModel);
            CombatViewModel = new CombatViewModel(CombatModel, SPECIALModel, BioModel);
            SkillViewModel = new SkillViewModel(SkillModel, SPECIALModel);
            LimbConditionsViewModel = new LimbConditionsViewModel(XtrnlLimbConditionsModel, LimbConditionsModel);
            ConditionsViewModel = new ConditionsViewModel(XtrnlConditionsModel, ConditionsModel);
            AmmoViewModel = new AmmoViewModel(XtrnlAmmoModel, AmmoModel);
            WeaponsViewModel = new WeaponsViewModel(XtrnlWeaponsModel, WeaponsModel);
            EquippableViewModel = new EquippableViewModel(WeaponsViewModel);
        }
        //
    }
}
