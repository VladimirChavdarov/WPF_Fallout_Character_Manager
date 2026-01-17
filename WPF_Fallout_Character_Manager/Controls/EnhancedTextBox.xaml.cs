using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for EnhancedTextBox.xaml
    /// </summary>
    public partial class EnhancedTextBox : UserControl
    {
        // Dependecy Properties
        #region TextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(EnhancedTextBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
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
        DependencyProperty.Register("Hint", typeof(string), typeof(EnhancedTextBox),
            new FrameworkPropertyMetadata(""/*, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault*/, new PropertyChangedCallback(OnHintChanged)));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        private static void OnHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
            ThisUserControl.OnHintChanged(e);
        }

        private void OnHintChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(EnhancedTextBox),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            CustomTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        #region ModValue
        public static readonly DependencyProperty ModValueProperty =
            DependencyProperty.Register("ModValue", typeof(ModTypeBase), typeof(EnhancedTextBox),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModValuePropertyChanged)));

        public ModTypeBase ModValue
        {
            get => (ModTypeBase)GetValue(ModValueProperty);
            set => SetValue(ModValueProperty, value);
        }

        private static void ModValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
            ThisUserControl.ModValuePropertyChanged(e);
        }

        private void ModValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region CanOpenModal
        public static readonly DependencyProperty CanOpenModalProperty =
        DependencyProperty.Register("CanOpenModal", typeof(bool), typeof(EnhancedTextBox),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CanOpenModalPropertyChanged)));

        public bool CanOpenModal
        {
            get => (bool)GetValue(CanOpenModalProperty);
            set => SetValue(CanOpenModalProperty, value);
        }

        private static void CanOpenModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
            ThisUserControl.CanOpenModalPropertyChanged(e);
            ThisUserControl.BindOpenModifierWindowCommand();
        }

        private void CanOpenModalPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region BackgroundBox
        public static readonly DependencyProperty TextBoxBackgroundProperty =
        DependencyProperty.Register(
            nameof(TextBoxBackground),
            typeof(Brush),
            typeof(EnhancedTextBox),
            new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003300"))
        ));

        public Brush TextBoxBackground
        {
            get => (Brush)GetValue(TextBoxBackgroundProperty);
            set => SetValue(TextBoxBackgroundProperty, value);
        }
        #endregion

        #region Foreground
        public static readonly DependencyProperty TextBoxForegroundProperty =
        DependencyProperty.Register(
            nameof(TextBoxForeground),
            typeof(Brush),
            typeof(EnhancedTextBox),
            new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#23ba03"))
        ));

        public Brush TextBoxForeground
        {
            get => (Brush)GetValue(TextBoxForegroundProperty);
            set => SetValue(TextBoxForegroundProperty, value);
        }
        #endregion

        #region TextWrapping
        public static readonly DependencyProperty TextWrappingProperty =
        DependencyProperty.Register(
            nameof(TextWrapping),
            typeof(TextWrapping),
            typeof(EnhancedTextBox),
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
            typeof(EnhancedTextBox),
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
            typeof(EnhancedTextBox),
            new PropertyMetadata(false)); // default

        public bool AllowTabs
        {
            get => (bool)GetValue(AllowTabsProperty);
            set => SetValue(AllowTabsProperty, value);
        }
        #endregion

        #region TextPadding
        public static readonly DependencyProperty TextPaddingProperty =
        DependencyProperty.Register("TextPadding", typeof(Thickness), typeof(EnhancedTextBox),
            new FrameworkPropertyMetadata(new Thickness(2, 3, 2, 3), new PropertyChangedCallback(TextPaddingPropertyChanged)));

        public Thickness TextPadding
        {
            get => (Thickness)GetValue(TextPaddingProperty);
            set => SetValue(TextPaddingProperty, value);
        }

        private static void TextPaddingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnhancedTextBox? ThisUserControl = d as EnhancedTextBox;
            ThisUserControl.TextPaddingPropertyChanged(e);
        }

        private void TextPaddingPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion
        //

        public EnhancedTextBox()
        {
            InitializeComponent();

            Loaded += (s, e) => BindOpenModifierWindowCommand();
            //DataContextChanged += (s, e) => BindOpenModifierWindowCommand();
        }

        private void CustomTextBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!CanOpenModal)
            {
                e.Handled = true;
            }
        }

        private void ClearMouseBindings()
        {
            var existingBindings = CustomTextBox.InputBindings
                .OfType<MouseBinding>()
                .Where(mouseBinding => mouseBinding.Gesture is MouseGesture mouseGesture && mouseGesture.MouseAction == MouseAction.RightClick)
                .ToList();

            foreach (var mouseBinding in existingBindings)
                CustomTextBox.InputBindings.Remove(mouseBinding);
        }

        private void BindOpenModifierWindowCommand()
        {
            ClearMouseBindings();

            // add binding if OpenModal is true
            if (CanOpenModal && DataContext != null)
            {
                var binding = new MouseBinding
                {
                    Gesture = new MouseGesture(MouseAction.RightClick)
                };

                // bind command
                BindingOperations.SetBinding(binding, MouseBinding.CommandProperty,
                    new Binding("OpenModifierModalWindowCommand") { Source = DataContext });

                // bind param
                BindingOperations.SetBinding(binding, MouseBinding.CommandParameterProperty,
                    new Binding(nameof(ModValue)) { Source = this });

                CustomTextBox.InputBindings.Add(binding);
            }
        }
    }
}
