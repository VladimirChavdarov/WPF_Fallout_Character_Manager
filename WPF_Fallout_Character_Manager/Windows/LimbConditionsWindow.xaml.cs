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
using System.Windows.Shapes;
using WPF_Fallout_Character_Manager.Controls;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.ViewModels;

namespace WPF_Fallout_Character_Manager.Windows
{
    /// <summary>
    /// Interaction logic for LimbConditionsWindow.xaml
    /// </summary>
    public partial class LimbConditionsWindow : Window
    {
        public LimbConditionsWindow(LimbConditionsViewModel limbConditionsViewModel)
        {
            InitializeComponent();
            this.DataContext = limbConditionsViewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is  LimbConditionsViewModel limbConditionsVM)
            {
                limbConditionsVM.IsModalOpen = false;
            }
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count == 0)
            //    return; // ignore deselection events

            //if (sender is ComboBox comboBox && comboBox.SelectedItem is LimbCondition selectedCondition)
            //{
            //    if (DataContext is LimbConditionsViewModel vm)
            //    {
            //        vm.ReplaceLimbConditionCommand.Execute(selectedCondition);
            //    }
            //}
        }
    }
}
