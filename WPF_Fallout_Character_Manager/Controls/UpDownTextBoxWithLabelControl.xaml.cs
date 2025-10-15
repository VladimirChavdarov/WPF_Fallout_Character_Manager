﻿using System;
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
    /// Interaction logic for UpDownTextBoxWithLabelControl.xaml
    /// </summary>
    public partial class UpDownTextBoxWithLabelControl : UserControl
    {
        // Dependency Properties
        #region TextBox
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
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
        DependencyProperty.Register("Hint", typeof(string), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnHintChanged)));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        private static void OnHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
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
            DependencyProperty.Register("ModValue", typeof(ModTypeBase), typeof(UpDownTextBoxWithLabelControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModValuePropertyChanged)));

        public ModTypeBase ModValue
        {
            get => (ModTypeBase)GetValue(ModValueProperty);
            set => SetValue(ModValueProperty, value);
        }

        private static void ModValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.ModValuePropertyChanged(e);
        }

        private void ModValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region Orientation
        public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(Orientation), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnOrientationChanged)));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.OnOrientationChanged(e);
        }

        private void OnOrientationChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region Label
        public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register("LabelText", typeof(string), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnLabelTextChanged)));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        private static void OnLabelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.OnLabelTextChanged(e);
        }

        private void OnLabelTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region LabelFontSize
        public static readonly DependencyProperty LabelFontSizeProperty =
        DependencyProperty.Register("LabelFontSize", typeof(int), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnLabelFontSizeChanged)));

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        private static void OnLabelFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
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
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            //CustomEnhancedTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        #region CanOpenModal
        public static readonly DependencyProperty CanOpenModalProperty =
        DependencyProperty.Register("CanOpenModal", typeof(bool), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CanOpenModalPropertyChanged)));

        public bool CanOpenModal
        {
            get => (bool)GetValue(CanOpenModalProperty);
            set => SetValue(CanOpenModalProperty, value);
        }

        private static void CanOpenModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.CanOpenModalPropertyChanged(e);
        }

        private void CanOpenModalPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region MaxValue
        public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register("MaxValue", typeof(int), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(2147483647, new PropertyChangedCallback(MaxValuePropertyChanged)));

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        private static void MaxValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.MaxValuePropertyChanged(e);
        }

        private void MaxValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region MinValue
        public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register("MinValue", typeof(int), typeof(UpDownTextBoxWithLabelControl),
            new FrameworkPropertyMetadata(-2147483648, new PropertyChangedCallback(MinValuePropertyChanged)));

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        private static void MinValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDownTextBoxWithLabelControl? ThisUserControl = d as UpDownTextBoxWithLabelControl;
            ThisUserControl.MinValuePropertyChanged(e);
        }

        private void MinValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion
        //

        public UpDownTextBoxWithLabelControl()
        {
            InitializeComponent();
        }
    }
}
