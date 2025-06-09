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
    /// Interaction logic for TextBoxWithLabelControl.xaml
    /// </summary>
    public partial class TextBoxWithLabelControl : UserControl
    {
        //Dependency Properties
        #region Label
        public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register("LabelText", typeof(string), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnLabelTextChanged)));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        private static void OnLabelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.OnLabelTextChanged(e);
        }

        private void OnLabelTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region TextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
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
            DependencyProperty.Register("ModInt", typeof(ModInt), typeof(TextBoxWithLabelControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModIntPropertyChanged)));

        public ModInt ModInt
        {
            get => (ModInt)GetValue(ModIntProperty);
            set => SetValue(ModIntProperty, value);
        }

        private static void ModIntPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.ModIntPropertyChanged(e);
        }

        private void ModIntPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region Orientation
        public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnOrientationChanged)));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.OnOrientationChanged(e);
        }

        private void OnOrientationChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region LabelFontSize
        public static readonly DependencyProperty LabelFontSizeProperty =
        DependencyProperty.Register("LabelFontSize", typeof(int), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnLabelFontSizeChanged)));

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        private static void OnLabelFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.OnLabelFontSizeChanged(e);
        }

        private void OnLabelFontSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            EnhancedTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        #region CanOpenModal
        public static readonly DependencyProperty CanOpenModalProperty =
        DependencyProperty.Register("CanOpenModal", typeof(bool), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CanOpenModalPropertyChanged)));

        public bool CanOpenModal
        {
            get => (bool)GetValue(CanOpenModalProperty);
            set => SetValue(CanOpenModalProperty, value);
        }

        private static void CanOpenModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.CanOpenModalPropertyChanged(e);
        }

        private void CanOpenModalPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            CanOpenModal = (bool)GetValue(CanOpenModalProperty);
        }
        #endregion
        //

        public TextBoxWithLabelControl()
        {
            InitializeComponent();
        }

        private void EnhancedTextBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!CanOpenModal)
            {
                e.Handled = true;
            }
        }
    }
}
