using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;
using WPF_Fallout_Character_Manager.Windows;

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

        private Trait _newTrait;
        public Trait NewTrait
        {
            get => _newTrait;
            set => Update(ref _newTrait, value);
        }

        private Perk _newPerk;
        public Perk NewPerk
        {
            get => _newPerk;
            set => Update(ref _newPerk, value);
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
            AddTraitToCatalogueCommand = new RelayCommand(AddTraitToCatalogue);
            AddPerkCommand = new RelayCommand(AddPerk);
            AddPerkToCatalogueCommand = new RelayCommand(AddPerkToCatalogue);
            DeleteTraitOrPerkCommand = new RelayCommand(DelteTraitOrPerk);
            OpenNewTraitModalWindowCommand = new RelayCommand(OpenNewTraitModalWindow);
            OpenNewPerkModalWindowCommand = new RelayCommand(OpenNewPerkModalWindow);
            LoadImageCommand = new RelayCommand(LoadImage);
            ResetNewTraitOrPerkCommand = new RelayCommand(ResetNewTraitOrPerk);
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

        public RelayCommand AddTraitToCatalogueCommand { get; private set; }
        private void AddTraitToCatalogue(object _ = null)
        {
            XtrnlPerksModel.Traits.Add(NewTrait.Clone());
            NewTrait = null;
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

        public RelayCommand AddPerkToCatalogueCommand { get; private set; }
        private void AddPerkToCatalogue(object _ = null)
        {
            XtrnlPerksModel.Perks.Add(NewPerk.Clone());
            NewPerk = null;
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

        public RelayCommand OpenNewTraitModalWindowCommand { get; private set; }
        private void OpenNewTraitModalWindow(object obj)
        {
            NewTrait = new Trait();
            NewTrait.ImagePath = "/Resources/vault_boy_thumbsup.png";

            var window = new NewTraitWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X;
            window.Top = mousePoint.Y - 100;

            window.ShowDialog();
        }

        public RelayCommand OpenNewPerkModalWindowCommand { get; private set; }
        private void OpenNewPerkModalWindow(object obj)
        {
            NewPerk = new Perk();
            NewPerk.ImagePath = "/Resources/vault_boy_thumbsup.png";
            NewPerk.MaxStacks = 1;

            var window = new NewPerkWindow();
            window.DataContext = this;
            var mousePoint = System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow);
            window.Left = mousePoint.X;
            window.Top = mousePoint.Y - 200;

            window.ShowDialog();
        }

        public RelayCommand LoadImageCommand { get; private set; }
        private void LoadImage(object _ = null)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Select an Avatar";
            dialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == true)
            {
                if (NewTrait != null)
                {
                    NewTrait.PickImageFromCardType = false;
                    NewTrait.ImagePath = dialog.FileName;
                }
                else if(NewPerk != null)
                {
                    NewPerk.PickImageFromCardType = false;
                    NewPerk.ImagePath = dialog.FileName;
                }
            }
        }

        public RelayCommand ResetNewTraitOrPerkCommand { get; private set; }
        private void ResetNewTraitOrPerk(object _ = null)
        {
            NewTrait = null;
            NewPerk = null;
        }
        //
    }
}
