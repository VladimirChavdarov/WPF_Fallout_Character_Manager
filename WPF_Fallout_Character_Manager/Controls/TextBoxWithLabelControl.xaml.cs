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
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;

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

        #region Hint
        public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnHintChanged)));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        private static void OnHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.OnHintChanged(e);
        }

        private void OnHintChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ModValue
        public static readonly DependencyProperty ModValueProperty =
            DependencyProperty.Register("ModValue", typeof(ModTypeBase), typeof(TextBoxWithLabelControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModValuePropertyChanged)));

        public ModTypeBase ModValue
        {
            get => (ModTypeBase)GetValue(ModValueProperty);
            set => SetValue(ModValueProperty, value);
        }

        private static void ModValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.ModValuePropertyChanged(e);
        }

        private void ModValuePropertyChanged(DependencyPropertyChangedEventArgs e)
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
            //CanOpenModal = (bool)GetValue(CanOpenModalProperty);
        }
        #endregion

        #region TextWrapping
        public static readonly DependencyProperty TextWrappingProperty =
        DependencyProperty.Register(
            nameof(TextWrapping),
            typeof(TextWrapping),
            typeof(TextBoxWithLabelControl),
            new PropertyMetadata(TextWrapping.Wrap)); // default

        public TextWrapping TextWrapping
        {
            get => (TextWrapping)GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }
        #endregion

        #region AllowNewLines
        public static readonly DependencyProperty AllowNewLinesProperty =
        DependencyProperty.Register(
            nameof(AllowNewLines),
            typeof(bool),
            typeof(TextBoxWithLabelControl),
            new PropertyMetadata(false)); // default

        public bool AllowNewLines
        {
            get => (bool)GetValue(AllowNewLinesProperty);
            set => SetValue(AllowNewLinesProperty, value);
        }
        #endregion

        #region AllowTabs
        public static readonly DependencyProperty AllowTabsProperty =
        DependencyProperty.Register(
            nameof(AllowTabs),
            typeof(bool),
            typeof(TextBoxWithLabelControl),
            new PropertyMetadata(false)); // default

        public bool AllowTabs
        {
            get => (bool)GetValue(AllowTabsProperty);
            set => SetValue(AllowTabsProperty, value);
        }
        #endregion

        #region TextPadding
        public static readonly DependencyProperty TextPaddingProperty =
        DependencyProperty.Register("TextPadding", typeof(Thickness), typeof(TextBoxWithLabelControl),
            new FrameworkPropertyMetadata(new Thickness(2, 3, 2, 3), new PropertyChangedCallback(TextPaddingPropertyChanged)));

        public Thickness TextPadding
        {
            get => (Thickness)GetValue(TextPaddingProperty);
            set => SetValue(TextPaddingProperty, value);
        }

        private static void TextPaddingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelControl? ThisUserControl = d as TextBoxWithLabelControl;
            ThisUserControl.TextPaddingPropertyChanged(e);
        }

        private void TextPaddingPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
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
