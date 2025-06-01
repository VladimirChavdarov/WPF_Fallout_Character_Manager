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

namespace WPF_Fallout_Character_Manager.Controls
{
    /// <summary>
    /// Interaction logic for UpDownNumberInputControl.xaml
    /// </summary>
    public partial class UpDownNumberInputControl : UserControl
    {

        // Dependency Properties
        #region ChunkyCaretTextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(UpDownNumberInputControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownNumberInputControl? ThisUserControl = d as UpDownNumberInputControl;
            ThisUserControl.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            //TODO: Make sure this communicates to/from the Model properly. Look at the other UserControls
            CustomChunkyCaretTextBox.CustomTextBox.Text = Text;
        }
        #endregion
        //

        public UpDownNumberInputControl()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(Text, out int value))
            {
                value++;
                Text = value.ToString();
            }
            else
            {
                Text = "0";
            }
        }

        private void btnSubtract_Click(object sender, RoutedEventArgs e)
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
}
