using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Contracts
{
    public interface IUdpService
    {
        Task<string> ListenAsync();
        Task SendAsync(string message);
        void Dispose();
    }
}
