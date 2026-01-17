using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class PerksViewModel : ViewModelBase
    {
        private XtrnlPerksModel _xtrnlPerksModel;
        public XtrnlPerksModel XtrnlPerksModel
        {
            get => _xtrnlPerksModel;
        }

        private PerksModel _perksModel;
        public PerksModel PerksModel
        {
            get => _perksModel;
        }

        private string _searchTraitsCatalogueText;
        public string SearchTraitsCatalogueText
        {
            get => _searchTraitsCatalogueText;
            set
            {
                Update(ref _searchTraitsCatalogueText, value);
                RefreshTraitsCatalogueView();
            }
        }

        private string _searchPerksCatalogueText;
        public string SearchPerksCatalogueText
        {
            get => _searchPerksCatalogueText;
            set
            {
                Update(ref _searchPerksCatalogueText, value);
                RefreshPerksCatalogueView();
            }
        }

        private Trait _selectedCatalogueTrait;
        public Trait SelectedCatalogueTrait
        {
            get => _selectedCatalogueTrait;
            set => Update(ref _selectedCatalogueTrait, value);
        }

        private Perk _selectedCataloguePerk;
        public Perk SelectedCataloguePerk
        {
            get => _selectedCataloguePerk;
            set => Update(ref _selectedCataloguePerk, value);
        }

        public ICollectionView TraitsCatalogueView { get; }
        public ICollectionView PerksCatalogueView { get; }

        // constructor
        public PerksViewModel(XtrnlPerksModel xtrnlPerksModel, PerksModel perksModel)
        {
            _xtrnlPerksModel = xtrnlPerksModel;
            _perksModel = perksModel;

            TraitsCatalogueView = CollectionViewSource.GetDefaultView(xtrnlPerksModel.Traits);
            TraitsCatalogueView.SortDescriptions.Add(new SortDescription(nameof(Trait.Name), ListSortDirection.Ascending));

            PerksCatalogueView = CollectionViewSource.GetDefaultView(xtrnlPerksModel.Perks);
            PerksCatalogueView.SortDescriptions.Add(new SortDescription(nameof(Perk.Name), ListSortDirection.Ascending));

            _searchTraitsCatalogueText = "";
            RefreshTraitsCatalogueView();
            _searchPerksCatalogueText = "";
            RefreshPerksCatalogueView();

            AddTraitCommand = new RelayCommand(AddTrait);
            AddPerkCommand = new RelayCommand(AddPerk);
            DeleteTraitOrPerkCommand = new RelayCommand(DelteTraitOrPerk);
        }
        //

        // methods
        private void RefreshTraitsCatalogueView()
        {
            TraitsCatalogueView.Filter = FilterTraits;
        }

        private void RefreshPerksCatalogueView()
        {
            PerksCatalogueView.Filter = FilterPerks;
        }

        private bool FilterTraits(object obj)
        {
            return FilterTPCard(obj, SearchTraitsCatalogueText);
        }

        private bool FilterPerks(object obj)
        {
            return FilterTPCard(obj, SearchPerksCatalogueText);
        }

        private bool FilterTPCard(object obj, string searchText)
        {
            if (obj is not TPCard card)
                return false;

            return searchText.Length == 0 || card.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        }
        //

        // commands
        public RelayCommand AddTraitCommand { get; private set; }
        private void AddTrait(object obj = null)
        {
            if(SelectedCatalogueTrait != null)
                PerksModel.Traits.Add(SelectedCatalogueTrait.Clone());
        }

        public RelayCommand AddPerkCommand { get; private set; }
        private void AddPerk(object obj = null)
        {
            if(SelectedCataloguePerk != null)
            {
                var perkToAdd = SelectedCataloguePerk.Clone();
                perkToAdd.CurrentStacks = 1;
                PerksModel.Perks.Add(perkToAdd);
            }
        }

        public RelayCommand DeleteTraitOrPerkCommand { get; private set; }
        private void DelteTraitOrPerk(object obj)
        {
            if(obj == null)
                return;

            switch(obj)
            {
                case Trait trait:
                    PerksModel.Traits.Remove(trait);
                    break;
                case Perk perk:
                    PerksModel.Perks.Remove(perk);
                    break;
            }
        }
        //
    }
}
