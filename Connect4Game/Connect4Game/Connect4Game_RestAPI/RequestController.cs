
using Connect4Game_BusinessLogic;
using Connect4Game_BusinessLogic_Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Connect4Game_RestAPI
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController
    {
        private ConnectionService connection;
        private LogicController logic;
        public RequestController()
        {

        }

        [HttpPost("RegisterPlayer")]
        public Player RegisterPlayer([FromQuery] string name, [FromQuery] string color)
        {
            //neue playerID generieren
            Guid uuid = Guid.NewGuid();
            string newPlayerID = uuid.ToString();

            Player p = new Player(newPlayerID, name, color);
            logic.PlayerList.Add(p);
            return p;
        }

        [HttpPost("CreateGame")]
        public Game CreateGame([FromQuery] string playerID)
        {
            //neue gameID generieren
            Guid uuid = Guid.NewGuid();
            string newGameID = uuid.ToString();

            Game g = new Game(newGameID, logic.GetPlayerFromID(playerID));
            logic.GameList.Add(g);
            return g;
        }

        [HttpPost("JoinGame")]
        public void JoinGame([FromQuery] string playerID, [FromQuery] string gameID)
        {
            logic.StartGame(playerID, gameID);
        }

        [HttpPost("LeaveGame")]
        public void LeaveGame([FromQuery] string playerID)
        {
            //Player leaves prematurely, this is not the natural way that a game ends.
            //The natural way, meaning somebody won or the game resulted in a draw, would trigger the "OnGameEnded" Event
            //remove the game which the player was playing
            foreach (IGame g in logic.GameList)
            {
                if ((g.Player1.PlayerID == playerID) || (g.Player2.PlayerID == playerID))
                {
                    g.OnGameEnded.Invoke(this, new EventArgs());
                    logic.GameList.Remove(g);
                }
            }
        }

        [HttpPost("MakeMove")]
        public void MakeMove([FromQuery] string playerID, int col, [FromQuery] string gameID)
        {
            logic.MakeMoveInGame(gameID, playerID, col);
        }

        [HttpGet("GetStatus")]
        //FrontEnd request and gets Status Update
        public IGame GetStatus([FromQuery] string gameID)
        {
            return logic.GetGameFromID(gameID);  //Interface oder nicht - das ist hier die Frage
        }

        [HttpGet("GetQueue")]
        public List<IPlayer> GetQueue()
        {
            return logic.Queue;
        }

        //post - methode ändert etwas an der Logik
        //get - holt sich infos, ändert nichts 

    }
}
