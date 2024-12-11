using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Contracts
{
    public interface IConnectionManager
    {
        Task<bool> CreateServerAsync();
        Task SendAsync(string message);
        Task<string> ReceiveAsync();
        void Disconnect();
        bool IsConnected();
    }
}
