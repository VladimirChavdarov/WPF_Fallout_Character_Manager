using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

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

        private string _selectedCategory;
        private string _searchText;
        private ObservableCollection<Item> Catalogue { get; } = new ObservableCollection<Item>();
        public ICollectionView CatalogueView { get; }
        //

        // public variables
        public IReadOnlyList<string> Categories { get; } = new List<string>() { "All", "Weapon", "Armor", "Ammo", "Aid", "Explosive", "Nourishment", "Gear", "Junk" };
        
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                Update(ref _selectedCategory, value);
                if(_selectedCategory != null)
                {
                    RefreshCatalogueView();
                }
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                Update(ref _searchText, value);
                if (_searchText != null)
                {
                    RefreshCatalogueView();
                }
            }
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
            foreach (Item i in XtrnlAmmoModel.Ammos) { Catalogue.Add(i); }
            foreach (Item i in XtrnlAidModel.AidItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlExplosivesModel.Explosives) { Catalogue.Add(i); }
            foreach (Item i in XtrnlNourishmentModel.Nourishments) { Catalogue.Add(i); }
            foreach (Item i in XtrnlGearModel.GearItems) { Catalogue.Add(i); }
            foreach (Item i in XtrnlJunkModel.JunkItems) { Catalogue.Add(i); }

            CatalogueView = CollectionViewSource.GetDefaultView(Catalogue);
            CatalogueView.SortDescriptions.Add(new SortDescription(nameof(Item.NameAmount), ListSortDirection.Ascending));

            _searchText = "";
            SelectedCategory = Categories.First();
            CatalogueView.Filter = FilterItem;
        }
        //

        // methods
        private void RefreshCatalogueView()
        {
            CatalogueView.Filter = FilterItem;
        }

        private bool FilterItem(object obj)
        {
            Item item = (Item)obj;
            if (SelectedCategory == "All" && item.NameString.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if (SelectedCategory.Contains(obj.GetType().Name))
            {
                if(item != null && item.NameString.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        //
    }
}
