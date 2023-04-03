using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4Game_RestAPI
{
    public class EventHandler
    {
        private ConnectionService connectionService;
        public EventHandler(ConnectionService connectionService)
        {
            this.connectionService = connectionService;
        }
        public void EventHandlerInitalize(string eventHandlerHub)
        {

        }
    }
}
