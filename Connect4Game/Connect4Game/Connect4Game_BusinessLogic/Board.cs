using Connect4Game_BusinessLogic_Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic
{
    public class Board : IBoard
    {
        public int Col { get; set; }
        public int Row { get; set; }
        public int[][] Squares { get; set; }

        public Board(int col, int row)
        {
            this.Col = col = 7;
            this.Row = row = 6;
            this.InitBoard(col, row);
        }

        public void InitBoard(int col, int row)
        {
            for (int i = 0; i < col; i++)
            {
                Squares[i] = new int[col];
                for (int j = 0; j < row; j++)
                {
                    Squares[i][j] = 0;
                }
            }
        }

        public void MakeMove(IPlayer p, int col, int playerIndex)
        {
            string playerID = p.PlayerID;
            int currentPlayer = playerIndex;

            /*if (Player1.PlayerID != playerID && Player2.PlayerID != playerID)
            {
                throw new PlayerNotInGameException();
            }
            
            if (Player1.PlayerID == playerID && currentPlayer != 1 || Player2:ID == playerID && currentPlayer != 2){
                throw new NotYourTurnException();
            }
            
            if (col < 1 || col < 7)
            {
                throw new BoardOutOfRangeException();
            }*/

            int row = -1;
            for (int r = Row - 1; r >= 0; r--)
            {
                if (Squares[col][r] == 0)
                {
                    row = r;
                    break;
                }
            }

            if (row == -1)
            {
                return;
            }

            Squares[col][row] = currentPlayer;

        }
    }
}
