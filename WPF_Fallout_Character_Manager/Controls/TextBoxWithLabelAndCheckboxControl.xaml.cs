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
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.ViewModels;

namespace WPF_Fallout_Character_Manager.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxWithLabelAndCheckboxControl.xaml
    /// </summary>
    public partial class TextBoxWithLabelAndCheckboxControl : UserControl
    {
        //Dependency Properties
        #region Label
        public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register("LabelText", typeof(string), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnLabelTextChanged)));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        private static void OnLabelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
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
        DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ModIntSkill
        public static readonly DependencyProperty ModIntSkillProperty =
            DependencyProperty.Register("ModIntSkill", typeof(ModIntSkill), typeof(TextBoxWithLabelAndCheckboxControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ModIntSkillPropertyChanged)));

        public ModIntSkill ModIntSkill
        {
            get => (ModIntSkill)GetValue(ModIntSkillProperty);
            set => SetValue(ModIntSkillProperty, value);
        }

        private static void ModIntSkillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.ModIntSkillPropertyChanged(e);
        }

        private void ModIntSkillPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region SkillKey
        public static readonly DependencyProperty SkillKeyProperty =
            DependencyProperty.Register("SkillKey", typeof(Skill), typeof(TextBoxWithLabelAndCheckboxControl),
                new FrameworkPropertyMetadata(Skill.Barter, new PropertyChangedCallback(SkillKeyPropertyChanged)));

        public Skill SkillKey
        {
            get => (Skill)GetValue(SkillKeyProperty);
            set => SetValue(SkillKeyProperty, value);
        }

        private static void SkillKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.SkillKeyPropertyChanged(e);
        }

        private void SkillKeyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region Orientation
        public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnOrientationChanged)));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
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
        DependencyProperty.Register("LabelFontSize", typeof(int), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnLabelFontSizeChanged)));

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        private static void OnLabelFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.OnLabelFontSizeChanged(e);
        }

        private void OnLabelFontSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region CheckboxSize
        public static readonly DependencyProperty CheckboxSizeProperty =
        DependencyProperty.Register("CheckboxSize", typeof(int), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnCheckboxSizeChanged)));

        public int CheckboxSize
        {
            get { return (int)GetValue(CheckboxSizeProperty); }
            set { SetValue(CheckboxSizeProperty, value); }
        }

        private static void OnCheckboxSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.OnCheckboxSizeChanged(e);
        }

        private void OnCheckboxSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            // This overrides the field and doesn't allow a new value to appear if the Model changed.
            //CustomTextBox.Text = e.NewValue.ToString();
        }
        #endregion

        #region ReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void IsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.IsReadOnlyPropertyChanged(e);
        }

        private void IsReadOnlyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //CustomTextBox.IsReadOnly = (bool)e.NewValue;
            LabeledTextBox.IsReadOnly = (bool)GetValue(IsReadOnlyProperty);
        }
        #endregion

        #region CanOpenModal
        public static readonly DependencyProperty CanOpenModalProperty =
        DependencyProperty.Register("CanOpenModal", typeof(bool), typeof(TextBoxWithLabelAndCheckboxControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CanOpenModalPropertyChanged)));

        public bool CanOpenModal
        {
            get => (bool)GetValue(CanOpenModalProperty);
            set => SetValue(CanOpenModalProperty, value);
        }

        private static void CanOpenModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithLabelAndCheckboxControl? ThisUserControl = d as TextBoxWithLabelAndCheckboxControl;
            ThisUserControl.CanOpenModalPropertyChanged(e);
        }

        private void CanOpenModalPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            CanOpenModal = (bool)GetValue(CanOpenModalProperty);
        }
        #endregion
        //

        public TextBoxWithLabelAndCheckboxControl()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton rb && rb.DataContext is SPECIAL special)
            {
                ModIntSkill.SelectedModifier = special;
            }
        }
    }
}
