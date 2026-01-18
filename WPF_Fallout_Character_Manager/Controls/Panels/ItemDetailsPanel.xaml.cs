using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Fallout_Character_Manager.Models.Inventory;
using WPF_Fallout_Character_Manager.ViewModels;

namespace WPF_Fallout_Character_Manager.Controls.Panels
{
    /// <summary>
    /// Interaction logic for ItemDetailsPanel.xaml
    /// </summary>
    public partial class ItemDetailsPanel : UserControl
    {
        public ItemDetailsPanel()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // NOTE: Make sure this doesn't cause a race condition with the command bound to the combo boxes for adding properties and upgrades
            if (sender is ComboBox comboBox)
            {
                comboBox.SelectedItem = null;
            }
        }

        private void ListBoxItem_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if(DataContext is InventoryViewModel vm)
            {
                if(vm.SelectedItem is Item gameItem)
                {
                    if(!gameItem.CanBeEdited)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }

            if (sender is ListBoxItem listBoxItem && listBoxItem.ContextMenu != null)
            {
                listBoxItem.ContextMenu.DataContext = this.DataContext;
            }
        }
    }
}
