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
using WPF_Fallout_Character_Manager.ViewModels;
using Condition = WPF_Fallout_Character_Manager.Models.External.Condition;

namespace WPF_Fallout_Character_Manager.Controls.Panels
{
    /// <summary>
    /// Interaction logic for ConditionsPanel.xaml
    /// </summary>
    public partial class ConditionsPanel : UserControl
    {
        public ConditionsPanel()
        {
            InitializeComponent();
        }

        private void EnhancedTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // NOTE: Should this happen in the code-behind?
            if (sender is FrameworkElement fe && fe.DataContext is Condition cond)
            {
                if (DataContext is ConditionsViewModel condVM)
                {
                    condVM.SwitchVisibility(cond);
                }
            }
        }

        private void NameEnhancedTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Condition cond)
            {
                if (cond.IsReadOnly)
                    Mouse.OverrideCursor = Cursors.Hand;
                else
                    Mouse.OverrideCursor = null;
            }
        }

        private void DescriptionEnhancedTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Condition cond)
            {
                if (cond.IsReadOnly)
                    Mouse.OverrideCursor = Cursors.Arrow;
                else
                    Mouse.OverrideCursor = null;
            }
        }

        private void EnhancedTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

    }
}
