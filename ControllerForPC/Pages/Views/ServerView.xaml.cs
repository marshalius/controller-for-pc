using ControllerForPC.Services;
using ControllerForPC.Services.Contracts;

namespace ControllerForPC.Pages.Views;

public partial class ServerView : ContentView
{
	private readonly string _address;
	private readonly int _port;
	private readonly string _name;
	private readonly ConnectionManager _connectionManager;
	public ServerView(string address, int port, string name, ConnectionManager connectionManager)
	{
		_connectionManager = connectionManager;
		_address = address;
		_port = port;
		_name = name;

		InitializeComponent();

		NameLabel.Text = _name;
		AddressLabel.Text = $"{_address}:{_port}";
	}

    private async void ConnectButton_Clicked(object sender, EventArgs e)
    {
		ConnectButton.IsEnabled = false;
		try
		{
			await _connectionManager.ConnectAsync(_address, _port);
			await Navigation.PushAsync(new GamepadPage(_connectionManager));
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Baðlanýrken bir hata meydana geldi: {ex}");
		}
		finally
		{ 
			ConnectButton.IsEnabled = true;
		}
    }
}