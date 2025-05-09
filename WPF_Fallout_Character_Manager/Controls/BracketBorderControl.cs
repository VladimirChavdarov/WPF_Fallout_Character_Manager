using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Fallout_Character_Manager.Controls
{
    class BracketBorderControl : ContentControl
    {
        public static readonly DependencyProperty BrushThicknessProperty =
            DependencyProperty.Register(
                "BrushThickness",
                typeof(double),
                typeof(BracketBorderControl),
                new PropertyMetadata(1.0)
                );

        public double BrushThickness
        {
            get => (double)GetValue( BrushThicknessProperty );
            set => SetValue( BrushThicknessProperty, value );
        }

        static BracketBorderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BracketBorderControl), new FrameworkPropertyMetadata(typeof(BracketBorderControl)));
        }
    }
}
