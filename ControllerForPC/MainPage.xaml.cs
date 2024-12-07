using ControllerForPC.Utilities;
using Microsoft.Maui.Layouts;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;

namespace ControllerForPC
{
    public partial class MainPage : ContentPage
    {
        readonly TcpConnection tcp;
        readonly JoystickUtility leftJoystick;
        readonly JoystickUtility rightJoystick;
        double screenWidth;
        double screenHeight;

        public MainPage()
        {
            InitializeComponent();

            tcp = new();
            leftJoystick = new(LeftJoystickArea, LeftJoystickBall);
            rightJoystick = new(RightJoystickArea, RightJoystickBall);

            var accelerometer = Accelerometer.Default;
            accelerometer.Start(SensorSpeed.Game);
            accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            try
            {
                await tcp.ConnectToServerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Sunucuya bağlanılamadı: {e}");
                throw;
            }
        }

        double lastAxisY = 0;
        private async void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            double axisY = Math.Round(e.Reading.Acceleration.Y, 2);
            if(axisY != lastAxisY)
            {
                await tcp.SendMessageAsync($"Accelerometer:{axisY}");
                lastAxisY = axisY;
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Dispatcher.DispatchAsync(() =>
            {
                screenWidth = ContentLayout.Width;
                screenHeight = ContentLayout.Height;
                
                ContentLayout.SetLayoutBounds(XYABLayout, new Rect(0.9, 0.1, screenHeight * 0.5, screenHeight * 0.5));
                ContentLayout.SetLayoutBounds(DirectionalLayout, new Rect(0.1, 0.1, screenHeight * 0.4, screenHeight * 0.4));

                ContentLayout.SetLayoutBounds(ShareButton, new Rect(0.43, 0.35, screenHeight * 0.1, screenHeight * 0.05));
                ContentLayout.SetLayoutBounds(MenuButton, new Rect(0.57, 0.35, screenHeight * 0.1, screenHeight * 0.05));

                ContentLayout.SetLayoutBounds(LeftButtonLayout, new Rect(0.35, 0, screenHeight * 0.16, screenHeight * 0.24));
                ContentLayout.SetLayoutBounds(RightButtonLayout, new Rect(0.65, 0, screenHeight * 0.16, screenHeight * 0.24));

                ContentLayout.SetLayoutBounds(LeftJoystickLayout, new Rect(0.32, 0.7, screenHeight * 0.3 / screenWidth, screenHeight * 0.3 / screenHeight));
                ContentLayout.SetLayoutBounds(RightJoystickLayout, new Rect(0.68, 0.7, screenHeight * 0.3 / screenWidth, screenHeight * 0.3 / screenHeight));

                ContentLayout.SetLayoutBounds(BackgroundView, new Rect(0, 0, screenWidth, screenHeight));
            });

        }
        
        private void ChangeButtonColor(Button button, Color backgroundColor, Color textColor, bool vibrate = false)
        {
            button.BackgroundColor = backgroundColor;
            button.TextColor = textColor;
            if(vibrate)
            {
                Vibration.Vibrate(1);
            }
        }
        private async void LeftJoystickPan_PanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            (short X, short Y) = leftJoystick.JoystickPanController(e);
            await tcp.SendMessageAsync($"Analog LX:{X} LY:{Y}");
        }
        private async void RightJoystickPan_PanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            (short X, short Y) = rightJoystick.JoystickPanController(e);
            await tcp.SendMessageAsync($"Analog RX:{X} RY:{Y}");
        }
        private async void BButton_Pressed(object? sender, EventArgs e)
        {
            ChangeButtonColor(BButton, Color.FromRgba("#F00"), Colors.White, true);
            await tcp.SendMessageAsync("BButton_Pressed");
        }
        private async void BButton_Released(object? sender, EventArgs e)
        {
            ChangeButtonColor(BButton, Colors.Transparent, Color.FromRgba("#F00"));
            await tcp.SendMessageAsync("BButton_Released");
        }
        private async void AButton_Pressed(object? sender, EventArgs e)
        {
            ChangeButtonColor(AButton, Color.FromRgba("#0F0"), Colors.Black, true);
            await tcp.SendMessageAsync("AButton_Pressed");
        }
        private async void AButton_Released(object? sender, EventArgs e)
        {
            ChangeButtonColor(AButton, Colors.Transparent, Color.FromRgba("#0F0"));
            await tcp.SendMessageAsync("AButton_Released");
        }
        private async void XButton_Pressed(object? sender, EventArgs e)
        {
            ChangeButtonColor(XButton, Color.FromRgba("#0AF"), Colors.White, true);
            await tcp.SendMessageAsync("XButton_Pressed");
        }
        private async void XButton_Released(object? sender, EventArgs e)
        {
            ChangeButtonColor(XButton, Colors.Transparent, Color.FromRgba("#0AF"));
            await tcp.SendMessageAsync("XButton_Released");
        }
        private async void YButton_Pressed(object? sender, EventArgs e)
        {
            ChangeButtonColor(YButton, Colors.Yellow, Colors.Black, true);
            await tcp.SendMessageAsync("YButton_Pressed");
        }
        private async void YButton_Released(object? sender, EventArgs e)
        {
            ChangeButtonColor(YButton, Colors.Transparent, Colors.Yellow);
            await tcp.SendMessageAsync("YButton_Released");
        }
        private async void LeftButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(LeftButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("LeftButton_Pressed");
        }
        private async void LeftButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(LeftButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("LeftButton_Released");
        }
        private async void UpButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(UpButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("UpButton_Pressed");
        }
        private async void UpButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(UpButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("UpButton_Released");
        }
        private async void DownButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(DownButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("DownButton_Pressed");
        }
        private async void DownButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(DownButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("DownButton_Released");
        }
        private async void RightButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(RightButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("RightButton_Pressed");
        }
        private async void RightButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(RightButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("RightButton_Released");
        }
        private async void MenuButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(MenuButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("MenuButton_Pressed");
        }
        private async void MenuButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(MenuButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("MenuButton_Released");
        }
        private async void ShareButton_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(ShareButton, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("ShareButton_Pressed");
        }
        private async void ShareButton_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(ShareButton, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("ShareButton_Released");
        }
        private async void L1Button_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(L1Button, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("L1Button_Pressed");
        }
        private async void L1Button_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(L1Button, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("L1Button_Released");
        }
        private async void L2Button_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(L2Button, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("L2Button_Pressed");
        }
        private async void L2Button_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(L2Button, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("L2Button_Released");
        }
        private async void R1Button_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(R1Button, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("R1Button_Pressed");
        }
        private async void R1Button_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(R1Button, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("R1Button_Released");
        }
        private async void R2Button_Pressed(object sender, EventArgs e)
        {
            ChangeButtonColor(R2Button, Colors.White, Colors.Black, true);
            await tcp.SendMessageAsync("R2Button_Pressed");
        }
        private async void R2Button_Released(object sender, EventArgs e)
        {
            ChangeButtonColor(R2Button, Colors.Transparent, Colors.Gray);
            await tcp.SendMessageAsync("R2Button_Released");
        }
    }

}
