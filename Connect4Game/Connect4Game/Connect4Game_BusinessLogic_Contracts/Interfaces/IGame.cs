using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic_Contracts.Interfaces
{
    public interface IGame
    {
        string GameID { get; }
        IPlayer Player1 { get; }
        IPlayer Player2 { get; set; }
        IBoard Board { get; }
        IPlayer CurrentPlayer { get; }
        EventHandler OnGameStarted { get; set; }
        EventHandler OnMoveMade { get; set; }
        EventHandler OnGameEnded { get; set; }

        bool MakeMove(IPlayer p, int col);

        void TogglePlayer();

        bool IsOver();

        bool IsDraw();

        IPlayer GetWinner();
    }
}
