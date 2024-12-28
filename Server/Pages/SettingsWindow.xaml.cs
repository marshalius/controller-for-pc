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
        int leftAnalogXValue = 0;
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox box)
            {
                switch(box.Name)
                {
                    case "LeftAnalogXTextBox":
                        LeftAnalogXSlider.Value = Convert.ToInt32(LeftAnalogXTextBox.Text);
                        break;
                    case "LeftAnalogYTextBox":
                        LeftAnalogYSlider.Value = Convert.ToInt32(LeftAnalogYTextBox.Text);
                        break;
                    case "LeftAnalogWidthTextBox":
                        LeftAnalogWidthSlider.Value = Convert.ToInt32(LeftAnalogWidthTextBox.Text);
                        break;
                    case "LeftAnalogHeightTextBox":
                        LeftAnalogHeightSlider.Value = Convert.ToInt32(LeftAnalogHeightTextBox.Text);
                        break;
                    case "LeftAnalogSensitivityTextBox":
                        LeftAnalogSensitivitySlider.Value = Convert.ToInt32(LeftAnalogSensitivityTextBox.Text);
                        break;
                    case "RightAnalogXTextBox":
                        RightAnalogXSlider.Value = Convert.ToInt32(RightAnalogXTextBox.Text);
                        break;
                    case "RightAnalogYTextBox":
                        RightAnalogYSlider.Value = Convert.ToInt32(RightAnalogYTextBox.Text);
                        break;
                    case "RightAnalogWidthTextBox":
                        RightAnalogWidthSlider.Value = Convert.ToInt32(RightAnalogWidthTextBox.Text);
                        break;
                    case "RightAnalogHeightTextBox":
                        RightAnalogHeightSlider.Value = Convert.ToInt32(RightAnalogHeightTextBox.Text);
                        break;
                    case "RightAnalogSensitivityTextBox":
                        RightAnalogSensitivitySlider.Value = Convert.ToInt32(RightAnalogSensitivityTextBox.Text);
                        break;
                }
                    
            }
            
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider slider)
            {
                switch (slider.Name)
                {
                    case "LeftAnalogXSlider":
                        LeftAnalogXTextBox.Text = ((int)LeftAnalogXSlider.Value).ToString();
                        break;
                }

            }
        }
    }
}
