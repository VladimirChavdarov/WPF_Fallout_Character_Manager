using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class InventoryViewModel : ViewModelBase
    {
        // local variables
        private WeaponsViewModel _weaponsViewModel;
        private ArmorViewModel _armorViewModel;

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

        private Type _selectedTypeForCreating;
        private dynamic _newItemTemplate;

        private Dictionary<Type, IList> _typeToInventoryCollections { get; } = new Dictionary<Type, IList>();
        private Dictionary<Type, IList> _typeToCatalogueCollections { get; } = new Dictionary<Type, IList>();
        //

        // public variables
        public IReadOnlyList<string> Categories { get; } = new List<string>() { "All", "Weapon", "Armor", "Ammo", "Aid", "Explosive", "Nourishment", "Gear", "Junk" };
        public IReadOnlyList<Type> ItemTypes { get; } = new List<Type>() { typeof(Weapon), typeof(Armor), typeof(PowerArmor), typeof(Ammo), typeof(Aid), typeof(Explosive), typeof(Nourishment), typeof(Gear), typeof(Junk) };
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


        public Type SelectedTypeForCreating
        {
            get => _selectedTypeForCreating;
            set
            {
                Update(ref _selectedTypeForCreating, value);

                if (_typeToCatalogueCollections.TryGetValue(SelectedTypeForCreating, out IList collection))
                {
                    dynamic item = collection[0];
                    NewItemTemplate = item.Clone();
                    NewItemTemplate.CanBeEdited = true;
                }
                else
                {
                    throw new Exception($"No collection found for item type {SelectedTypeForCreating}");
                }
            }
        }
        public dynamic NewItemTemplate
        {
            get => _newItemTemplate;
            set
            {
                Update(ref _newItemTemplate, value);
            }
        }

        public object SelectedItem => SelectedInventoryItem ?? SelectedCatalogueItem;

        public WeaponsViewModel WeaponsViewModel
        {
            get => _weaponsViewModel;
        }

        public ArmorViewModel ArmorViewModel
        {
            get => _armorViewModel;
        }

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
            WeaponsViewModel weaponsViewModel,
            ArmorViewModel armorViewModel,

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
            _weaponsModel = weaponsModel;
            _armorModel = armorModel;

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

            foreach (Item i in FullInventory)
            {
                i.PropertyChanged += InventoryItem_PropertyChanged;
            }

            _typeToInventoryCollections.Add(typeof(Weapon), WeaponsModel.Weapons);
            _typeToInventoryCollections.Add(typeof(Armor), ArmorModel.Armors);
            _typeToInventoryCollections.Add(typeof(PowerArmor), ArmorModel.PowerArmors);
            _typeToInventoryCollections.Add(typeof(Ammo), AmmoModel.Ammos);
            _typeToInventoryCollections.Add(typeof(Aid), InventoryModel.AidItems);
            _typeToInventoryCollections.Add(typeof(Explosive), InventoryModel.Explosives);
            _typeToInventoryCollections.Add(typeof(Nourishment), InventoryModel.Nourishment);
            _typeToInventoryCollections.Add(typeof(Gear), InventoryModel.GearItems);
            _typeToInventoryCollections.Add(typeof(Junk), InventoryModel.JunkItems);

            _typeToCatalogueCollections.Add(typeof(Weapon), XtrnlWeaponsModel.Weapons);
            _typeToCatalogueCollections.Add(typeof(Armor), XtrnlArmorModel.Armors);
            _typeToCatalogueCollections.Add(typeof(PowerArmor), XtrnlArmorModel.PowerArmors);
            _typeToCatalogueCollections.Add(typeof(Ammo), XtrnlAmmoModel.Ammos);
            _typeToCatalogueCollections.Add(typeof(Aid), XtrnlAidModel.AidItems);
            _typeToCatalogueCollections.Add(typeof(Explosive), XtrnlExplosivesModel.Explosives);
            _typeToCatalogueCollections.Add(typeof(Nourishment), XtrnlNourishmentModel.Nourishments);
            _typeToCatalogueCollections.Add(typeof(Gear), XtrnlGearModel.GearItems);
            _typeToCatalogueCollections.Add(typeof(Junk), XtrnlJunkModel.JunkItems);

            CatalogueView = CollectionViewSource.GetDefaultView(Catalogue);
            CatalogueView.SortDescriptions.Add(new SortDescription(nameof(Item.NameString), ListSortDirection.Ascending));

            FullInventoryView = CollectionViewSource.GetDefaultView(FullInventory);
            FullInventoryView.SortDescriptions.Add(new SortDescription(nameof(Item.NameString), ListSortDirection.Ascending));

            OpenAddToInventoryWindowCommand = new RelayCommand(OpenAddToInventoryWindow);
            AddToInventoryCommand = new RelayCommand(AddToInventory);
            RemoveFromInventoryCommand = new RelayCommand(RemoveFromInventory);
            DuplicateStackCommand = new RelayCommand(DuplicateStack);
            CreateNewTemplateFromSelectedItemCommand = new RelayCommand(CreateNewTemplateFromSelectedItem);

            OpenNewTemplateWindowCommand = new RelayCommand(OpenNewTemplateWindow);
            AddToCatalogueCommand = new RelayCommand(AddToCatalogue);
            NullifyNewItemTemplateCommand = new RelayCommand(NullifyNewItemTemplate);

            AddPropertyCommand = new RelayCommand(AddProperty);
            RemovePropertyCommand = new RelayCommand(RemoveProperty);
            AddUpgradeCommand = new RelayCommand(AddUpgrade);
            RemoveUpgradeCommand = new RelayCommand(RemoveUpgrade);

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

        // NOTE: This scales badly.
        private void CalculateCurrentLoad()
        {
            InventoryModel.CurrentLoad.BaseValue = 0.0f;
            foreach (Item item in FullInventory)
            {
                InventoryModel.CurrentLoad.BaseValue += item.TotalLoad;
            }
        }

        private void AddToCurrentLoad(float loadToAdd)
        {
            InventoryModel.CurrentLoad.BaseValue += loadToAdd;
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
            float loadToAdd = 0.0f;

            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(Item item in e.NewItems)
                    {
                        loadToAdd += item.TotalLoad;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Item item in e.OldItems)
                    {
                        loadToAdd -= item.TotalLoad;
                    }
                    break;
            }

            AddToCurrentLoad(loadToAdd);
        }

        private void InventoryItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Item.TotalLoad))
            {
                CalculateCurrentLoad();
            }
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

            if (_typeToInventoryCollections.TryGetValue(itemType, out IList collection))
            {
                ItemToAddToInventory.CanBeEdited = true;
                if(ItemToAddToInventory is Item item)
                {
                    item.PropertyChanged += InventoryItem_PropertyChanged;
                }

                collection.Add(ItemToAddToInventory);
                FullInventory.Add(ItemToAddToInventory);
                FullInventoryView.Refresh();
            }
            else
            {
                throw new Exception($"No collection found for item type {itemType.Name}");
            }
        }

        public RelayCommand RemoveFromInventoryCommand { get; private set; }
        private void RemoveFromInventory(object _ = null)
        {
            Type itemType = SelectedItem.GetType();

            if (_typeToInventoryCollections.TryGetValue(itemType, out IList collection))
            {
                collection.Remove(SelectedItem);
                if(SelectedItem is  Item item)
                {
                    FullInventory.Remove(item);
                }
            }
        }

        public RelayCommand AddPropertyCommand { get; private set; }
        private void AddProperty(object obj)
        {
            if (obj == null)
                return;

            dynamic item = SelectedItem ?? NewItemTemplate;
            item.AddProperty(obj);
        }

        public RelayCommand RemovePropertyCommand { get; private set; }
        private void RemoveProperty(object obj)
        {
            if (obj == null)
                return;

            dynamic item = SelectedItem ?? NewItemTemplate;
            item.RemoveProperty(obj);
        }

        public RelayCommand AddUpgradeCommand { get; private set; }
        private void AddUpgrade(object obj)
        {
            if (obj == null)
                return;

            dynamic item = SelectedItem ?? NewItemTemplate;
            item.AddUpgrade(obj);
        }

        public RelayCommand RemoveUpgradeCommand { get; private set; }
        private void RemoveUpgrade(object obj)
        {
            if (obj == null)
                return;

            dynamic item = SelectedItem ?? NewItemTemplate;
            item.RemoveUpgrade(obj);
        }

        public RelayCommand OpenNewTemplateWindowCommand { get; private set; }
        private void OpenNewTemplateWindow(object _ = null)
        {
            NewItemTemplate = null;
            SelectedCatalogueItem = null;
            SelectedInventoryItem = null;

            var window = new NewTemplateWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.ShowDialog();
        }

        public RelayCommand AddToCatalogueCommand { get; private set; }
        private void AddToCatalogue(object _ = null)
        {
            if (NewItemTemplate == null)
                return;

            if (_typeToCatalogueCollections.TryGetValue(SelectedTypeForCreating, out IList collection))
            {
                NewItemTemplate.CanBeEdited = false;
                NewItemTemplate.ConstructNote();

                collection.Add(NewItemTemplate);
                Catalogue.Add(NewItemTemplate);
                CatalogueView.Refresh();

                NewItemTemplate = null;
                SelectedCatalogueItem = null;
                SelectedInventoryItem = null;
            }
            else
            {
                throw new Exception($"No collection found for item type {SelectedTypeForCreating}");
            }
        }

        public RelayCommand NullifyNewItemTemplateCommand { get; private set; }
        private void NullifyNewItemTemplate(object _ = null)
        {
            NewItemTemplate = null;
            SelectedCatalogueItem = null;
            SelectedInventoryItem = null;
        }

        public RelayCommand DuplicateStackCommand { get; private set; }
        private void DuplicateStack(object _ = null)
        {
            Type itemType = SelectedItem.GetType();

            if (_typeToInventoryCollections.TryGetValue(itemType, out IList collection))
            {
                dynamic selectedItem = SelectedItem;
                Item newItem = selectedItem.Clone();
                newItem.PropertyChanged += InventoryItem_PropertyChanged;

                collection.Add(newItem);
                FullInventory.Add(newItem);
                FullInventoryView.Refresh();
            }
            else
            {
                throw new Exception($"No collection found for item type {itemType.Name}");
            }
        }

        public RelayCommand CreateNewTemplateFromSelectedItemCommand { get; private set; }
        private void CreateNewTemplateFromSelectedItem(object _ = null)
        {
            dynamic selectedItem = SelectedItem;
            SelectedTypeForCreating = SelectedItem.GetType();
            NewItemTemplate = selectedItem.Clone();
            NewItemTemplate.CanBeEdited = true;

            SelectedCatalogueItem = null;
            SelectedInventoryItem = null;

            var window = new NewTemplateWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.ShowDialog();
        }
        //
    }
}
