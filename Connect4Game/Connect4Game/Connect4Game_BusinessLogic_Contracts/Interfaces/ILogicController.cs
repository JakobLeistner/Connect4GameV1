using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic_Contracts.Interfaces
{
    public interface ILogicController
    {
        List<IGame> GameList { get; }
        List<IPlayer> PlayerList { get; }
        List<IPlayer> Queue { get; }

        void JoinQueue(IPlayer playerI);

        IGame StartGame(string playerID1, string playerID2);

        public void MakeMoveInGame(string gameID, string playerID, int col);

        void EndGame(string gameID);

        IGame GetGameFromID(string gameID);
    }
}
