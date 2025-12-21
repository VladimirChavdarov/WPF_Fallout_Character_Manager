using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    class SelectableJunk : ModTypeBase
    {
        private bool _isSelected;

        public Junk Junk;
        public bool IsSelected
        {
            get => _isSelected;
            set => Update(ref _isSelected, value);
        }

        public string NameAmount => Junk.NameAmount;
    }

    class JunkManagerViewModel : ViewModelBase
    {
        // local variables
        XtrnlJunkModel _xtrnlJunkModel;
        InventoryModel _inventoryModel;
        //

        // public variables
        public ObservableCollection<JunkComponent> ComponentsToSpend { get; }
        public ObservableCollection<SelectableJunk> SelectedJunkItems { get; }
        public ObservableCollection<SelectableJunk> SelectableJunkItems { get; }

        public XtrnlJunkModel XtrnlJunkModel
        {
            get => _xtrnlJunkModel;
        }

        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
        }
        //

        // constructor
        public JunkManagerViewModel(XtrnlJunkModel xtrnlJunkModel, InventoryModel inventoryModel)
        {
            _xtrnlJunkModel = xtrnlJunkModel;
            _inventoryModel = inventoryModel;

            ComponentsToSpend = new ObservableCollection<JunkComponent>();
            SelectedJunkItems = new ObservableCollection<SelectableJunk>();
            SelectableJunkItems = new ObservableCollection<SelectableJunk>();
            InventoryModel.JunkItems.CollectionChanged += JunkItems_CollectionChanged;
            UpdateSelectableJunkCollection();

            AddJunkComponentForSpendingCommand = new RelayCommand(AddJunkComponentForSpending);
            RemoveJunkComponentFromSpendingListCommand = new RelayCommand(RemoveJunkComponentFromSpendingList);
            SelectOrDeselectJunkCommand = new RelayCommand(SelectOrDeselectJunk);
        }
        //

        // methods
        private void UpdateSelectableJunkCollection()
        {
            SelectableJunkItems.Clear();
            foreach (Junk junk in InventoryModel.JunkItems)
            {
                SelectableJunk selectableJunk = new SelectableJunk();
                selectableJunk.Junk = junk;
                selectableJunk.IsSelected = false;
                SelectableJunkItems.Add(selectableJunk);
            }
        }

        private void JunkItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectableJunkCollection();
        }
        //

        // commands
        public RelayCommand AddJunkComponentForSpendingCommand { get; private set; }
        private void AddJunkComponentForSpending(object obj)
        {
            if(obj is JunkComponent junkComponentToSpend)
            {
                JunkComponent clonedComponent = junkComponentToSpend.Clone();
                clonedComponent.CanBeEdited = true;
                clonedComponent.Amount.BaseValue = 1;
                ComponentsToSpend.Add(clonedComponent);
            }
        }

        public RelayCommand RemoveJunkComponentFromSpendingListCommand { get; private set; }
        private void RemoveJunkComponentFromSpendingList(object obj)
        {
            if (obj is JunkComponent junkComponentToRemove)
            {
                ComponentsToSpend.Remove(junkComponentToRemove);
            }
        }

        public RelayCommand SelectOrDeselectJunkCommand { get; private set; }
        private void SelectOrDeselectJunk(object obj)
        {
            if(obj is SelectableJunk junk)
            {
                if(SelectedJunkItems.Contains(junk))
                {
                    junk.IsSelected = false;
                    SelectedJunkItems.Remove(junk);
                }
                else
                {
                    junk.IsSelected = true;
                    SelectedJunkItems.Add(junk);
                }
            }
        }
        //
    }
}
