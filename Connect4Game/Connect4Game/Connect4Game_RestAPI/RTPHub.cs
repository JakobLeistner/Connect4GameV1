using Connect4Game_BusinessLogic;
using Connect4Game_BusinessLogic_Contracts.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Connect4Game_RestAPI
{
    public class RTPHub : Hub
    {
        private readonly ConnectionService connection;
        private readonly LogicController logic;

        public RTPHub()
        {
            connection = new ConnectionService();
            logic = new LogicController();
        }
        //Hub wartet und hört auf events

        //gameID kommt aus Logik, denn dort wurde das event invoked 
        [HubMethodName("OnGameStarted")]
        public async Task OnGameStarted(string gameID)
        {
            await SendMessage(gameID, "GameStarted", null);
        }

        [HubMethodName("OnGameEnded")]
        public async Task OnGameEnded(string gameID)
        {
            await SendMessage(gameID, "GameEnded", null);
        }

        [HubMethodName("OnMoveMade")]
        public async Task OnMoveMade(string gameID)
        {
            await SendMessage(gameID, "MoveMade", null);
        }

        [HubMethodName("OnQueueChanged")]
        public async Task OnQueueChanged()
        {
            await BroadcastMessage("QueueChanged");
        }

        //sends a message to both players from a game(gameID)
        private async Task SendMessage(string gameID, string message, object data)
        {
            string Player1ConnectionID = connection.PlayerIDToConnectionID[logic.GetGameFromID(gameID).Player1.PlayerID];
            await Clients.Client(Player1ConnectionID).SendAsync(message, data);

            string Player2ConnectionID = connection.PlayerIDToConnectionID[logic.GetGameFromID(gameID).Player2.PlayerID];
            await Clients.Client(Player2ConnectionID).SendAsync(message, data);
        }

        private async Task BroadcastMessage(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
