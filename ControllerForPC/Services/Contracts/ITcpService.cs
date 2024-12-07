using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Services.Contracts
{
    public interface ITcpService
    {
        bool IsConnected();
        Task ConnectAsync(string ip, int port);
        Task SendAsync(string message);
        Task<string> ReceiveAsync();
        void Disconnect();
    }
}
