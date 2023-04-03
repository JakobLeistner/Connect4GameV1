using Connect4Game_BusinessLogic_Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic
{
    public class Game : IGame
    {
        public string GameID { get; set; }
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public IBoard Board { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public EventHandler OnGameStarted { get; set; }
        public EventHandler OnMoveMade { get; set; }
        public EventHandler OnGameEnded { get; set; }

        public Game(string gameID, IPlayer player)
        {
            this.GameID = gameID;
            this.Player1 = player;
        }

        public bool MakeMove(IPlayer p, int col)
        {
            int playerIndex = GetPlayerIndex();
            Board.MakeMove(p, col, playerIndex);
            TogglePlayer();
            IsOver();
            GetWinner();
            return true;
        }
        private int GetPlayerIndex()
        {

            if (Player1 == CurrentPlayer)
            {
                return 1;
            }
            else if (Player2 == CurrentPlayer)
            {
                return 2;
            }
            throw new Exception();
        }

        public void TogglePlayer()
        {
            if (Player1 == CurrentPlayer)
            {
                CurrentPlayer = Player2;
            }
            else if (Player2 == CurrentPlayer)
            {
                CurrentPlayer = Player1;
            }
        }

        public bool IsOver()
        {
            if (GetWinner() != null)
            {
                return true;
            }
            return false;
        }

        public bool IsDraw()
        {
            int[][] board;
            board = this.Board.Squares;
            for (int col = 0; col < 7; col++)
            {
                if (board[0][col] == 0 && GetWinner() == null)
                {
                    return false;
                }
            }
            return true;
        }

        private IPlayer GetPlayerFromPlayerIndex(int winner)
        {
            return (winner == 1) ? Player1 : Player2;
        }

        public IPlayer GetWinner()
        {
            int[][] board;
            board = this.Board.Squares;
            bool win = false;
            // Überprüfen horizontal
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c <= 3; c++)
                {
                    int player = board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (board[r][c + i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen vertikal
            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (board[r + i][c] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen diagonal "\"
            for (int c = 0; c <= 3; c++)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (board[r + i][c + i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen diagonal "/"
            for (int c = 7 - 1; c >= 4 - 1; c--)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (board[r + i][c - i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            return null;
        }
    }
}
