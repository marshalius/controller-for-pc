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
using System.Threading;
using System.Windows.Navigation;
using System.Windows.Media;

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
            int port = _tcpService.GetAvailablePort(50000, 50010);
            

            string sendMessage = $"[ControllerForPCServer]\n{Environment.MachineName}\n{GetIPAddress()}:{port}";
            CancellationTokenSource udpTokenSource = new ();
            _ = BroadcastPortAsync(port, sendMessage, udpTokenSource.Token);

            _label.Content = "Sunucu ayarlanıyor";
            return await _tcpService.SetTcpServer(port, udpTokenSource);

        }

        private async Task BroadcastPortAsync(int port, string message, CancellationToken token)
        {
            _udpService = new(port);
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    await _udpService.SendAsync(message);
                    await Task.Delay(100, token);
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("UDP yayını sonlandırıldı.");
            }
            finally
            {
                _udpService.Dispose();
            }
            
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
            string message = await _tcpService.ReceiveAsync();
            return message;
            
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
