using ControllerForPC.Pages.Views;
using ControllerForPC.Services;
using ControllerForPC.Services.Contracts;
using Animation = Microsoft.Maui.Controls.Animation;

namespace ControllerForPC.Pages;

public partial class ConnectionPage : ContentPage
{
	private readonly ConnectionManager _connectionManager;
	public ConnectionPage()
	{
		InitializeComponent();

		_connectionManager = new ConnectionManager();
        StartBackgroundColorAnimation();
        ScanServers();
	}
	private async void ScanServers()
	{
		(string address, int port, string name) = await _connectionManager.ScanServerAsync(50000, 50010);
		ServerListLayout.Children.Add(new ServerView(address, port, name, _connectionManager));
    }

    private void StartBackgroundColorAnimation()
    {
        
        Color startColor = Colors.Red;
        Color endColor = Colors.Blue;

        float startR = startColor.Red;
        float startG = startColor.Green;
        float startB = startColor.Blue;

        float endR = endColor.Red;
        float endG = endColor.Green;
        float endB = endColor.Blue;
        
        var colorAnimation = new Animation(v =>
        {
            float r, g, b;
            if(v < 0.25)
            {
                Color startColor = Colors.Red;
                Color endColor = Colors.Maroon;

                float startR = startColor.Red;
                float startG = startColor.Green;
                float startB = startColor.Blue;

                float endR = endColor.Red;
                float endG = endColor.Green;
                float endB = endColor.Blue;

                r = startR + (endR - startR) * (float)v * 4;
                g = startG + (endG - startG) * (float)v * 4;
                b = startB + (endB - startB) * (float)v * 4;
            }
            else if (v < 0.50)
            {
                Color startColor = Colors.Maroon;
                Color endColor = Colors.Blue;

                float startR = startColor.Red;
                float startG = startColor.Green;
                float startB = startColor.Blue;

                float endR = endColor.Red;
                float endG = endColor.Green;
                float endB = endColor.Blue;

                r = startR + (endR - startR) * ((float)v - 0.25f) * 4;
                g = startG + (endG - startG) * ((float)v - 0.25f) * 4;
                b = startB + (endB - startB) * ((float)v - 0.25f) * 4;
            }
            else if (v < 0.75)
            {
                Color startColor = Colors.Blue;
                Color endColor = Colors.Purple;

                float startR = startColor.Red;
                float startG = startColor.Green;
                float startB = startColor.Blue;

                float endR = endColor.Red;
                float endG = endColor.Green;
                float endB = endColor.Blue;

                r = startR + (endR - startR) * ((float)v - 0.50f) * 4;
                g = startG + (endG - startG) * ((float)v - 0.50f) * 4;
                b = startB + (endB - startB) * ((float)v - 0.50f) * 4;
            }
            else
            {
                Color startColor = Colors.Purple;
                Color endColor = Colors.Red;

                float startR = startColor.Red;
                float startG = startColor.Green;
                float startB = startColor.Blue;

                float endR = endColor.Red;
                float endG = endColor.Green;
                float endB = endColor.Blue;

                r = startR + (endR - startR) * ((float)v - 0.75f) * 4;
                g = startG + (endG - startG) * ((float)v - 0.75f) * 4;
                b = startB + (endB - startB) * ((float)v - 0.75f) * 4;
            }


            this.BackgroundColor = new Color(r, g, b);
        }, 0, 1);

        colorAnimation.Repeats = true;

        colorAnimation.Commit(this, "ColorChangeAnimation", length: 10000, repeat: () => true);
    }
}