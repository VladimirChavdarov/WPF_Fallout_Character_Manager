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
using WPF_Fallout_Character_Manager.Models.ModifierSystem;

namespace WPF_Fallout_Character_Manager.Controls
{
    /// <summary>
    /// Interaction logic for UpDownTextBoxControl.xaml
    /// </summary>
    public partial class UpDownTextBoxControl : UserControl
    {
        // Dependency Properties
        #region TextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ModInt
        public static readonly DependencyProperty ModIntProperty =
            DependencyProperty.Register("ModInt", typeof(ModInt), typeof(UpDownTextBoxControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModIntPropertyChanged)));

        public ModInt ModInt
        {
            get => (ModInt)GetValue(ModIntProperty);
            set => SetValue(ModIntProperty, value);
        }

        private static void ModIntPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.ModIntPropertyChanged(e);
        }

        private void ModIntPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region ReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            CustomEnhancedTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion
        //

        public UpDownTextBoxControl()
        {
            InitializeComponent();
        }

        private void btnSubtract_Click(object sender, RoutedEventArgs e)
        {
            if(!IsReadOnly)
            {
                if (int.TryParse(Text, out int value))
                {
                    value--;
                    Text = value.ToString();
                }
                else
                {
                    Text = "0";
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsReadOnly)
            {
                if (int.TryParse(Text, out int value))
                {
                    value++;
                    Text = value.ToString();
                }
                else
                {
                    Text = "0";
                }
            }
        }
    }
}
