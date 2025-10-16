using System;
using System.Collections.Generic;
using System.Globalization;
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
            
        }
        #endregion

        #region Hint
        public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnHintChanged)));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        private static void OnHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.OnHintChanged(e);
        }

        private void OnHintChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region IsValueFloat
        public static readonly DependencyProperty IsValueFloatProperty =
        DependencyProperty.Register("IsValueFloat", typeof(bool), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsValueFloatChanged)));

        public bool IsValueFloat
        {
            get { return (bool)GetValue(IsValueFloatProperty); }
            set { SetValue(IsValueFloatProperty, value); }
        }

        private static void OnIsValueFloatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.OnIsValueFloatChanged(e);
        }

        private void OnIsValueFloatChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ModValue
        public static readonly DependencyProperty ModValueProperty =
            DependencyProperty.Register("ModValue", typeof(ModTypeBase), typeof(UpDownTextBoxControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModValuePropertyChanged)));

        public ModTypeBase ModValue
        {
            get => (ModTypeBase)GetValue(ModValueProperty);
            set => SetValue(ModValueProperty, value);
        }

        private static void ModValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.ModValuePropertyChanged(e);
        }

        private void ModValuePropertyChanged(DependencyPropertyChangedEventArgs e)
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

        #region CanOpenModal
        public static readonly DependencyProperty CanOpenModalProperty =
        DependencyProperty.Register("CanOpenModal", typeof(bool), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CanOpenModalPropertyChanged)));

        public bool CanOpenModal
        {
            get => (bool)GetValue(CanOpenModalProperty);
            set => SetValue(CanOpenModalProperty, value);
        }

        private static void CanOpenModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.CanOpenModalPropertyChanged(e);
        }

        private void CanOpenModalPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region MaxValue
        public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register("MaxValue", typeof(int), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(2147483647, new PropertyChangedCallback(MaxValuePropertyChanged)));

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        private static void MaxValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.MaxValuePropertyChanged(e);
        }

        private void MaxValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region MinValue
        public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register("MinValue", typeof(int), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(-2147483648, new PropertyChangedCallback(MinValuePropertyChanged)));

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        private static void MinValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.MinValuePropertyChanged(e);
        }

        private void MinValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region TextBoxSlot
        public static readonly DependencyProperty TextBoxSlotProperty =
        DependencyProperty.Register("TextBoxSlot", typeof(int), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(TextBoxSlotPropertyChanged)));

        public int TextBoxSlot
        {
            get => (int)GetValue(TextBoxSlotProperty);
            set => SetValue(TextBoxSlotProperty, value);
        }

        private static void TextBoxSlotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.TextBoxSlotPropertyChanged(e);
        }

        private void TextBoxSlotPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region PlusButtonSlot
        public static readonly DependencyProperty PlusButtonSlotProperty =
        DependencyProperty.Register("PlusButtonSlot", typeof(int), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(2, new PropertyChangedCallback(PlusButtonSlotPropertyChanged)));

        public int PlusButtonSlot
        {
            get => (int)GetValue(PlusButtonSlotProperty);
            set => SetValue(PlusButtonSlotProperty, value);
        }

        private static void PlusButtonSlotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.PlusButtonSlotPropertyChanged(e);
        }

        private void PlusButtonSlotPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region MinusButtonSlot
        public static readonly DependencyProperty MinusButtonSlotProperty =
        DependencyProperty.Register("MinusButtonSlot", typeof(int), typeof(UpDownTextBoxControl),
            new FrameworkPropertyMetadata(1, new PropertyChangedCallback(MinusButtonSlotPropertyChanged)));

        public int MinusButtonSlot
        {
            get => (int)GetValue(MinusButtonSlotProperty);
            set => SetValue(MinusButtonSlotProperty, value);
        }

        private static void MinusButtonSlotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxControl? ThisUserControl = d as UpDownTextBoxControl;
            ThisUserControl.MinusButtonSlotPropertyChanged(e);
        }

        private void MinusButtonSlotPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
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
                    if (value > MaxValue)
                        value = MaxValue;
                    if (value < MinValue)
                        value = MinValue;
                    Text = value.ToString();
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
                    if(value > MaxValue)
                        value = MaxValue;
                    if (value < MinValue)
                        value = MinValue;
                    Text = value.ToString();
                }
            }
        }

        private void CustomEnhancedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && sender is EnhancedTextBox textbox)
            {
                BindingExpression binding = textbox.GetBindingExpression(EnhancedTextBox.TextProperty);
                binding?.UpdateSource();

                Keyboard.ClearFocus();
                e.Handled = true;
            }
        }
    }
}
