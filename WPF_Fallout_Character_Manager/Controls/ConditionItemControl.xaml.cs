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
    /// Interaction logic for ConditionItemControl.xaml
    /// </summary>
    public partial class ConditionItemControl : UserControl
    {
        #region Condition
        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(Condition), typeof(ConditionItemControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ConditionPropertyChanged)));

        public Condition Condition
        {
            get => (Condition)GetValue(ConditionProperty);
            set => SetValue(ConditionProperty, value);
        }

        private static void ConditionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ConditionItemControl? ThisUserControl = d as ConditionItemControl;
            ThisUserControl.ConditionPropertyChanged(e);
        }

        private void ConditionPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        public ConditionItemControl()
        {
            InitializeComponent();
        }
    }
}
