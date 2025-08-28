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
    /// Interaction logic for ConditionButtonControl.xaml
    /// </summary>
    public partial class ConditionButtonControl : UserControl
    {
        #region ButtonText
        public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register("ButtonText", typeof(string), typeof(ConditionButtonControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnButtonTextChanged)));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        private static void OnButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ConditionButtonControl? ThisUserControl = d as ConditionButtonControl;
            ThisUserControl.OnButtonTextChanged(e);
        }

        private void OnButtonTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        public ConditionButtonControl()
        {
            InitializeComponent();
        }
    }
}
