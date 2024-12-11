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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public DashboardPage(ConnectionManager connectionManager)
        {
            InitializeComponent();
            _vigemClient = new();
            _controller = _vigemClient.CreateXbox360Controller();
            _controller.Connect();
            _connectionManager = connectionManager;
            GamepadControl gamepadControl = new()
            {
                Width = 500
            };
            ContentPanel.Children.Add(gamepadControl);
            Listen();
        }

        private async void Listen()
        {
            while (true)
            {
                string message = await _connectionManager.ReceiveAsync();
                ProcessMessage(message);
            }
            
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
                }
                else if (command == "AButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.A, false);
                }
                else if (command == "BButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.B, true);
                }
                else if (command == "BButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.B, false);
                }
                else if (command == "XButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.X, true);
                }
                else if (command == "XButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.X, false);
                }
                else if (command == "YButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Y, true);
                }
                else if (command == "YButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Y, false);
                }
                else if (command == "UpButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Up, true);
                }
                else if (command == "UpButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Up, false);
                }
                else if (command == "DownButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Down, true);
                }
                else if (command == "DownButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Down, false);
                }
                else if (command == "LeftButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Left, true);
                }
                else if (command == "LeftButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Left, false);
                }
                else if (command == "RightButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Right, true);
                }
                else if (command == "RightButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Right, false);
                }
                else if (command == "ShareButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Back, true);
                }
                else if (command == "ShareButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Back, false);
                }
                else if (command == "MenuButton_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.Start, true);
                }
                else if (command == "MenuButton_Released")
                {
                    _controller.SetButtonState(Xbox360Button.Start, false);
                }
                else if (command == "L1Button_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.LeftShoulder, true);
                }
                else if (command == "L1Button_Released")
                {
                    _controller.SetButtonState(Xbox360Button.LeftShoulder, false);
                }
                else if (command == "R1Button_Pressed")
                {
                    _controller.SetButtonState(Xbox360Button.RightShoulder, true);
                }
                else if (command == "R1Button_Released")
                {
                    _controller.SetButtonState(Xbox360Button.RightShoulder, false);
                }
                else if (command == "L2Button_Pressed")
                {
                    _controller.SetSliderValue(Xbox360Slider.LeftTrigger, byte.MaxValue);
                }
                else if (command == "L2Button_Released")
                {
                    _controller.SetSliderValue(Xbox360Slider.LeftTrigger, byte.MinValue);
                }
                else if (command == "R2Button_Pressed")
                {
                    _controller.SetSliderValue(Xbox360Slider.RightTrigger, byte.MaxValue);
                }
                else if (command == "R2Button_Released")
                {
                    _controller.SetSliderValue(Xbox360Slider.RightTrigger, byte.MinValue);
                }
                else if (command.StartsWith("Analog"))
                {
                    try
                    {
                        string[] commandParts = command.Split(" ");
                        string[] xParts = commandParts[1].Split(":");
                        string[] yParts = commandParts[2].Split(":");
                        if (xParts[0] == "LX" && yParts[0] == "LY")
                        {
                            _controller.SetAxisValue(Xbox360Axis.LeftThumbX, short.Parse(xParts[1]));
                            _controller.SetAxisValue(Xbox360Axis.LeftThumbY, short.Parse(yParts[1]));
                        }
                        else if (xParts[0] == "RX" && yParts[0] == "RY")
                        {
                            _controller.SetAxisValue(Xbox360Axis.RightThumbX, short.Parse(xParts[1]));
                            _controller.SetAxisValue(Xbox360Axis.RightThumbY, short.Parse(yParts[1]));
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
    }
}
