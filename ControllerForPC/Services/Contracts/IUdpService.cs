using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Services.Contracts
{
    public interface IUdpService
    {
        Task BroadcastAsync(string message);
        Task<(string address, int port, string message)> ReceiveAsync(CancellationToken cancellation);
        
    }
}
