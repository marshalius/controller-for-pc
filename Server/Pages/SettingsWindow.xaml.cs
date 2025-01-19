using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server.Pages
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        Dictionary<string, (bool isInt, Slider slider, TextBox textbox)> SliderAndTextBoxes;
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
            SliderAndTextBoxes = new()
            {
                { "LeftAnalogX",            (true,      LeftAnalogXSlider,              LeftAnalogXTextBox) },
                { "LeftAnalogY",            (true,      LeftAnalogYSlider,              LeftAnalogYTextBox) },
                { "LeftAnalogSize",         (true,      LeftAnalogSizeSlider,           LeftAnalogSizeTextBox) },
                { "LeftAnalogSensitivity",  (false,     LeftAnalogSensitivitySlider,    LeftAnalogSensitivityTextBox) },
                { "RightAnalogX",           (true,      RightAnalogXSlider,             RightAnalogXTextBox) },
                { "RightAnalogY",           (true,      RightAnalogYSlider,             RightAnalogYTextBox) },
                { "RightAnalogSize",        (true,      RightAnalogSizeSlider,          RightAnalogSizeTextBox) },
                { "RightAnalogSensitivity", (false,     RightAnalogSensitivitySlider,   RightAnalogSensitivityTextBox) },
                { "XYABX",                  (true,      XYABXSlider,                    XYABXTextBox) },
                { "XYABY",                  (true,      XYABYSlider,                    XYABYTextBox) },
                { "XYABSize",               (true,      XYABSizeSlider,                 XYABSizeTextBox) },
                { "DirectionalX",           (true,      DirectionalXSlider,             DirectionalXTextBox) },
                { "DirectionalY",           (true,      DirectionalYSlider,             DirectionalYTextBox) },
                { "DirectionalSize",        (true,      DirectionalSizeSlider,          DirectionalSizeTextBox) },
                { "R1ButtonX",              (true,      R1ButtonXSlider,                R1ButtonXTextBox) },
                { "R1ButtonY",              (true,      R1ButtonYSlider,                R1ButtonYTextBox) },
                { "R1ButtonWidth",          (true,      R1ButtonWidthSlider,            R1ButtonWidthTextBox) },
                { "R1ButtonHeight",         (true,      R1ButtonHeightSlider,           R1ButtonHeightTextBox) },
                { "R2TriggerX",             (true,      R2TriggerXSlider,               R2TriggerXTextBox) },
                { "R2TriggerY",             (true,      R2TriggerYSlider,               R2TriggerYTextBox) },
                { "R2TriggerWidth",         (true,      R2TriggerWidthSlider,           R2TriggerWidthTextBox) },
                { "R2TriggerHeight",        (true,      R2TriggerHeightSlider,          R2TriggerHeightTextBox) },
                { "L1ButtonX",              (true,      L1ButtonXSlider,                L1ButtonXTextBox) },
                { "L1ButtonY",              (true,      L1ButtonYSlider,                L1ButtonYTextBox) },
                { "L1ButtonWidth",          (true,      L1ButtonWidthSlider,            L1ButtonWidthTextBox) },
                { "L1ButtonHeight",         (true,      L1ButtonHeightSlider,           L1ButtonHeightTextBox) },
                { "L2TriggerX",             (true,      L2TriggerXSlider,               L2TriggerXTextBox) },
                { "L2TriggerY",             (true,      L2TriggerYSlider,               L2TriggerYTextBox) },
                { "L2TriggerWidth",         (true,      L2TriggerWidthSlider,           L2TriggerWidthTextBox) },
                { "L2TriggerHeight",        (true,      L2TriggerHeightSlider,          L2TriggerHeightTextBox) },
                { "MenuButtonX",            (true,      MenuButtonXSlider,              MenuButtonXTextBox) },
                { "MenuButtonY",            (true,      MenuButtonYSlider,              MenuButtonYTextBox) },
                { "MenuButtonWidth",        (true,      MenuButtonWidthSlider,          MenuButtonWidthTextBox) },
                { "MenuButtonHeight",       (true,      MenuButtonHeightSlider,         MenuButtonHeightTextBox) },
                { "ShareButtonX",           (true,      ShareButtonXSlider,             ShareButtonXTextBox) },
                { "ShareButtonY",           (true,      ShareButtonYSlider,             ShareButtonYTextBox) },
                { "ShareButtonWidth",       (true,      ShareButtonWidthSlider,         ShareButtonWidthTextBox) },
                { "ShareButtonHeight",      (true,      ShareButtonHeightSlider,        ShareButtonHeightTextBox) },
                { "XboxButtonX",            (true,      XboxButtonXSlider,              XboxButtonXTextBox) },
                { "XboxButtonY",            (true,      XboxButtonYSlider,              XboxButtonYTextBox) },
                { "XboxButtonSize",         (true,      XboxButtonSizeSlider,           XboxButtonSizeTextBox) },

            };

            
        }

        private void SyncTextBoxSlider(object sender)
        {
            if (sender is Slider slider)
            {
                var selected = SliderAndTextBoxes[slider.Tag.ToString()!];
                selected.textbox.Text = (selected.isInt ? ((int)slider.Value).ToString() : (Math.Round((double)slider.Value, 2)).ToString());
            }
            if (sender is TextBox box)
            {
                if (!CommaNumericRegex().IsMatch(box.Text))
                {
                    box.Text = "0";
                }
                var selected = SliderAndTextBoxes[box.Tag.ToString()!];
                selected.slider.Value = (selected.isInt ? Convert.ToInt32(box.Text) : Convert.ToDouble(box.Text));
            }
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SyncTextBoxSlider(sender);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SyncTextBoxSlider(sender);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex;
            if (sender is TextBox textBox)
            {
                if(textBox.Text.Contains(','))
                {
                    regex = NumericRegex();
                }
                else
                {
                    regex = CommaNumericRegex();
                }
                e.Handled = !regex.IsMatch(e.Text);
            }
            
            
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) SyncTextBoxSlider(sender);
        }

        [GeneratedRegex("[^0-9]+")]
        private static partial Regex NumericRegex();

        [GeneratedRegex("^[0-9,]*$")]
        private static partial Regex CommaNumericRegex();
    }
}
