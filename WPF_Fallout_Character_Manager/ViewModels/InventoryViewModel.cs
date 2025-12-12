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

        private string _selectedCatalogueCategory;
        private string _searchCatalogueText;
        private string _selectedInventoryCategory;
        private string _searchInventoryText;
        private ObservableCollection<Item> Catalogue { get; } = new ObservableCollection<Item>();
        private ObservableCollection<Item> FullInventory { get; } = new ObservableCollection<Item>();

        private dynamic _itemToAddToInventory;

        private Dictionary<Type, System.Collections.IList> _typeToCollectionMap { get; } = new Dictionary<Type, System.Collections.IList>();
        //private readonly Dictionary<Type, IList<Item>> _typeToCollectionMap = new Dictionary<Type, IList<Item>>()
        //{
        //    { typeof(Weapon), WeaponsModel.Weapons },
        //    { typeof(Armor), ArmorModel.Armors },
        //    { typeof(Ammo), AmmoModel.Ammos },
        //    { typeof(Aid), InventoryModel.AidItems },
        //    { typeof(Explosive), InventoryModel.Explosives },
        //    { typeof(Nourishment), InventoryModel.Nourishment },
        //    { typeof(Gear), InventoryModel.GearItems },
        //    { typeof(Junk), InventoryModel.JunkItems }
        //};
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

        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
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
            XtrnlJunkModel xtrnlJunkModel)
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

            foreach(Item i in XtrnlWeaponsModel.Weapons) { Catalogue.Add(i); }
            foreach(Item i in XtrnlArmorModel.Armors) { Catalogue.Add(i); }
            foreach(Item i in XtrnlArmorModel.PowerArmors) { Catalogue.Add(i); }
            foreach (Item i in XtrnlAmmoModel.Ammos) { Catalogue.Add(i); }
            foreach (Item i in XtrnlAidModel.AidItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlExplosivesModel.Explosives) { Catalogue.Add(i); }
            foreach (Item i in XtrnlNourishmentModel.Nourishments) { Catalogue.Add(i); }
            foreach (Item i in XtrnlGearModel.GearItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlJunkModel.JunkItems) { Catalogue.Add(i); }

            foreach(Item i in WeaponsModel.Weapons) { FullInventory.Add(i); }
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
        //

        // commands
        public RelayCommand OpenAddToInventoryWindowCommand { get; private set; }
        private void OpenAddToInventoryWindow(object obj)
        {
            dynamic dynamicItem = obj;
            ItemToAddToInventory= dynamicItem.Clone();

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
