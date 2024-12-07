using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Services.Contracts
{
    public interface IConnectionManager
    {
        Task<(string address, int port, string name)> ScanServerAsync(int minPort, int maxPort);
        Task ConnectAsync(string host, int port);
        Task SendAsync(string message);
        Task<string> ReceiveAsync();
        void Disconnect();
        bool IsConnected();
    }
}
