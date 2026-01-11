using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        //private int _amountToRemove;
        //public int AmountToRemove
        //{
        //    get => _amountToRemove;
        //    set => Update(ref _amountToRemove, value);
        //}

        public string NameAmount => Junk?.NameAmount ?? string.Empty;

        private void Junk_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Item.NameAmount))
            {
                OnPropertyChanged(nameof(NameAmount));
            }
        }
    }

    class EvaluatedJunk
    {
        public SelectableJunk Junk { get; set; }
        public int Coefficient { get; set; }
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
            foreach(SelectableJunk sj in SelectableJunkItems)
            {
                sj.Junk.PropertyChanged -= SelectableJunk_PropertyChanged;
            }
            SelectedJunkItems.Clear();
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

                int amountToScrap = 0;
                List<Junk> scrappedJunkItem = selectedJunk.Junk.ScrapJunkItem();
                List<Junk> scrappedJunkItemTemplate = new List<Junk>();
                foreach(Junk junk in scrappedJunkItem)
                {
                    Junk junkToAdd = junk.Clone();
                    junkToAdd.Amount.BaseValue /= selectedJunk.Junk.Amount.Total;
                    scrappedJunkItemTemplate.Add(junkToAdd);
                }

                // determine how many of this item we need to scrap to satisfy the Relevant Components
                foreach (JunkComponentWrapped relComp in relevantComponents)
                {
                    Junk desiredScrappedJunk = scrappedJunkItem.FirstOrDefault(x => x.Name.Total == relComp.JunkComponent.Name.Total);
                    int compAmountInScrappedJunk = scrappedJunkItemTemplate.FirstOrDefault(x => x.Name.Total == relComp.JunkComponent.Name.Total).Amount.Total;
                    if(desiredScrappedJunk != null)
                    {
                        int neededAmount = relComp.JunkComponent.Amount.Total - relComp.AvailableAmount;
                        amountToScrap = (int)Math.Ceiling((double)neededAmount / compAmountInScrappedJunk);
                        amountToScrap = Math.Min(amountToScrap, selectedJunk.Junk.Amount.Total);
                    }
                }

                foreach (Junk scrappedJunk in scrappedJunkItem)
                {
                    Junk desiredScrappedJunkTemplate = scrappedJunkItemTemplate.FirstOrDefault(x => x.Name.Total == scrappedJunk.Name.Total);
                    scrappedJunk.Amount.BaseValue -= desiredScrappedJunkTemplate.Amount.Total * (selectedJunk.Junk.Amount.Total - amountToScrap);

                    JunkComponentWrapped relComp = relevantComponents.FirstOrDefault(x => x.JunkComponent.Name.Total == scrappedJunk.Name.Total);
                    if(relComp != null)
                    {
                        int neededAmount = relComp.JunkComponent.Amount.Total - relComp.AvailableAmount;
                        scrappedJunk.Amount.BaseValue -= neededAmount;

                        if(scrappedJunk.Amount.BaseValue >= 0)
                        {
                            relComp.AvailableAmount = relComp.JunkComponent.Amount.Total;
                        }
                        else
                        {
                            relComp.AvailableAmount += neededAmount + scrappedJunk.Amount.Total;
                        }
                    }

                    scrappedJunk.Amount.BaseValue = Math.Max(0, scrappedJunk.Amount.Total);
                }

                // setup Output list
                if (relevantComponents.Count != 0)
                {
                    selectedJunk.JunkAction = JunkAction.ForRemove;
                    Output.Add(selectedJunk);
                    if (selectedJunk.Junk.Amount.Total != amountToScrap)
                    {
                        SelectableJunk selectedJunkToAdd = new SelectableJunk();
                        selectedJunkToAdd.Junk = selectedJunk.Junk.Clone();
                        selectedJunkToAdd.Junk.Amount.BaseValue -= amountToScrap;
                        selectedJunkToAdd.JunkAction = JunkAction.ForAdd;
                        Output.Add(selectedJunkToAdd);
                    }

                    foreach (Junk scrappedJunk in scrappedJunkItem)
                    {
                        if(scrappedJunk.Amount.Total != 0)
                        {
                            SelectableJunk desiredSelectableJunk = new SelectableJunk();
                            desiredSelectableJunk.Junk = scrappedJunk;
                            desiredSelectableJunk.JunkAction = JunkAction.ForAdd;
                            Output.Add(desiredSelectableJunk);
                        }
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
            if (ComponentsToSpend.Count <= 0 || SelectableJunkItems.Count <= 0)
            {
                return;
            }

            // clear all selections
            foreach(SelectableJunk junk in SelectableJunkItems)
            {
                junk.IsSelected = false;
            }
            SelectedJunkItems.Clear();
            //

            List<EvaluatedJunk> evaluatedJunkItems = new List<EvaluatedJunk>();

            foreach(SelectableJunk inventoryJunkItem in SelectableJunkItems)
            {
                EvaluatedJunk evalJunk = new EvaluatedJunk();
                evalJunk.Junk = inventoryJunkItem;
                evalJunk.Coefficient = 0;
                evaluatedJunkItems.Add(evalJunk);

                List<JunkComponentWrapped> relevantComponents =
                    ComponentsToSpend.Where(x => inventoryJunkItem.Junk.Components.Any(y => x.JunkComponent.Name.Total == y.Name.Total)
                                                 && x.AvailableAmount != x.JunkComponent.Amount.Total).ToList();

                int amountToScrap = 0;
                List<Junk> scrappedJunkItem = inventoryJunkItem.Junk.ScrapJunkItem();
                List<Junk> scrappedJunkItemTemplate = new List<Junk>();
                foreach (Junk junk in scrappedJunkItem)
                {
                    Junk junkToAdd = junk.Clone();
                    junkToAdd.Amount.BaseValue /= inventoryJunkItem.Junk.Amount.Total;
                    scrappedJunkItemTemplate.Add(junkToAdd);
                }

                // determine how many of this item we need to scrap to satisfy the Relevant Components
                foreach (JunkComponentWrapped relComp in relevantComponents)
                {
                    Junk desiredScrappedJunk = scrappedJunkItem.FirstOrDefault(x => x.Name.Total == relComp.JunkComponent.Name.Total);
                    int compAmountInScrappedJunk = scrappedJunkItemTemplate.FirstOrDefault(x => x.Name.Total == relComp.JunkComponent.Name.Total).Amount.Total;
                    if (desiredScrappedJunk != null)
                    {
                        int neededAmount = relComp.JunkComponent.Amount.Total - relComp.AvailableAmount;
                        amountToScrap = (int)Math.Ceiling((double)neededAmount / compAmountInScrappedJunk);
                        amountToScrap = Math.Min(amountToScrap, inventoryJunkItem.Junk.Amount.Total);
                    }
                }

                if(amountToScrap == 0)
                {
                    continue;
                }

                // assign coefficients
                foreach (Junk scrappedJunk in scrappedJunkItem)
                {
                    Junk desiredScrappedJunkTemplate = scrappedJunkItemTemplate.FirstOrDefault(x => x.Name.Total == scrappedJunk.Name.Total);
                    JunkComponentWrapped relComp = relevantComponents.FirstOrDefault(x => x.JunkComponent.Name.Total == scrappedJunk.Name.Total);
                    EvaluatedJunk evalScrappedJunk = evaluatedJunkItems.LastOrDefault();

                    if(relComp != null)
                    {
                        int relJunkAmount = desiredScrappedJunkTemplate.Amount.Total * amountToScrap;
                        scrappedJunk.Amount.BaseValue -= relJunkAmount;
                        evalScrappedJunk.Coefficient += relJunkAmount;
                    }

                    evalScrappedJunk.Coefficient -= scrappedJunk.Amount.BaseValue;
                }
            }

            // sort and traverse by coefficient
            List<EvaluatedJunk> sortedEvaluatedJunkItems = evaluatedJunkItems.OrderByDescending(e => e.Coefficient).ToList();
            foreach (EvaluatedJunk evalJunk in sortedEvaluatedJunkItems)
            {
                evalJunk.Junk.IsSelected = true;
                SelectedJunkItems.Add(evalJunk.Junk);
            }

            ProcessSelectedJunkItems();
        }
        //
    }
}
