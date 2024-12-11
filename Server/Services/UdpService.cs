using Server.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UdpService : IUdpService
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endpoint;
        public UdpService(int port)
        {
            _endpoint = new IPEndPoint(IPAddress.Broadcast, port);
            _udpClient = new UdpClient(port) { EnableBroadcast = true};
        }
        public async Task<string> ListenAsync()
        {
            var received = await _udpClient.ReceiveAsync();
            string message = Encoding.UTF8.GetString(received.Buffer);
            return message;
        }

        public async Task SendAsync(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _udpClient.SendAsync(buffer, buffer.Length, _endpoint);
        }

        public void Dispose()
        {
            _udpClient?.Dispose();
        }
    }
}
