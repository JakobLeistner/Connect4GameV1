using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4Game_RestAPI
{
    public class ConnectionService
    {
        public Dictionary<string, string> PlayerIDToConnectionID;

        //Wenn eine neue Person die Seite lädt
        public ConnectionService()
        {
            PlayerIDToConnectionID = new Dictionary<string, string>();
        }
    }
}
