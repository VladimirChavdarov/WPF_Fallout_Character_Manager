using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Serialization;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    /// <summary>
    /// This is the entry-point of the program. The main ViewModel contains the initial instances of all other Models and ViewModels.
    /// I also use this ViewModel to handle the serialization of the character and all catalogues.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        // External Data Models
        public XtrnlLevelModel XtrnlLevelModel { get; } = new XtrnlLevelModel();
        public XtrnlLimbConditionsModel XtrnlLimbConditionsModel { get; } = new XtrnlLimbConditionsModel();
        public XtrnlConditionsModel XtrnlConditionsModel { get; } = new XtrnlConditionsModel();
        public XtrnlAmmoModel XtrnlAmmoModel { get; } = new XtrnlAmmoModel();
        public XtrnlWeaponsModel XtrnlWeaponsModel { get; } // set in constructor
        public XtrnlArmorModel XtrnlArmorModel { get; } = new XtrnlArmorModel();
        public XtrnlAidModel XtrnlAidModel { get; } = new XtrnlAidModel();
        public XtrnlExplosivesModel XtrnlExplosivesModel { get; } = new XtrnlExplosivesModel();
        public XtrnlNourishmentModel XtrnlNourishmentModel { get; } = new XtrnlNourishmentModel();
        public XtrnlGearModel XtrnlGearModel { get; } = new XtrnlGearModel();
        public XtrnlJunkModel XtrnlJunkModel { get; } = new XtrnlJunkModel();
        public XtrnlPerksModel XtrnlPerksModel { get; } = new XtrnlPerksModel();
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
        public PerksModel PerksModel { get; } = new PerksModel();

        public BioModel BioModel { get; private set; }
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
        public JunkManagerViewModel JunkManagerViewModel { get; }
        public PerksViewModel PerksViewModel { get; }
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
                                     WeaponsViewModel,
                                     ArmorViewModel,

                                     InventoryModel,
                                     WeaponsModel,
                                     ArmorModel,
                                     AmmoModel,

                                     XtrnlWeaponsModel,
                                     XtrnlArmorModel,
                                     XtrnlAmmoModel,
                                     XtrnlAidModel,
                                     XtrnlExplosivesModel,
                                     XtrnlNourishmentModel,
                                     XtrnlGearModel,
                                     XtrnlJunkModel,
                                     SPECIALModel);

            JunkManagerViewModel = new JunkManagerViewModel(XtrnlJunkModel, InventoryModel);

            PerksViewModel = new PerksViewModel(XtrnlPerksModel, PerksModel);

            // serialization
            SaveCharacterCommand = new RelayCommand(SaveCharacter);
            LoadCharacterCommand = new RelayCommand(LoadCharacter);
            //
        }
        //

        // serialization
        void SerializeCharacterJson()
        {
            BioModelDTO dto = BioModel.ToDto();

            string json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("test_character.json", json);
        }

        void DeserializeCharacterJson()
        {
            string json = File.ReadAllText("test_character.json");

            BioModelDTO dto = JsonSerializer.Deserialize<BioModelDTO>(json);
            BioModel.FromDto(dto);
            //BioViewModel.BioModel = new BioModel(dto, XtrnlLevelModel);
        }

        public RelayCommand SaveCharacterCommand { get; private set; }
        private void SaveCharacter(object _ = null)
        {
            SerializeCharacterJson();
        }

        public RelayCommand LoadCharacterCommand { get; private set; }
        private void LoadCharacter(object _ = null)
        {
            DeserializeCharacterJson();
        }
        //
    }
}
