using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ChunkyCaretTextBox.xaml
    /// </summary>
     
    public partial class ChunkyCaretTextBox : UserControl
    {
        // Dependency Properties
        #region TextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(ChunkyCaretTextBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChunkyCaretTextBox? ThisUserControl = d as ChunkyCaretTextBox;
            ThisUserControl.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ChunkyCaretTextBox),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChunkyCaretTextBox? ThisUserControl = d as ChunkyCaretTextBox;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            CustomTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        #region ModInt
        public static readonly DependencyProperty ModIntProperty =
            DependencyProperty.Register("ModInt", typeof(ModInt), typeof(ChunkyCaretTextBox),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModIntPropertyChanged)));

        public ModInt ModInt
        {
            get => (ModInt)GetValue(ModIntProperty);
            set => SetValue(ModIntProperty, value);
        }

        private static void ModIntPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChunkyCaretTextBox? ThisUserControl = d as ChunkyCaretTextBox;
            ThisUserControl.ModIntPropertyChanged(e);
        }

        private void ModIntPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }
        #endregion
        //

        public ChunkyCaretTextBox()
        {
            InitializeComponent();

            // Caret setup
            this.CustomTextBox.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                Caret.Visibility = Visibility.Visible;
                MoveCustomCaret();
            };
            this.CustomTextBox.SelectionChanged += (sender, e) => MoveCustomCaret();
            this.CustomTextBox.LostFocus += (sender, e) => Caret.Visibility = Visibility.Collapsed;
            this.CustomTextBox.GotFocus += (sender, e) => Caret.Visibility = Visibility.Visible;
            this.CustomTextBox.SelectionChanged += CustomTextBox_SelectionChanged;
            //
        }

        private void MoveCustomCaret()
        {
            var caretLocation = CustomTextBox.GetRectFromCharacterIndex(CustomTextBox.CaretIndex).Location;

            //Debug.WriteLine($"Char before caret: '{caretLocation}'");
            if (!double.IsInfinity(caretLocation.X))
            {
                Canvas.SetLeft(Caret, caretLocation.X);
            }

            if (!double.IsInfinity(caretLocation.Y))
            {
                Canvas.SetTop(Caret, caretLocation.Y);
            }
        }

        private void CustomTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int caretIndex = CustomTextBox.CaretIndex;
            double charWidth = 0;

            // This should be true if there is at least one character 
            if (caretIndex < CustomTextBox.Text.Length)
            {
                char charBefore = CustomTextBox.Text[caretIndex];

                var typeface = new Typeface(CustomTextBox.FontFamily, CustomTextBox.FontStyle,
                                            CustomTextBox.FontWeight, CustomTextBox.FontStretch);

                var formattedText = new FormattedText(
                    charBefore.ToString(),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    typeface,
                    CustomTextBox.FontSize,
                    Brushes.Red,
                    new NumberSubstitution(),
                    TextFormattingMode.Ideal,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);

                charWidth = formattedText.WidthIncludingTrailingWhitespace + 1;
            }

            // Fallback to default size
            if (charWidth <= 0)
                Caret.Width = CustomTextBox.FontSize;
            else
                Caret.Width = Math.Max(1, charWidth);
        }
    }
}
