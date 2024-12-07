using ControllerForPC.Services;
using ControllerForPC.Services.Contracts;
using ControllerForPC.Utilities;

namespace ControllerForPC.Pages;

public partial class GamepadPage : ContentPage
{
	private readonly ConnectionManager _connectionManager;
    readonly JoystickUtility leftJoystick;
    readonly JoystickUtility rightJoystick;
    readonly Dictionary<Button, (string name, Color backgroundColor, Color textColor)> buttonPressedProps;
    readonly Dictionary<Button, (string name, Color backgroundColor, Color textColor)> buttonReleasedProps;
    

    public GamepadPage(ConnectionManager connectionManager)
    {
        InitializeComponent();

#if ANDROID
        var activity = Platform.CurrentActivity;
        activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
#endif

        _connectionManager = connectionManager;
        
        leftJoystick = new(LeftJoystickArea, LeftJoystickBall);
        rightJoystick = new(RightJoystickArea, RightJoystickBall);

        buttonPressedProps = new(){
            { AButton, ("AButton", Color.FromRgba("#0F0"), Colors.Black)},
            { BButton, ("BButton", Color.FromRgba("#F00"), Colors.White)},
            { XButton, ("XButton", Color.FromRgba("#0AF"), Colors.White)},
            { YButton, ("YButton", Colors.Yellow, Colors.Black)},

            { UpButton, ("UpButton", Colors.White, Colors.Black)},
            { DownButton, ("DownButton", Colors.White, Colors.Black)},
            { LeftButton, ("LeftButton", Colors.White, Colors.Black)},
            { RightButton, ("RightButton", Colors.White, Colors.Black)},

            { MenuButton, ("MenuButton", Colors.White, Colors.Black)},
            { ShareButton, ("ShareButton", Colors.White, Colors.Black)},

            { L1Button, ("L1Button", Colors.White, Colors.Black)},
            { L2Button, ("L2Button", Colors.White, Colors.Black)},
            { R1Button, ("R1Button", Colors.White, Colors.Black)},
            { R2Button, ("R2Button", Colors.White, Colors.Black)},
        };

        buttonReleasedProps = new(){
            { AButton, ("AButton", Colors.Transparent,Color.FromRgba("#0F0"))},
            { BButton, ("BButton", Colors.Transparent, Color.FromRgba("#F00"))},
            { XButton, ("XButton", Colors.Transparent, Color.FromRgba("#0AF"))},
            { YButton, ("YButton", Colors.Transparent, Colors.Yellow)},

            { UpButton, ("UpButton", Colors.Transparent, Colors.Gray)},
            { DownButton, ("DownButton", Colors.Transparent, Colors.Gray)},
            { LeftButton, ("LeftButton", Colors.Transparent, Colors.Gray)},
            { RightButton, ("RightButton", Colors.Transparent, Colors.Gray)},

            { MenuButton, ("MenuButton", Colors.Transparent, Colors.Gray)},
            { ShareButton, ("ShareButton", Colors.Transparent, Colors.Gray)},

            { L1Button, ("L1Button", Colors.Transparent, Colors.Gray)},
            { L2Button, ("L2Button", Colors.Transparent, Colors.Gray)},
            { R1Button, ("R1Button", Colors.Transparent, Colors.Gray)},
            { R2Button, ("R2Button", Colors.Transparent, Colors.Gray)},
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        

        
        DisplayInfo displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        double screenWidth = displayInfo.Height / displayInfo.Density;
        double screenHeight = displayInfo.Width / displayInfo.Density;
        ContentLayout.SetLayoutBounds(XYABLayout, new Rect(0.9, 0.1, screenHeight * 0.5, screenHeight * 0.5));
        ContentLayout.SetLayoutBounds(DirectionalLayout, new Rect(0.1, 0.1, screenHeight * 0.4, screenHeight * 0.4));

        ContentLayout.SetLayoutBounds(ShareButton, new Rect(0.43, 0.35, screenHeight * 0.1, screenHeight * 0.05));
        ContentLayout.SetLayoutBounds(MenuButton, new Rect(0.57, 0.35, screenHeight * 0.1, screenHeight * 0.05));

        ContentLayout.SetLayoutBounds(LeftButtonLayout, new Rect(0.35, 0, screenHeight * 0.16, screenHeight * 0.24));
        ContentLayout.SetLayoutBounds(RightButtonLayout, new Rect(0.65, 0, screenHeight * 0.16, screenHeight * 0.24));

        ContentLayout.SetLayoutBounds(LeftJoystickLayout, new Rect(0.32, 0.7, screenHeight * 0.3 / screenWidth, screenHeight * 0.3 / screenHeight));
        ContentLayout.SetLayoutBounds(RightJoystickLayout, new Rect(0.68, 0.7, screenHeight * 0.3 / screenWidth, screenHeight * 0.3 / screenHeight));

        ContentLayout.SetLayoutBounds(BackgroundView, new Rect(0, 0, screenWidth, screenHeight));

    }

    
    private async void LeftJoystickPan_PanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        (short X, short Y) = leftJoystick.JoystickPanController(e);
        await _connectionManager.SendAsync($"Analog LX:{X} LY:{Y}");
    }
    private async void RightJoystickPan_PanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        (short X, short Y) = rightJoystick.JoystickPanController(e);
        await _connectionManager.SendAsync($"Analog RX:{X} RY:{Y}");
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            ButtonUtility.ChangeButtonColor(button, buttonPressedProps[button].backgroundColor, buttonPressedProps[button].textColor, true);
            await _connectionManager.SendAsync($"{buttonPressedProps[button].name}_Pressed");
        }
    }
    private async void Button_Released(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            ButtonUtility.ChangeButtonColor(button, buttonReleasedProps[button].backgroundColor, buttonReleasedProps[button].textColor);
            await _connectionManager.SendAsync($"{buttonReleasedProps[button].name}_Released");
        }
    }
}