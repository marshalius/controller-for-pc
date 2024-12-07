using ControllerForPC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Services
{
    public class UdpService(int port) : IUdpService
    {
        private readonly UdpClient _udpClient = new ();
        private readonly IPEndPoint _endpoint = new (IPAddress.Broadcast, port);

        public async Task BroadcastAsync(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await _udpClient.SendAsync(buffer, buffer.Length, _endpoint);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending UDP message: {ex}");
            }
                
        }

        public void Dispose()
        {
            _udpClient.Dispose();
        }

        public async Task<(string address, int port, string message)> ReceiveAsync(CancellationToken cancellation)
        {
            
            UdpReceiveResult received = await _udpClient.ReceiveAsync(cancellation);
            byte[] buffer = received.Buffer;
            string message = Encoding.UTF8.GetString(buffer);
            string address = received.RemoteEndPoint.Address.ToString();
            int port = received.RemoteEndPoint.Port;
            return (address, port, message);
        }
    }
}
