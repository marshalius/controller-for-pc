using Server.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Controls;

namespace Server.Services
{
    public class ConnectionManager : IConnectionManager
    {
        readonly TcpService _tcpService;
        UdpService _udpService;
        Label _label;
        public ConnectionManager(Label label)
        {
            _tcpService = new TcpService();
            _label = label;
        }
        public async Task<bool> CreateServerAsync()
        {
            _label.Content = "Uygun port aranıyor";
            int port = _tcpService.GetAvailablePort(50000, 50100);
            _label.Content = "Uzak bağlantı bekleniyor";
            _udpService = new(port);
            while (true)
            {
                string message = await _udpService.ListenAsync();
                if (message.StartsWith("controller"))
                {
                    _label.Content = "Uzak bağlantı yakalandı";
                    string sendMessage = $"[Response]{Environment.MachineName}\n{GetIPAddress()}:{port}";
                    await _udpService.SendAsync(sendMessage);
                    break;

                }
            }
            _udpService.Dispose();

            _label.Content = "Sunucu ayarlanıyor";
            return await _tcpService.SetTcpServer(port);

        }

        public void Disconnect()
        {
            _tcpService.Disconnect();
        }

        public bool IsConnected()
        {
            return _tcpService.IsConnected();
        }

        public async Task<string> ReceiveAsync()
        {
            return await _tcpService.ReceiveAsync();
        }

        public async Task SendAsync(string message)
        {
            await _tcpService.SendAsync(message);
        }

        private string GetIPAddress()
        {
            foreach (NetworkInterface netinterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netinterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties properties = netinterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return address.Address.ToString();
                        }
                    }
                }
            }
            throw new Exception("IP adresi tespit edilemedi.");
        }
    }
}
