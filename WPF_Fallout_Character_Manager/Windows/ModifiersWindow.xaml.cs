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
using WPF_Fallout_Character_Manager.ViewModels.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Windows
{
    /// <summary>
    /// Interaction logic for ModifiersWindow.xaml
    /// </summary>
    public partial class ModifiersWindow : Window
    {
        public ModifiersWindow(object modValueViewModel)
        {
            InitializeComponent();

            if (!(modValueViewModel is ModValueViewModel<int>) &&
                !(modValueViewModel is ModValueViewModel<float>) &&
                !(modValueViewModel is ModValueViewModel<string>))
            {
                throw new Exception("modValueViewModel is not a correct type.");
            }

            this.DataContext = modValueViewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
