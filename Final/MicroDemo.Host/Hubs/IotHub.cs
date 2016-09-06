using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroDemo.Host.Hubs
{
    public class IotHub : Hub
    {
        public void BroadcastSensorStats(int stat)
        {
            Clients.All.broadcastStat(stat);
        }
    }
}
