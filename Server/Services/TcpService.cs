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
    public class TcpService : ITcpService
    {
        private TcpListener _listener;
        private NetworkStream _stream;
        private bool _isConnected;
        
        public async Task<bool> SetTcpServer(int port)
        {
            _listener = new (IPAddress.Any ,port);
            _listener.Start();
            var client = await _listener.AcceptTcpClientAsync();
            _stream = client.GetStream();
            return _stream != null;
        }
        public void Disconnect()
        {
            _listener?.Dispose();
            _stream?.Dispose();
        }

        public int GetAvailablePort(int firstPort, int endPort)
        {
            try
            {
                for (int port = firstPort; port <= endPort; port++)
                {
                    try
                    {
                        using var listener = new TcpListener(IPAddress.Loopback, port);
                        listener.Start();
                        return port;
                    }
                    catch
                    {
                        continue;
                    }
                }
                throw new Exception("Uygun port bulunamadı.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool IsConnected()
        {
            return _isConnected;
        }

        public async Task<string> ReceiveAsync()
        {
            byte[] buffer = new byte[1024];
            int received = await _stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
            string message = Encoding.UTF8.GetString(buffer, 0, received);
            return message;
        }

        public async Task SendAsync(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(buffer);
        }

        
    }
}
