using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Contracts
{
    public interface ITcpService
    {
        int GetAvailablePort(int firstPort, int endPort);
        bool IsConnected();
        Task<bool> SetTcpServer(int port);
        Task SendAsync(string message);
        Task<string> ReceiveAsync();
        void Disconnect();
    }
}
