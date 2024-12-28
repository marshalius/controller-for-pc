using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using Server.Pages.Components;
using Server.Services;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Diagnostics;

namespace Server.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        private readonly ConnectionManager _connectionManager;
        private readonly ViGEmClient _vigemClient;
        private readonly IXbox360Controller _controller;
        private readonly GamepadControl gamepadControl;
        public DashboardPage(ConnectionManager connectionManager)
        {
            InitializeComponent();
            _vigemClient = new();
            _controller = _vigemClient.CreateXbox360Controller();
            _controller.Connect();
            _connectionManager = connectionManager;
            gamepadControl = new()
            {
                Width = 500
            };
            GamepadPanel.Children.Add(gamepadControl);
            Listen();

            
        }

        private async void Listen()
        {
            while (true)
            {
                try
                {
                    string message = await _connectionManager.ReceiveAsync();
                    ChangeConnectionStatus(true);
                    Debug.WriteLine(message);
                    ProcessMessage(message);
                }
                catch (Exception)
                {
                    ChangeConnectionStatus(false);
                    _connectionManager.Disconnect();
                    await _connectionManager.CreateServerAsync();
                }
            }
            
            
        }

        private void ChangeConnectionStatus(bool isConnected)
        {
            ConnectionStatusEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString((isConnected) ? "#0f0" : "#f00"));
            ConnectionStatusLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString((isConnected) ? "#0f0" : "#f00"));
            ConnectionStatusLabel.Content = (isConnected) ? "Bağlı" : "Bağlantı kesildi";
        }

        private void ProcessMessage(string message)
        {
            Console.WriteLine($"İstemciden gelen mesaj: {message}");

            string[] messageParse = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (string command in messageParse)
            {
                if (command == "AButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.A, true);
                    gamepadControl.a_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f0"));
                }
                else if (command == "AButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.A, false);
                    gamepadControl.a_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "BButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.B, true);
                    gamepadControl.b_button_background.Fill = new SolidColorBrush(Colors.Red);
                }
                else if (command == "BButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.B, false);
                    gamepadControl.b_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "XButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.X, true);
                    gamepadControl.x_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0AF"));
                }
                else if (command == "XButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.X, false);
                    gamepadControl.x_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "YButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Y, true);
                    gamepadControl.y_button_background.Fill = new SolidColorBrush(Colors.Yellow);
                }
                else if (command == "YButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Y, false);
                    gamepadControl.y_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "UpButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Up, true);
                    gamepadControl.up_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "UpButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Up, false);
                    gamepadControl.up_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff101010"));
                }
                else if (command == "DownButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Down, true);
                    gamepadControl.down_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "DownButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Down, false);
                    gamepadControl.down_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff101010"));
                }
                else if (command == "LeftButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Left, true);
                    gamepadControl.left_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "LeftButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Left, false);
                    gamepadControl.left_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff101010"));
                }
                else if (command == "RightButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Right, true);
                    gamepadControl.right_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "RightButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Right, false);
                    gamepadControl.right_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff101010"));
                }
                else if (command == "ShareButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Back, true);
                    gamepadControl.share_button_background.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "ShareButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Back, false);
                    gamepadControl.share_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "MenuButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Start, true);
                    gamepadControl.menu_button_background.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "MenuButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Start, false);
                    gamepadControl.menu_button_background.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b4245"));
                }
                else if (command == "L1Button_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.LeftShoulder, true);
                    gamepadControl.l1_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "L1Button_Released")
                {
                    _controller.SetButtonState(Xbox360Button.LeftShoulder, false);
                    gamepadControl.l1_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff222629"));
                }
                else if (command == "R1Button_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.RightShoulder, true);
                    gamepadControl.r1_button.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "R1Button_Released")
                {
                    _controller.SetButtonState(Xbox360Button.RightShoulder, false);
                    gamepadControl.r1_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff222629"));
                }
                else if (command == "L2Button_Pressed")
                {
                    _controller.SetSliderValue(Xbox360Slider.LeftTrigger, byte.MaxValue);
                    gamepadControl.l2_trigger.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "L2Button_Released")
                {
                    _controller.SetSliderValue(Xbox360Slider.LeftTrigger, byte.MinValue);
                    gamepadControl.l2_trigger.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff1c1d21"));
                }
                else if (command == "R2Button_Pressed")
                {
                    _controller.SetSliderValue(Xbox360Slider.RightTrigger, byte.MaxValue);
                    gamepadControl.r2_trigger.Fill = new SolidColorBrush(Colors.White);
                }
                else if (command == "R2Button_Released")
                {
                    _controller.SetSliderValue(Xbox360Slider.RightTrigger, byte.MinValue);
                    gamepadControl.r2_trigger.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff1c1d21"));
                }
                else if (command == "XboxButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Guide, true);
                    gamepadControl.xbox_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f0"));
                }
                else if (command == "XboxButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Guide, false);
                    gamepadControl.xbox_button.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff222629"));
                }
                else if (command.StartsWith("Analog"))
                {
                    try
                    {
                        string[] commandParts = command.Split(" ");
                        string[] xParts = commandParts[1].Split(":");
                        string[] yParts = commandParts[2].Split(":");
                        short x = short.Parse(xParts[1]);
                        short y = short.Parse(yParts[1]);
                        if (xParts[0] == "LX" && yParts[0] == "LY")
                        {
                            _controller.SetAxisValue(Xbox360Axis.LeftThumbX, x);
                            _controller.SetAxisValue(Xbox360Axis.LeftThumbY, y);
                            SetLeftAnalogMovement(x, y);
                            leftAnalogX.Content = x.ToString();
                            leftAnalogY.Content = y.ToString();
                        }
                        else if (xParts[0] == "RX" && yParts[0] == "RY")
                        {
                            _controller.SetAxisValue(Xbox360Axis.RightThumbX, short.Parse(xParts[1]));
                            _controller.SetAxisValue(Xbox360Axis.RightThumbY, short.Parse(yParts[1]));
                            SetRightAnalogMovement(x, y);
                            rightAnalogX.Content = x.ToString();
                            rightAnalogY.Content = y.ToString();
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
                else if (command.StartsWith("Accelerometer"))
                {
                    try
                    {
                        float sensitivity = 0.9f;
                        string[] commandParts = command.Split(":");
                        float yValue = Math.Clamp(float.Parse(commandParts[1]), -1 * sensitivity, sensitivity);
                        short xAxis = (short)(yValue / sensitivity * short.MaxValue);
                        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, xAxis);

                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void SetLeftAnalogMovement(short x, short y)
        {

            (int movementX, int movementY) = AxisNormalization(x, y, 1000);
            
            Canvas.SetTop(gamepadControl.left_analog_stick, movementY);
            Canvas.SetLeft(gamepadControl.left_analog_stick, movementX);


            if (x != 0 || y != 0)
            {
                gamepadControl.left_analog_stick_edge.Fill = new SolidColorBrush(Colors.White);
            }
            else
            {
                gamepadControl.left_analog_stick_edge.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff22272a"));
            }


        }
        private void SetRightAnalogMovement(short x, short y)
        {

            (int movementX, int movementY) = AxisNormalization(x, y, 1000);

            Canvas.SetTop(gamepadControl.right_analog_stick, movementY);
            Canvas.SetLeft(gamepadControl.right_analog_stick, movementX);

            if(x != 0 || y != 0)
            {
                gamepadControl.right_analog_stick_edge.Fill = new SolidColorBrush(Colors.White);
            }
            else
            {
                gamepadControl.right_analog_stick_edge.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff22272a"));
            }



        }

        private (int axisX, int axisY) AxisNormalization(double x, double y, double max)
        {
            int xNegative = ((x < 0) ? -1 : 1);
            int yNegative = ((y < 0) ? -1 : 1);
            x = Math.Abs(x);
            y = Math.Abs(y);
            if (Math.Abs(x) > Math.Abs(y))
            {
                double ratio = Math.Abs(x / y);
                y = x / Math.Sqrt(ratio * ratio + 1);
                x = y * ratio;
            }
            else
            {
                double ratio = Math.Abs(y / x);
                x = y / Math.Sqrt(ratio * ratio + 1);
                y = x * ratio;
            }

            x *= xNegative;
            y *= yNegative;

            double axisX = x / short.MaxValue * max;
            double axisY = -y / short.MaxValue * max;
            return ((short)axisX, (short)axisY);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}
