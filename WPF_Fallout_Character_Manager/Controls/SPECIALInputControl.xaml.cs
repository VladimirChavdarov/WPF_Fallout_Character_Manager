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
    /// Interaction logic for SPECIALInputControl.xaml
    /// </summary>
    public partial class SPECIALInputControl : UserControl
    {
        #region PrimaryStat
        public static readonly DependencyProperty PrimaryStatProperty =
            DependencyProperty.Register("PrimaryStat", typeof(string), typeof(SPECIALInputControl),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnPrimaryStatChanged)));

        public string PrimaryStat
        {
            get {  return (string)GetValue(PrimaryStatProperty);}
            set { SetValue(PrimaryStatProperty, value); }
        }

        private static void OnPrimaryStatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SPECIALInputControl control)
            {
                control.OnPrimaryStatChanged(e);
            }
        }

        private void OnPrimaryStatChanged(DependencyPropertyChangedEventArgs e)
        {
            // Update StatModifier whenever PrimaryStat changes
            if (int.TryParse(PrimaryStat, out int stat))
            {
                StatModifier = (stat - 5).ToString();
            }
            else
            {
                StatModifier = "-42";
            }

            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //PrimaryStatTextBox.Text = PrimaryStat;
            ModifierTextBox.Text = StatModifier;
        }
        #endregion

        #region StatModifier
        public static readonly DependencyProperty StatModifierProperty =
            DependencyProperty.Register("StatModifier", typeof(string), typeof(SPECIALInputControl),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private string StatModifier
        {

            get { return (string)GetValue(StatModifierProperty); }
            set { SetValue(StatModifierProperty, value); }
        }
        #endregion

        // ---------------------

        #region StatLabel
        public static readonly DependencyProperty StatLabelProperty =
            DependencyProperty.Register("StatLabel", typeof(string), typeof(SPECIALInputControl),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnStatLabelChanged)));

        public string StatLabel
        {
            get { return (string)GetValue(StatLabelProperty); }
            set { SetValue(StatLabelProperty, value); }
        }

        private static void OnStatLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SPECIALInputControl control)
            {
                control.OnStatLabelChanged(e);
            }
        }

        private void OnStatLabelChanged(DependencyPropertyChangedEventArgs e)
        {
            if (StatLabel != null)
            {
                StatSymbol = StatLabel.Substring(0, 1);
            }
            StatName.Content = StatLabel;
            StatSymbolLabel.Content = StatSymbol;
        }
        #endregion

        #region StatSymbolLabel
        public static readonly DependencyProperty StatSymbolProperty =
            DependencyProperty.Register("StatSymbol", typeof(string), typeof(SPECIALInputControl),
                new FrameworkPropertyMetadata(""));

        private string StatSymbol
        {

            get { return (string)GetValue(StatSymbolProperty); }
            set { SetValue(StatSymbolProperty, value); }
        }
        #endregion

        #region ReadOnly
        // Make them read-only by default
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(SPECIALInputControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SPECIALInputControl? ThisUserControl = d as SPECIALInputControl;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            PrimaryStatTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
            ModifierTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        public SPECIALInputControl()
        {
            InitializeComponent();

            
        }
    }
}
