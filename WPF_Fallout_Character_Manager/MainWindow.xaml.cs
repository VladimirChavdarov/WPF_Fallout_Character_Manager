using System.Text;
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

namespace WPF_Fallout_Character_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnFullscreen_Click(object sender, RoutedEventArgs e)
        {
            //WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RunFunctionTab_SaveCharacter(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel mainVM)
            {
                if(mainVM.SaveCharacter())
                {
                    MessageBox.Show("Character Saved!");
                }
                else
                {
                    MessageBox.Show("Unable to Save Character.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            e.Handled = true;
        }

        private void RunFunctionTab_LoadCharacter(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is  MainWindowViewModel mainVM)
            {
                if(mainVM.LoadCharacter())
                {
                    MessageBox.Show("Characted Loaded!");
                }
                else
                {
                    MessageBox.Show("Unable to Load Character.", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            e.Handled = true;
        }
    }
}