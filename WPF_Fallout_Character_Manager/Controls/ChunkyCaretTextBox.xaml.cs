using System;
using System.Collections.Generic;
using System.Diagnostics;
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
namespace WPF_Fallout_Character_Manager.Controls
{
    /// <summary>
    /// Interaction logic for ChunkyCaretTextBox.xaml
    /// </summary>
    public partial class ChunkyCaretTextBox : UserControl
    {
        public ChunkyCaretTextBox()
        {
            InitializeComponent();

            this.CustomTextBox.SelectionChanged += (sender, e) => MoveCustomCaret();
            this.CustomTextBox.LostFocus += (sender, e) => Caret.Visibility = Visibility.Collapsed;
            this.CustomTextBox.GotFocus += (sender, e) => Caret.Visibility = Visibility.Visible;
            this.CustomTextBox.SelectionChanged += CustomTextBox_SelectionChanged;
        }

        private void MoveCustomCaret()
        {
            var caretLocation = CustomTextBox.GetRectFromCharacterIndex(CustomTextBox.CaretIndex).Location;

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
            if (caretIndex > 0 && caretIndex <= CustomTextBox.Text.Length)
            {
                double charWidth = 0;
                if(caretIndex+1 <= CustomTextBox.Text.Length)
                {
                    char charBefore = CustomTextBox.Text[caretIndex];
                    Debug.WriteLine($"Char before caret: '{caretIndex}', '{charBefore}'");

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

                    charWidth = formattedText.WidthIncludingTrailingWhitespace;

                }

                Caret.Width = Math.Max(20, charWidth);
            }
        }
    }
}
