using ControllerForPC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Services
{
    public class TcpService : ITcpService
    {
        private TcpClient? _tcpClient;
        private NetworkStream? _stream;
        private bool _isConnected = false;

        public async Task ConnectAsync(string ip, int port)
        {
            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(ip, port);
                _stream = _tcpClient.GetStream();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't connect to the server: {ex.Message}");
            }
            
        }

        public void Disconnect()
        {
            try
            {
                if (IsConnected())
                {
                    _tcpClient.Close();
                    _isConnected = false;
                }
                else
                {
                    throw new Exception("The connection is already closed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't disconnect from the server: {ex.Message}");
            }
        }

        public async Task<string> ReceiveAsync()
        {
            try
            {
                if (IsConnected())
                {
                    byte[] buffer = new byte[1024];
                    int length = await _stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    return message;
                }
                else
                {
                    throw new Exception("Cannot receive message without connection.");
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
            {
                throw new Exception($"A network error occurred while receiving the message: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while receiving the message: {ex.Message}");
            }
            
        }

        public async Task SendAsync(string message)
        {
            try
            {
                if(IsConnected())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    await _stream.WriteAsync(buffer.AsMemory(0, buffer.Length));
                }
                else
                {
                    throw new Exception("Cannot send message without connection.");
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
            {
                throw new Exception($"A network error occurred while sending the message: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending the message: {ex.Message}");
            }
        }

        public bool IsConnected()
        {
            return _isConnected && _tcpClient != null && _stream != null;
        }
    }
}
