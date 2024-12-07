using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC
{
    public class TcpConnection
    {
        private readonly TcpClient _tcpClient;
        private NetworkStream _stream;

        public TcpConnection()
        {
            _tcpClient = new()
            {
                NoDelay = true,
            };
        }
        public async Task SendMessageAsync(string message)
        {
            if (_tcpClient == null || !_tcpClient.Connected)
            {
                Console.WriteLine("Bağlantı kesildi, yeniden bağlanılıyor...");
                await ConnectToServerAsync();
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                await _stream.WriteAsync(data.AsMemory(0, data.Length));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mesaj gönderilemedi: {ex.Message}");
            }
        }
        public async Task ConnectToServerAsync()
        {
            try
            {
                (string address, int port) = await DetectServerAsync(49152, 49153);
                while (true)
                {
                    try
                    {
                        await _tcpClient.ConnectAsync(address, port);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"TCP Sunucusuna Bağlanılamadı ({address}{port}): {e}");
                        await Task.Delay(2000);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            _stream = _tcpClient.GetStream();

            try
            {
                while (true)
                {
                    var buffer = new byte[1024];
                    int bytesRead = await _stream.ReadAsync(buffer.AsMemory(0, buffer.Length));

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] commands = message.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string command in commands)
                    {
                        if (message.StartsWith("Vibration"))
                        {
                            string[] commandParts = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                            string[] smallMotorParts = commandParts[1].Split(":");
                            string[] largeMotorParts = commandParts[2].Split(":");

                            int smallMotorValue = Convert.ToInt32(smallMotorParts[1]) / 255 * 500;
                            int largeMotorValue = Convert.ToInt32(largeMotorParts[1]) / 255 * 500;

                            Vibration.Vibrate(smallMotorValue / 2);
                            Vibration.Vibrate(largeMotorValue);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        
        private async Task<(string address, int port)> DetectServerAsync(int firstPort, int endPort)
        {
            Console.WriteLine("Sunucu aranıyor...");
            for (int port = firstPort; port <= endPort; port++)
            {
                try
                {
                    using UdpClient udpClient = new(port)
                    {
                        EnableBroadcast = true,
                    };
                    IPEndPoint endpoint = new(IPAddress.Broadcast, port);
#if ANDROID
                    string sendMessage = $"controller:{Android.OS.Build.Manufacturer}";
#else
                    string sendMessage = $"controller";
#endif
                    byte[] sendMessageBytes = Encoding.UTF8.GetBytes(sendMessage);
                    await udpClient.SendAsync(sendMessageBytes, sendMessageBytes.Length, endpoint);
                    Console.WriteLine($"Message Sent [{port}]: {sendMessage}");

                    IPEndPoint receiveEndpoint = new(IPAddress.Any, port);
                    string receivedData;
                    while (true)
                    {
                        var receivedBytes = await udpClient.ReceiveAsync();
                        receivedData = Encoding.UTF8.GetString(receivedBytes.Buffer);

                        if(receivedData != sendMessage)
                            break;
                    }
                    

                    if (receivedData.StartsWith("[Response]"))
                    {
                        string receivedMessage = receivedData[10..];
                        string[] connectionInfo = receivedMessage.Split(":");
                        int connectionPort = int.Parse(connectionInfo[1]);
                        string[] connectionHost = connectionInfo[0].Split("\n");
                        string connectionName = connectionHost[0];
                        string connectionAddress = connectionHost[1];
                        return (connectionAddress, connectionPort);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in detecting server: {ex.Message}");
                    continue;
                }
            }
            throw new Exception("Sunucu bulunamadı.");
        }
    }
}
