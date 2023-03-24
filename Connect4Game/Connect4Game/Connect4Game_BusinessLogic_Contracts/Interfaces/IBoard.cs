using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic_Contracts.Interfaces
{
    public interface IBoard
    {
        int Col { get; }
        int Row { get; }
        int[][] Squares { get; set; }

        void InitBoard(int col, int row);

        void MakeMove(IPlayer p, int col, int playerIndex);
    }
}
