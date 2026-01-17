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
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.ViewModels;

namespace WPF_Fallout_Character_Manager.Controls
{
    /// <summary>
    /// Interaction logic for TPCardControl.xaml
    /// </summary>
    public partial class TPCardControl : UserControl
    {
        //#region TPCard
        //public static readonly DependencyProperty TPCardProperty =
        //    DependencyProperty.Register("TPCard", typeof(TPCard), typeof(TPCardControl),
        //        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(TPCardPropertyChanged)));

        //public TPCard TPCard
        //{
        //    get => (TPCard)GetValue(TPCardProperty);
        //    set => SetValue(TPCardProperty, value);
        //}

        //private static void TPCardPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    TPCardControl? ThisUserControl = d as TPCardControl;
        //    ThisUserControl.TPCardPropertyChanged(e);
        //}

        //private void TPCardPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{

        //}
        //#endregion

        #region WildWastelandVisibility
        public static readonly DependencyProperty WildWastelandVisibilityProperty =
        DependencyProperty.Register("WildWastelandVisibility", typeof(Visibility), typeof(TPCardControl),
            new FrameworkPropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(WildWastelandVisibilityPropertyChanged)));

        public Visibility WildWastelandVisibility
        {
            get => (Visibility)GetValue(WildWastelandVisibilityProperty);
            set => SetValue(WildWastelandVisibilityProperty, value);
        }

        private static void WildWastelandVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TPCardControl? ThisUserControl = d as TPCardControl;
            ThisUserControl.WildWastelandVisibilityPropertyChanged(e);
        }

        private void WildWastelandVisibilityPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        public TPCardControl()
        {
            InitializeComponent();
        }

        private void EnhancedTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is FrameworkElement fe && DataContext is TPCard card)
            {
                card.ShowDescription = false;
            }
        }

        private void DescriptionEnhancedTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                Mouse.OverrideCursor = Cursors.Hand;
            }
        }

        private void EnhancedTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
