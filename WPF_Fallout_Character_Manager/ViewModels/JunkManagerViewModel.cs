using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.External.Inventory;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    public enum JunkAction
    {
        ForAdd,
        ForRemove
    }

    class SelectableJunk : ModTypeBase
    {
        private Junk _junk;
        public Junk Junk
        {
            get => _junk;
            set
            {
                if (_junk != null)
                    _junk.PropertyChanged -= Junk_PropertyChanged;

                Update(ref _junk, value);

                if (_junk != null)
                    _junk.PropertyChanged += Junk_PropertyChanged;

                OnPropertyChanged(nameof(NameAmount));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => Update(ref _isSelected, value);
        }

        private JunkAction _junkAction;
        public JunkAction JunkAction
        {
            get => _junkAction;
            set
            {
                Update(ref _junkAction, value);
                OnPropertyChanged(nameof(ActionSymbol));
            }
        }

        public string ActionSymbol
        {
            get
            {
                if (JunkAction == JunkAction.ForAdd)
                    return "+ ";
                else
                    return "- ";
            }
        }

        private int _amountToRemove;
        public int AmountToRemove
        {
            get => _amountToRemove;
            set => Update(ref _amountToRemove, value);
        }

        public string NameAmount => Junk?.NameAmount ?? string.Empty;

        private void Junk_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Item.NameAmount))
            {
                OnPropertyChanged(nameof(NameAmount));
            }
        }
    }

    class JunkComponentWrapped : ModTypeBase
    {
        private int _availableAmount;
        public int AvailableAmount
        {
            get => _availableAmount;
            set => Update(ref _availableAmount, value);
        }

        private JunkComponent _junkComponent;
        public JunkComponent JunkComponent
        {
            get => _junkComponent;
            set => Update(ref _junkComponent, value);
        }
    }

    class JunkManagerViewModel : ViewModelBase
    {
        // local variables
        XtrnlJunkModel _xtrnlJunkModel;
        InventoryModel _inventoryModel;
        //

        // public variables
        public ObservableCollection<JunkComponentWrapped> ComponentsToSpend { get; }
        public ObservableCollection<SelectableJunk> SelectedJunkItems { get; }
        public ObservableCollection<SelectableJunk> Output { get; }
        public ObservableCollection<SelectableJunk> SelectableJunkItems { get; } // this is just a wrapper for the junk inventory that uses SelectableJunk instead of Junk class.

        public XtrnlJunkModel XtrnlJunkModel
        {
            get => _xtrnlJunkModel;
        }

        public InventoryModel InventoryModel
        {
            get => _inventoryModel;
        }

        public Visibility ShowAcceptButton
        {
            get
            {
                foreach(JunkComponentWrapped component in ComponentsToSpend)
                {
                    if(component.AvailableAmount != component.JunkComponent.Amount.Total)
                        return Visibility.Hidden;
                }

                return ComponentsToSpend.Any() ? Visibility.Visible : Visibility.Hidden;
            }
        }
        //

        // constructor
        public JunkManagerViewModel(XtrnlJunkModel xtrnlJunkModel, InventoryModel inventoryModel)
        {
            _xtrnlJunkModel = xtrnlJunkModel;
            _inventoryModel = inventoryModel;

            ComponentsToSpend = new ObservableCollection<JunkComponentWrapped>();
            SelectedJunkItems = new ObservableCollection<SelectableJunk>();
            Output = new ObservableCollection<SelectableJunk>();
            SelectableJunkItems = new ObservableCollection<SelectableJunk>();
            InventoryModel.JunkItems.CollectionChanged += JunkItems_CollectionChanged;
            ComponentsToSpend.CollectionChanged += ComponentsToSpend_CollectionChanged;
            UpdateSelectableJunkCollection();

            AddJunkComponentForSpendingCommand = new RelayCommand(AddJunkComponentForSpending);
            RemoveJunkComponentFromSpendingListCommand = new RelayCommand(RemoveJunkComponentFromSpendingList);
            SelectOrDeselectJunkCommand = new RelayCommand(SelectOrDeselectJunk);
            AutoSelectJunkCommand = new RelayCommand(AutoSelectJunk);
        }

        //

        // methods
        private void UpdateSelectableJunkCollection()
        {
            foreach(SelectableJunk sj in SelectedJunkItems)
            {
                sj.Junk.PropertyChanged -= SelectableJunk_PropertyChanged;
            }
            SelectableJunkItems.Clear();

            foreach (Junk junk in InventoryModel.JunkItems)
            {
                SelectableJunk selectableJunk = new SelectableJunk();
                selectableJunk.Junk = junk;
                selectableJunk.IsSelected = false;
                selectableJunk.Junk.PropertyChanged += SelectableJunk_PropertyChanged;
                SelectableJunkItems.Add(selectableJunk);
            }

            OnPropertyChanged(nameof(ShowAcceptButton));
        }

        private void ProcessSelectedJunkItems()
        {
            // reset
            foreach(var wrappedJunk in ComponentsToSpend)
            {
                wrappedJunk.AvailableAmount = 0;
            }
            Output.Clear();
            //

            // stress test the shit out of this one.
            foreach(SelectableJunk selectedJunk in SelectedJunkItems)
            {
                // this looks performance heavy
                List<JunkComponentWrapped> relevantComponents =
                    ComponentsToSpend.Where(x => selectedJunk.Junk.Components.Any(y => x.JunkComponent.Name.Total == y.Name.Total)
                                                 && x.AvailableAmount != x.JunkComponent.Amount.Total).ToList();

                if(relevantComponents.Count != 0)
                {
                    List<Junk> scrappedJunkItem = selectedJunk.Junk.ScrapJunkItem();
                    selectedJunk.JunkAction = JunkAction.ForRemove;
                    Output.Add(selectedJunk);

                    //for (int i = 0; i < selectedJunk.Junk.Amount.Total; i++)
                    //{
                    //    selectedJunk.AmountToRemove++;


                    //}

                    foreach (JunkComponentWrapped desiredComponent in ComponentsToSpend)
                    {
                        Junk desiredScrappedJunk = scrappedJunkItem.FirstOrDefault(x => x.Name.Total == desiredComponent.JunkComponent.Name.Total);
                        if(desiredScrappedJunk != null)
                        {
                            int neededAmount = desiredComponent.JunkComponent.Amount.Total - desiredComponent.AvailableAmount;
                            int surplus = desiredScrappedJunk.Amount.Total - neededAmount;
                            if(surplus > 0)
                            {
                                SelectableJunk desiredSelectableJunk = new SelectableJunk();
                                desiredSelectableJunk.Junk = desiredScrappedJunk;
                                desiredSelectableJunk.Junk.Amount.BaseValue = surplus;
                                desiredSelectableJunk.JunkAction = JunkAction.ForAdd;
                                Output.Add(desiredSelectableJunk);

                                desiredComponent.AvailableAmount = desiredComponent.JunkComponent.Amount.Total;
                            }
                            else
                            {
                                desiredComponent.AvailableAmount += desiredScrappedJunk.Amount.BaseValue;
                            }

                            scrappedJunkItem.Remove(desiredScrappedJunk);
                        }
                    }

                    // add all of the leftover components from the scrapped junk as junk items back to the inventory
                    foreach(Junk scrappedJunk in scrappedJunkItem)
                    {
                        SelectableJunk desiredSelectableJunk = new SelectableJunk();
                        desiredSelectableJunk.Junk = scrappedJunk;
                        desiredSelectableJunk.JunkAction = JunkAction.ForAdd;
                        Output.Add(desiredSelectableJunk);
                    }
                }
            }

            OnPropertyChanged(nameof(ShowAcceptButton));
        }

        private void SelectableJunk_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ProcessSelectedJunkItems();
        }

        private void ComponentToSpendAmount_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ProcessSelectedJunkItems();
        }

        private void ComponentsToSpend_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ProcessSelectedJunkItems();
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
                JunkComponentWrapped wrappedComponent = new JunkComponentWrapped();
                wrappedComponent.AvailableAmount = 0;
                wrappedComponent.JunkComponent = junkComponentToSpend.Clone();
                wrappedComponent.JunkComponent.CanBeEdited = true;
                wrappedComponent.JunkComponent.Amount.BaseValue = 1;
                wrappedComponent.JunkComponent.Amount.PropertyChanged += ComponentToSpendAmount_PropertyChanged;
                ComponentsToSpend.Add(wrappedComponent);
            }
        }

        public RelayCommand RemoveJunkComponentFromSpendingListCommand { get; private set; }
        private void RemoveJunkComponentFromSpendingList(object obj)
        {
            if (obj is JunkComponentWrapped junkComponentToRemove)
            {
                junkComponentToRemove.JunkComponent.Amount.PropertyChanged -= ComponentToSpendAmount_PropertyChanged;
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

            ProcessSelectedJunkItems();
        }

        public RelayCommand AutoSelectJunkCommand { get; private set; }
        private void AutoSelectJunk(object _ = null)
        {

        }
        //
    }
}
