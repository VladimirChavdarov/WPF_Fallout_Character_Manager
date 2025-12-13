using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class InventoryViewModel : ViewModelBase
    {
        // local variables
        private InventoryModel _inventoryModel;
        private WeaponsModel _weaponsModel;
        private ArmorModel _armorModel;
        private AmmoModel _ammoModel;

        private XtrnlWeaponsModel _xtrnlWeaponsModel;
        private XtrnlArmorModel _xtrnlArmorModel;
        private XtrnlAmmoModel _xtrnlAmmoModel;
        private XtrnlAidModel _xtrnlAidModel;
        private XtrnlExplosivesModel _xtrnlExplosivesModel;
        private XtrnlNourishmentModel _xtrnlNourishmentModel;
        private XtrnlGearModel _xtrnlGearModel;
        private XtrnlJunkModel _xtrnlJunkModel;

        private SPECIALModel _specialModel;

        private string _selectedCatalogueCategory;
        private string _searchCatalogueText;
        private string _selectedInventoryCategory;
        private string _searchInventoryText;
        private ObservableCollection<Item> Catalogue { get; } = new ObservableCollection<Item>();
        private ObservableCollection<Item> FullInventory { get; } = new ObservableCollection<Item>();

        private dynamic _itemToAddToInventory;
        private dynamic _selectedCatalogueItem;
        private dynamic _selectedInventoryItem;

        private Dictionary<Type, IList> _typeToCollectionMap { get; } = new Dictionary<Type, IList>();
        //

        // public variables
        public IReadOnlyList<string> Categories { get; } = new List<string>() { "All", "Weapon", "Armor", "Ammo", "Aid", "Explosive", "Nourishment", "Gear", "Junk" };
        public ICollectionView CatalogueView { get; }
        public ICollectionView FullInventoryView { get; }
        
        public string SelectedCatalogueCategory
        {
            get => _selectedCatalogueCategory;
            set
            {
                Update(ref _selectedCatalogueCategory, value);
                if(_selectedCatalogueCategory != null)
                {
                    RefreshCatalogueView();
                }
            }
        }
        public string SearchCatalogueText
        {
            get => _searchCatalogueText;
            set
            {
                Update(ref _searchCatalogueText, value);
                if (_searchCatalogueText != null)
                {
                    RefreshCatalogueView();
                }
            }
        }

        public string SelectedInventoryCategory
        {
            get => _selectedInventoryCategory;
            set
            {
                Update(ref _selectedInventoryCategory, value);
                if (_selectedInventoryCategory != null)
                {
                    RefreshInventoryView();
                }
            }
        }
        public string SearchInventoryText
        {
            get => _searchInventoryText;
            set
            {
                Update(ref _searchInventoryText, value);
                if (_searchInventoryText != null)
                {
                    RefreshInventoryView();
                }
            }
        }

        public dynamic ItemToAddToInventory
        {
            get => _itemToAddToInventory;
            set => Update(ref _itemToAddToInventory, value);
        }

        public dynamic SelectedCatalogueItem
        {
            get => _selectedCatalogueItem;
            set
            {
                Update(ref _selectedCatalogueItem, value);
                if (_selectedCatalogueItem != null)
                {
                    SelectedInventoryItem = null;
                }
            }
        }

        public dynamic SelectedInventoryItem
        {
            get => _selectedInventoryItem;
            set
            {
                Update(ref _selectedInventoryItem, value);
                if (_selectedInventoryItem != null)
                {
                    SelectedCatalogueItem = null;
                }
            }
        }

        public object SelectedItem => SelectedInventoryItem ?? SelectedCatalogueItem;

        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
            set => Update(ref _inventoryModel, value);
        }

        public WeaponsModel WeaponsModel
        {
            get => _weaponsModel;
        }

        public ArmorModel ArmorModel
        {
            get => _armorModel;
        }

        public AmmoModel AmmoModel
        {
            get => _ammoModel;
        }

        public XtrnlWeaponsModel XtrnlWeaponsModel
        {
            get => _xtrnlWeaponsModel;
        }

        public XtrnlArmorModel XtrnlArmorModel
        {
            get => _xtrnlArmorModel;
        }

        public XtrnlAmmoModel XtrnlAmmoModel
        {
            get => _xtrnlAmmoModel;
        }

        public XtrnlAidModel XtrnlAidModel
        {
            get => _xtrnlAidModel;
        }

        public XtrnlExplosivesModel XtrnlExplosivesModel
        {
            get => _xtrnlExplosivesModel;
        }

        public XtrnlNourishmentModel XtrnlNourishmentModel
        {
            get => _xtrnlNourishmentModel;
        }

        public XtrnlGearModel XtrnlGearModel
        {
            get => _xtrnlGearModel;
        }

        public XtrnlJunkModel XtrnlJunkModel
        {
            get => _xtrnlJunkModel;
        }
        //

        // constructor
        public InventoryViewModel(
            InventoryModel inventoryModel,
            WeaponsModel weaponsModel,
            ArmorModel armorModel,
            AmmoModel ammoModel,

            XtrnlWeaponsModel xtrnlWeaponsModel,
            XtrnlArmorModel xtrnlArmorModel,
            XtrnlAmmoModel xtrnlAmmoModel,
            XtrnlAidModel xtrnlAidModel,
            XtrnlExplosivesModel xtrnlExplosivesModel,
            XtrnlNourishmentModel xtrnlNourishmentModel,
            XtrnlGearModel xtrnlGearModel,
            XtrnlJunkModel xtrnlJunkModel,
            SPECIALModel specialModel)
        {
            _inventoryModel = inventoryModel;
            _weaponsModel = weaponsModel;
            _armorModel = armorModel;
            _ammoModel = ammoModel;

            _xtrnlWeaponsModel = xtrnlWeaponsModel;
            _xtrnlArmorModel = xtrnlArmorModel;
            _xtrnlAmmoModel = xtrnlAmmoModel;
            _xtrnlAidModel = xtrnlAidModel;
            _xtrnlExplosivesModel = xtrnlExplosivesModel;
            _xtrnlNourishmentModel = xtrnlNourishmentModel;
            _xtrnlGearModel = xtrnlGearModel;
            _xtrnlJunkModel = xtrnlJunkModel;

            _specialModel = specialModel;

            foreach (Item i in XtrnlWeaponsModel.Weapons) { Catalogue.Add(i); }
            foreach (Item i in XtrnlArmorModel.Armors) { Catalogue.Add(i); }
            foreach (Item i in XtrnlArmorModel.PowerArmors) { Catalogue.Add(i); }
            foreach (Item i in XtrnlAmmoModel.Ammos) { Catalogue.Add(i); }
            foreach (Item i in XtrnlAidModel.AidItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlExplosivesModel.Explosives) { Catalogue.Add(i); }
            foreach (Item i in XtrnlNourishmentModel.Nourishments) { Catalogue.Add(i); }
            foreach (Item i in XtrnlGearModel.GearItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlJunkModel.JunkItems) { Catalogue.Add(i); }

            foreach (Item i in WeaponsModel.Weapons) { FullInventory.Add(i); }
            foreach (Item i in ArmorModel.Armors) { FullInventory.Add(i); }
            foreach (Item i in ArmorModel.PowerArmors) { FullInventory.Add(i); }
            foreach (Item i in AmmoModel.Ammos) { FullInventory.Add(i); }
            foreach (Item i in InventoryModel.AidItems) { FullInventory.Add(i); }
            foreach (Item i in InventoryModel.Explosives) { FullInventory.Add(i); }
            foreach (Item i in InventoryModel.Nourishment) { FullInventory.Add(i); }
            foreach (Item i in InventoryModel.GearItems) { FullInventory.Add(i); }
            foreach (Item i in InventoryModel.JunkItems) { FullInventory.Add(i); }

            _typeToCollectionMap.Add(typeof(Weapon), WeaponsModel.Weapons);
            _typeToCollectionMap.Add(typeof(Armor), ArmorModel.Armors);
            _typeToCollectionMap.Add(typeof(PowerArmor), ArmorModel.PowerArmors);
            _typeToCollectionMap.Add(typeof(Ammo), AmmoModel.Ammos);
            _typeToCollectionMap.Add(typeof(Aid), InventoryModel.AidItems);
            _typeToCollectionMap.Add(typeof(Explosive), InventoryModel.Explosives);
            _typeToCollectionMap.Add(typeof(Nourishment), InventoryModel.Nourishment);
            _typeToCollectionMap.Add(typeof(Gear), InventoryModel.GearItems);
            _typeToCollectionMap.Add(typeof(Junk), InventoryModel.JunkItems);

            CatalogueView = CollectionViewSource.GetDefaultView(Catalogue);
            CatalogueView.SortDescriptions.Add(new SortDescription(nameof(Item.NameString), ListSortDirection.Ascending));

            FullInventoryView = CollectionViewSource.GetDefaultView(FullInventory);
            FullInventoryView.SortDescriptions.Add(new SortDescription(nameof(Item.NameString), ListSortDirection.Ascending));

            OpenAddToInventoryWindowCommand = new RelayCommand(OpenAddToInventoryWindow);
            AddToInventoryCommand = new RelayCommand(AddToInventory);

            _searchCatalogueText = "";
            SelectedCatalogueCategory = Categories.First();
            CatalogueView.Filter = FilterCatalogue;
            _searchInventoryText = "";
            SelectedInventoryCategory = Categories.First();
            FullInventoryView.Filter = FilterInventory;

            _specialModel.PropertyChanged += _specialModel_PropertyChanged;
            InventoryModel.CarryLoad.BaseValue = _specialModel.Strength.Total * 10.0f;

            FullInventory.CollectionChanged += FullInventory_CollectionChanged;
            CalculateCurrentLoad();
        }
        //

        // methods
        private void RefreshCatalogueView()
        {
            CatalogueView.Filter = FilterCatalogue;
        }
        private void RefreshInventoryView()
        {
            FullInventoryView.Filter = FilterInventory;
        }

        private bool FilterCatalogue(object obj)
        {
            return FilterItem(obj, SearchCatalogueText, SelectedCatalogueCategory);
        }
        private bool FilterInventory(object obj)
        {
            return FilterItem(obj, SearchInventoryText, SelectedInventoryCategory);
        }

        private bool FilterItem(object obj, string searchText, string selectedCategory)
        {
            if (obj is not Item item)
                return false;

            bool passSearch = searchText.Length == 0 || item.NameString.Contains(searchText, StringComparison.OrdinalIgnoreCase);

            if (!passSearch)
                return false;

            if (selectedCategory == "All")
                return true;

            if (obj.GetType().Name.Contains(selectedCategory))
                return true;

            return false;
        }

        private void CalculateCurrentLoad()
        {
            InventoryModel.CurrentLoad.BaseValue = 0.0f;
            foreach (Item item in FullInventory)
            {
                InventoryModel.CurrentLoad.BaseValue += item.TotalLoad;
            }
        }

        private void _specialModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SPECIALModel.Strength))
            {
                InventoryModel.CarryLoad.BaseValue = _specialModel.Strength.Total * 10.0f;
            }
        }

        private void FullInventory_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalculateCurrentLoad();
        }
        //

        // commands
        public RelayCommand OpenAddToInventoryWindowCommand { get; private set; }
        private void OpenAddToInventoryWindow(object obj)
        {
            dynamic dynamicItem = obj;
            ItemToAddToInventory = dynamicItem.Clone();

            ItemToAddToInventory.Amount.BaseValue += 1;

            var window = new AddToInventoryWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X;
            window.Top = mousePoint.Y;

            window.ShowDialog();
        }

        public RelayCommand AddToInventoryCommand { get; private set; }
        private void AddToInventory(object _ = null)
        {
            Type itemType = ItemToAddToInventory.GetType();

            if (_typeToCollectionMap.TryGetValue(itemType, out IList collection))
            {
                collection.Add(ItemToAddToInventory);
                FullInventory.Add(ItemToAddToInventory);
                FullInventoryView.Refresh();
            }
            else
            {
                throw new Exception($"No collection found for item type {itemType.Name}");
            }
        }
        //
    }
}
