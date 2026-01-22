using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.External.Serialization;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.ViewModels.Serialization;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    public enum DtoType
    {
        Character,
        Catalogue,
    }

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

        // serialization
        private readonly Dictionary<Type, object> _serializableModels = new();
        private readonly Dictionary<Type, object> _serializableXtrnlModels = new();
        private readonly Dictionary<DtoType, string> _serializationFilePaths = new Dictionary<DtoType, string>
        {
            { DtoType.Character, "test_character.json" },
            { DtoType.Catalogue, "test_catalogue.json" }
        };
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
            RegisterModelForSerialization<BioModel, BioModelDTO>(BioModel, DtoType.Character);
            RegisterModelForSerialization<SurvivalModel, SurvivalModelDTO>(SurvivalModel, DtoType.Character);
            RegisterModelForSerialization<SPECIALModel, SPECIALModelDTO>(SPECIALModel, DtoType.Character);
            RegisterModelForSerialization<CombatModel, CombatModelDTO>(CombatModel, DtoType.Character);
            RegisterModelForSerialization<SkillModel, SkillModelDTO>(SkillModel, DtoType.Character);
            RegisterModelForSerialization<LimbConditionsModel, LimbConditionsModelDTO>(LimbConditionsModel, DtoType.Character);
            RegisterModelForSerialization<ConditionsModel, ConditionsModelDTO>(ConditionsModel, DtoType.Character);

            RegisterModelForSerialization<XtrnlWeaponsModel, XtrnlWeaponsModelDTO>(XtrnlWeaponsModel, DtoType.Catalogue);
            //
        }
        //

        // serialization
        private void RegisterModelForSerialization<TModel, TDto>(TModel model, DtoType type) where TModel : ISerializable<TDto>
        {
            if(type == DtoType.Character)
                _serializableModels[typeof(TModel)] = model;
            else if(type == DtoType.Catalogue)
            {
                _serializableXtrnlModels[typeof(TModel)] = model;
            }
        }

        CharacterDTO CreateCharacterDto()
        {
            var characterDto = new CharacterDTO();
            foreach(var (modelKey, modelVal) in _serializableModels)
            {
                var method = modelVal.GetType().GetMethod("ToDto");
                var dtoValue = method.Invoke(modelVal, null);

                characterDto.ModelDtos[modelKey.Name] = JsonSerializer.SerializeToElement(dtoValue);
            }

            return characterDto;
        }

        CatalogueDTO CreateCatalogueDTO()
        {
            var catalogueDto = new CatalogueDTO();
            foreach (var (modelKey, modelVal) in _serializableXtrnlModels)
            {
                var method = modelVal.GetType().GetMethod("ToDto");
                var dtoValue = method.Invoke(modelVal, null);

                catalogueDto.XtrnlModelDtos[modelKey.Name] = JsonSerializer.SerializeToElement(dtoValue);
            }

            return catalogueDto;
        }

        bool SerializeCharacterJson()
        {
            CharacterDTO characterDto = CreateCharacterDto();
            string characterJson = JsonSerializer.Serialize(characterDto, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_serializationFilePaths[DtoType.Character], characterJson);

            return true;
        }

        bool SerializeCatalogueJson()
        {
            CatalogueDTO catalogueDto = CreateCatalogueDTO();
            string catalogueJson = JsonSerializer.Serialize(catalogueDto, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_serializationFilePaths[DtoType.Catalogue], catalogueJson);

            return true;
        }

        bool DeserializeCharacterJson()
        {
            string json = File.ReadAllText(_serializationFilePaths[DtoType.Character]);
            CharacterDTO dto = new CharacterDTO();
            float appVersion = dto.Version;
            dto = JsonSerializer.Deserialize<CharacterDTO>(json);
            bool versionMisMatch = appVersion != dto.Version;

            foreach(var (modelName, jsonElement) in dto.ModelDtos)
            {
                if (!_serializableModels.TryGetValue(_serializableModels.Keys.First(t => t.Name == modelName), out var model))
                {
                    return false;
                }

                var fromDtoMethod = model.GetType().GetMethod("FromDto");
                var dtoType = fromDtoMethod.GetParameters()[0].ParameterType;

                var typedDto = jsonElement.Deserialize(dtoType);
                fromDtoMethod.Invoke(model, new[] { typedDto, versionMisMatch });
            }

            return true;
        }

        bool DeserializeCatalogueJson()
        {
            string json = File.ReadAllText(_serializationFilePaths[DtoType.Catalogue]);
            CatalogueDTO dto = new CatalogueDTO();
            float appVersion = dto.Version;
            dto = JsonSerializer.Deserialize<CatalogueDTO>(json);
            bool versionMisMatch = appVersion != dto.Version;

            foreach (var (modelName, jsonElement) in dto.XtrnlModelDtos)
            {
                if (!_serializableXtrnlModels.TryGetValue(_serializableXtrnlModels.Keys.First(t => t.Name == modelName), out var model))
                {
                    return false;
                }

                var fromDtoMethod = model.GetType().GetMethod("FromDto");
                var dtoType = fromDtoMethod.GetParameters()[0].ParameterType;

                var typedDto = jsonElement.Deserialize(dtoType);
                fromDtoMethod.Invoke(model, new[] { typedDto, versionMisMatch });
            }

            return true;
        }

        private void PostLoadUpdate()
        {
            SurvivalModel.UpdateModel(SPECIALModel);
            CombatModel.UpdateModel(SPECIALModel, BioModel.Level);
            SkillModel.UpdateModel(SPECIALModel);

            InventoryViewModel.ReloadCatalogueAndInventory();
        }

        public bool SaveCharacter(object _ = null)
        {
            if (!SerializeCatalogueJson())
            {
                return false;
            }

            if(!SerializeCharacterJson())
            {
                return false;
            }

            return true;
        }

        public bool LoadCharacter(object _ = null)
        {
            if (!DeserializeCatalogueJson())
            {
                return false;
            }

            if (!DeserializeCharacterJson())
            {
                return false;
            }

            PostLoadUpdate();

            return true;
        }
        //
    }
}
