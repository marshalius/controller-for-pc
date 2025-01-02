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
using System.Windows.Shapes;

namespace Server.Pages
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        Dictionary<string, (bool isInt, string? lockWith, CheckBox? lockCheckbox, Slider slider, TextBox textbox)> SliderAndTextBoxes;
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
            SliderAndTextBoxes = new()
            {
                { "LeftAnalogX",            (true,      null,                   null,                       LeftAnalogXSlider,            LeftAnalogXTextBox) },
                { "LeftAnalogY",            (true,      null,                   null,                       LeftAnalogYSlider,            LeftAnalogYTextBox) },
                { "LeftAnalogWidth",        (true,      "LeftAnalogHeight",     LeftAnalogSizeCheckBox,     LeftAnalogWidthSlider,        LeftAnalogWidthTextBox) },
                { "LeftAnalogHeight",       (true,      "LeftAnalogWidth",      LeftAnalogSizeCheckBox,     LeftAnalogHeightSlider,       LeftAnalogHeightTextBox) },
                { "LeftAnalogSensitivity",  (false,     null,                   null,                       LeftAnalogSensitivitySlider,  LeftAnalogSensitivityTextBox) },
                { "RightAnalogX",           (true,      null,                   null,                       RightAnalogXSlider,           RightAnalogXTextBox) },
                { "RightAnalogY",           (true,      null,                   null,                       RightAnalogYSlider,           RightAnalogYTextBox) },
                { "RightAnalogWidth",       (true,      "RightAnalogHeight",    RightAnalogSizeCheckBox,    RightAnalogWidthSlider,       RightAnalogWidthTextBox) },
                { "RightAnalogHeight",      (true,      "RightAnalogWidth",     RightAnalogSizeCheckBox,    RightAnalogHeightSlider,      RightAnalogHeightTextBox) },
                { "RightAnalogSensitivity", (false,     null,                   null,                       RightAnalogSensitivitySlider, RightAnalogSensitivityTextBox) },
                { "XYABX",                  (true,      null,                   null,                       XYABXSlider,                  XYABXTextBox) },
                { "XYABY",                  (true,      null,                   null,                       XYABYSlider,                  XYABYTextBox) },
                { "XYABWidth",              (true,      "XYABHeight",           XYABSizeCheckBox,           XYABWidthSlider,              XYABWidthTextBox) },
                { "XYABHeight",             (true,      "XYABWidth",            XYABSizeCheckBox,           XYABHeightSlider,             XYABHeightTextBox) },
                { "DirectionalX",           (true,      null,                   null,                       DirectionalXSlider,           DirectionalXTextBox) },
                { "DirectionalY",           (true,      null,                   null,                       DirectionalYSlider,           DirectionalYTextBox) },
                { "DirectionalWidth",       (true,      "DirectionalHeight",    DirectionalSizeCheckBox,    DirectionalWidthSlider,       DirectionalWidthTextBox) },
                { "DirectionalHeight",      (true,      "DirectionalWidth",     DirectionalSizeCheckBox,    DirectionalHeightSlider,      DirectionalHeightTextBox) },
                { "R1ButtonX",              (true,      null,                   null,                       R1ButtonXSlider,              R1ButtonXTextBox) },
                { "R1ButtonY",              (true,      null,                   null,                       R1ButtonYSlider,              R1ButtonYTextBox) },
                { "R1ButtonWidth",          (true,      "R1ButtonHeight",       R1ButtonSizeCheckBox,       R1ButtonWidthSlider,          R1ButtonWidthTextBox) },
                { "R1ButtonHeight",         (true,      "R1ButtonWidth",        R1ButtonSizeCheckBox,       R1ButtonHeightSlider,         R1ButtonHeightTextBox) },
                { "R2TriggerX",             (true,      null,                   null,                       R2TriggerXSlider,             R2TriggerXTextBox) },
                { "R2TriggerY",             (true,      null,                   null,                       R2TriggerYSlider,             R2TriggerYTextBox) },
                { "R2TriggerWidth",         (true,      "R2TriggerHeight",      R2TriggerSizeCheckBox,      R2TriggerWidthSlider,         R2TriggerWidthTextBox) },
                { "R2TriggerHeight",        (true,      "R2TriggerWidth",       R2TriggerSizeCheckBox,      R2TriggerHeightSlider,        R2TriggerHeightTextBox) },
                { "L1ButtonX",              (true,      null,                   null,                       L1ButtonXSlider,              L1ButtonXTextBox) },
                { "L1ButtonY",              (true,      null,                   null,                       L1ButtonYSlider,              L1ButtonYTextBox) },
                { "L1ButtonWidth",          (true,      "L1ButtonHeight",       L1ButtonSizeCheckBox,       L1ButtonWidthSlider,          L1ButtonWidthTextBox) },
                { "L1ButtonHeight",         (true,      "L1ButtonWidth",        L1ButtonSizeCheckBox,       L1ButtonHeightSlider,         L1ButtonHeightTextBox) },
                { "L2TriggerX",             (true,      null,                   null,                       L2TriggerXSlider,             L2TriggerXTextBox) },
                { "L2TriggerY",             (true,      null,                   null,                       L2TriggerYSlider,             L2TriggerYTextBox) },
                { "L2TriggerWidth",         (true,      "L2TriggerHeight",      L2TriggerSizeCheckBox,      L2TriggerWidthSlider,         L2TriggerWidthTextBox) },
                { "L2TriggerHeight",        (true,      "L2TriggerWidth",       L2TriggerSizeCheckBox,      L2TriggerHeightSlider,        L2TriggerHeightTextBox) },
                { "MenuButtonX",            (true,      null,                   null,                       MenuButtonXSlider,            MenuButtonXTextBox) },
                { "MenuButtonY",            (true,      null,                   null,                       MenuButtonYSlider,            MenuButtonYTextBox) },
                { "MenuButtonWidth",        (true,      "MenuButtonHeight",     MenuButtonSizeCheckBox,     MenuButtonWidthSlider,        MenuButtonWidthTextBox) },
                { "MenuButtonHeight",       (true,      "MenuButtonWidth",      MenuButtonSizeCheckBox,     MenuButtonHeightSlider,       MenuButtonHeightTextBox) },
                { "ShareButtonX",           (true,      null,                   null,                       ShareButtonXSlider,           ShareButtonXTextBox) },
                { "ShareButtonY",           (true,      null,                   null,                       ShareButtonYSlider,           ShareButtonYTextBox) },
                { "ShareButtonWidth",       (true,      "ShareButtonHeight",    ShareButtonSizeCheckBox,    ShareButtonWidthSlider,       ShareButtonWidthTextBox) },
                { "ShareButtonHeight",      (true,      "ShareButtonWidth",     ShareButtonSizeCheckBox,    ShareButtonHeightSlider,      ShareButtonHeightTextBox) },
                { "XboxButtonX",            (true,      null,                   null,                       XboxButtonXSlider,            XboxButtonXTextBox) },
                { "XboxButtonY",            (true,      null,                   null,                       XboxButtonYSlider,            XboxButtonYTextBox) },
                { "XboxButtonWidth",        (true,      "XboxButtonHeight",     XboxButtonSizeCheckBox,     XboxButtonWidthSlider,        XboxButtonWidthTextBox) },
                { "XboxButtonHeight",       (true,      "XboxButtonWidth",      XboxButtonSizeCheckBox,     XboxButtonHeightSlider,       XboxButtonHeightTextBox) },

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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) SyncTextBoxSlider(sender);
        }
    }
}
