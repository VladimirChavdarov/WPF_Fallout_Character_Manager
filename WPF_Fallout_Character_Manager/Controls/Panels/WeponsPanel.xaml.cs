using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for WeponsPanel.xaml
    /// </summary>
    public partial class WeponsPanel : UserControl
    {
        public WeponsPanel()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                string name = comboBox.Text;
                // ay ay ay. This is an ugly way to handle it and there is a twitch when selecting a property or an upgrade but it will do.
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    comboBox.Text = name;
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }
    }
}
