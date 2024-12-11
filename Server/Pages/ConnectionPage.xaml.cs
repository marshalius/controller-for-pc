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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server.Pages
{
    /// <summary>
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Page
    {
        private ConnectionManager _connectionManager;
        public ConnectionPage()
        {
            InitializeComponent();
            _connectionManager = new ConnectionManager(state_label);
            
            CreateServer();
        }

        private async void CreateServer()
        {
            bool isConnected = await _connectionManager.CreateServerAsync();
            state_label.Content = "İstemci sunucuya bağlandı!";
            if (isConnected)
            {
                state_label.Content = "Gamepad ekranı açılıyor...";
                this.NavigationService.Navigate(new DashboardPage(_connectionManager));
            }
        }

        
    }
}
