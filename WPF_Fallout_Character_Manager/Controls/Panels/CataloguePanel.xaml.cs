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

namespace WPF_Fallout_Character_Manager.Controls.Panels
{
    /// <summary>
    /// Interaction logic for CataloguePanel.xaml
    /// </summary>
    public partial class CataloguePanel : UserControl
    {
        public CataloguePanel()
        {
            InitializeComponent();
        }

        private void ListBoxItem_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is ListBoxItem item && item.ContextMenu != null)
            {
                item.ContextMenu.DataContext = this.DataContext;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            scrollViewer.ScrollToHorizontalOffset(
                scrollViewer.HorizontalOffset - e.Delta);

            e.Handled = true;
        }
    }
}
