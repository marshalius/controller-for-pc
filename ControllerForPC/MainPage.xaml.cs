using Microsoft.Maui.Layouts;

namespace ControllerForPC
{
    public partial class MainPage : ContentPage
    {
        double screenWidth;
        double screenHeight;
        

        Frame joystick = new Frame { BorderColor = Colors.Black, BackgroundColor = Colors.Transparent, };
        Button YButton;
        Button XButton;
        Button AButton;
        Button BButton;
        Grid XYABLayout;
        AbsoluteLayout ContentLayout;

        public MainPage()
        {
            InitializeComponent();
#if ANDROID
            screenWidth = DeviceDisplay.MainDisplayInfo.Height;
            screenHeight = DeviceDisplay.MainDisplayInfo.Width;
#elif WINDOWS
            screenWidth = DeviceDisplay.MainDisplayInfo.Width;
            screenHeight = DeviceDisplay.MainDisplayInfo.Height;
#endif
            int corner = Convert.ToInt32(screenHeight * 0.0675);
            YButton = new Button { Text = "Y", FontSize = 48, CornerRadius = short.MaxValue, FontAttributes = FontAttributes.Bold, TextColor = Colors.Yellow, BackgroundColor = Colors.Transparent, BorderColor = Colors.Gray, BorderWidth = 2 };
            XButton = new Button { Text = "X", FontSize = 48, CornerRadius = short.MaxValue, FontAttributes = FontAttributes.Bold, TextColor = Color.FromRgba("#0AF"), BackgroundColor = Colors.Transparent, BorderColor = Colors.Gray, BorderWidth = 2 };
            AButton = new Button { Text = "A", FontSize = 48, CornerRadius = short.MaxValue, FontAttributes = FontAttributes.Bold, TextColor = Color.FromRgba("#0F0"), BackgroundColor = Colors.Transparent, BorderColor = Colors.Gray, BorderWidth = 2 };
            BButton = new Button { Text = "B", FontSize = 48, CornerRadius = short.MaxValue, FontAttributes = FontAttributes.Bold, TextColor = Color.FromRgba("#F00"), BackgroundColor = Colors.Transparent, BorderColor = Colors.Gray, BorderWidth = 2 };


            ContentLayout = new AbsoluteLayout();
            XYABLayout = new Grid();

            XYABLayout.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            XYABLayout.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            XYABLayout.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            XYABLayout.AddRowDefinition(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            XYABLayout.AddRowDefinition(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            XYABLayout.AddRowDefinition(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            XYABLayout.Children.Add(XButton);
            XYABLayout.Children.Add(YButton);
            XYABLayout.Children.Add(AButton);
            XYABLayout.Children.Add(BButton);

            XYABLayout.SetColumn(XButton, 0);
            XYABLayout.SetColumn(YButton, 1);
            XYABLayout.SetColumn(AButton, 1);
            XYABLayout.SetColumn(BButton, 2);

            XYABLayout.SetRow(XButton, 1);
            XYABLayout.SetRow(YButton, 0);
            XYABLayout.SetRow(AButton, 2);
            XYABLayout.SetRow(BButton, 1);

            Content = ContentLayout;

            ContentLayout.SetLayoutBounds(XYABLayout, new Rect(0.9, 0.5, screenHeight * 0.4/screenWidth, screenHeight * 0.4/screenHeight));
            ContentLayout.SetLayoutFlags(XYABLayout, AbsoluteLayoutFlags.All);
            ContentLayout.Children.Add(XYABLayout);



            YButton.Pressed += YButton_Pressed;
            XButton.Pressed += XButton_Pressed;
            AButton.Pressed += AButton_Pressed;
            BButton.Pressed += BButton_Pressed;

            YButton.Released += YButton_Released;
            XButton.Released += XButton_Released;
            AButton.Released += AButton_Released;
            BButton.Released += BButton_Released;


            var joystickGesture = new PanGestureRecognizer();
            joystickGesture.PanUpdated += OnPanUpdated;
            ContentLayout.GestureRecognizers.Add(joystickGesture);

            ContentLayout.SetLayoutBounds(JoystickLayout, new Rect(0.15, 0.8, screenHeight*0.3/screenWidth, screenHeight * 0.3 / screenHeight));
            ContentLayout.SetLayoutFlags(JoystickLayout, AbsoluteLayoutFlags.All);
            ContentLayout.Children.Add(JoystickLayout);
        }
        private bool IsJoystickActivated = false;
        private void JoystickOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            IsJoystickActivated = true;
        }
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if(IsJoystickActivated)
            {
                if (e.StatusType == GestureStatus.Running)
                {
                    double x = e.TotalX;
                    double y = e.TotalY;


                    double distance = Math.Sqrt(x * x + y * y); // X ve Y'nin karelerinin toplamı (hipotenüs)

                    if (distance > JoystickArea.Width / 2)
                    {
                        // Eğer mesafe dairenin yarıçapını aşarsa, joystick'i daire sınırına kısıtla
                        double ratio = (JoystickArea.Width / 2) / distance;
                        x *= ratio;
                        y *= ratio;
                    }


                    double newX = x;
                    double newY = y;

                    // Joystick'in hareketini alandaki sınırlara göre güncelle
                    JoystickBall.TranslationX = newX;
                    JoystickBall.TranslationY = newY;
                }
                else if (e.StatusType == GestureStatus.Completed)
                {
                    JoystickBall.TranslationX = 0;
                    JoystickBall.TranslationY = 0;
                    IsJoystickActivated = false;
                }
            }
            
        }

        private void BButton_Pressed(object? sender, EventArgs e)
        {
            BButton.BackgroundColor = Color.FromRgba("#F00");
            BButton.TextColor = Colors.White;
        }
        private void BButton_Released(object? sender, EventArgs e)
        {
            BButton.BackgroundColor = Colors.Transparent;
            BButton.TextColor = Color.FromRgba("#F00");
        }
        private void AButton_Pressed(object? sender, EventArgs e)
        {
            AButton.BackgroundColor = Color.FromRgba("#0F0");
            AButton.TextColor = Colors.Black;
        }
        private void AButton_Released(object? sender, EventArgs e)
        {
            AButton.BackgroundColor = Colors.Transparent;
            AButton.TextColor = Color.FromRgba("#0F0");
        }
        private void XButton_Pressed(object? sender, EventArgs e)
        {
            XButton.BackgroundColor = Color.FromRgba("#00F");
            XButton.TextColor = Colors.White;
        }
        private void XButton_Released(object? sender, EventArgs e)
        {
            XButton.BackgroundColor = Colors.Transparent;
            XButton.TextColor = Color.FromRgba("#00F");
        }

        private void YButton_Pressed(object? sender, EventArgs e)
        {
            YButton.BackgroundColor = Colors.Yellow;
            YButton.TextColor = Colors.Black;
        }
        private void YButton_Released(object? sender, EventArgs e)
        {
            YButton.BackgroundColor = Colors.Transparent;
            YButton.TextColor = Colors.Yellow;
        }

        
    }

}
