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
    /// Interaction logic for PowerArmorsPanel.xaml
    /// </summary>
    public partial class PowerArmorsPanel : UserControl
    {
        public PowerArmorsPanel()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                comboBox.SelectedItem = null;
            }
        }
    }
}
