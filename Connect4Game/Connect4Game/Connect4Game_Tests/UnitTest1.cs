using Connect4Game_BusinessLogic;
using Connect4Game_BusinessLogic_Contracts.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Connect4Game_Tests
{
    [TestClass]
    public class UnitTest1
    {
        Player Player;
        Game Game;

        [TestInitialize]
        public void Init()
        {
            Player = new Player("12345", "Alexander Marcus", "Rosa");
            Game = new Game("egal123", (IPlayer) Player); // gameID, playerID
        }

        // GetName(): string
        [TestMethod]
        public void GetName_WhenNamesAreSame_NamesMatch()
        {
            string name = "";
            Assert.AreSame(name, Game.CurrentPlayer.GetName());
        }


        // MakeMove(Player p, int col): bool
        [TestMethod]
        [DataRow(10)]
        [DataRow(-3)]
        public void MakeMove_WhenColumnIsOutOfRange_ShouldThrowArgumentOutOfRange(int col)
        {
            Player player = (Player)Game.Player1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Game.MakeMove(player, col));
        }

        [TestMethod]
        public void MakeMove_WhenAColumnIsFull_ShouldThrowInvalidOperations()
        {
            Player player = (Player)Game.Player1;
            int col = 1; // depends on Array Column's Number

            Game.Board.Squares = new int[][]
            {
              new int[]{0,2,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,2,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,2,0,0,0,0,0},
            };

            Assert.ThrowsException<InvalidOperationException>(() => Game.MakeMove(player, col));
        }

        [TestMethod]
        [DataRow(0)] // depends on Array Column' Number
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void MakeMove_WhenTokensDroppedInColumns(int col)
        {
            Board newBoard = new Board(7, 6);
            Player player = (Player)Game.Player2;

            Game.Board.Squares = new int[][]
            {
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
            };

            newBoard.Squares = new int[][]
            {
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
            };

            newBoard.Squares[5][col] = 2;

            Game.MakeMove(player, col);
            Assert.AreEqual(newBoard.Squares, Game.Board.Squares);
        }

        [TestMethod]
        public void MakeMove_WhenItIsNotCurrentPlayerWhoMakesTheMove_ShouldReturnFalse() // Exceptions vom Lukas
        {
            Game.CurrentPlayer = Game.Player1;

            Assert.ThrowsException<ArgumentException>(() => Game.MakeMove(Game.Player2, 6));
        }

        [TestMethod]
        public void MakeMove_WhenTheFirstColumnIsFull_ShouldThrowInvalidOperations()
        {
            Game.Board.Squares = new int[][]
            {
              new int[]{2,0,0,0,0,0,0},
              new int[]{2,0,0,0,0,0,0},
              new int[]{1,0,0,0,0,0,0},
              new int[]{2,0,0,0,0,0,0},
              new int[]{1,0,0,0,0,0,0},
              new int[]{1,0,0,0,0,0,0},
            };

            Assert.ThrowsException<InvalidOperationException>(() => Game.MakeMove(Game.Player2, 0)); // depends on Array Column's Number
        }

        [TestMethod]
        public void MakeMove_WhenTheLastColumnIsFull_ShouldThrowInvalidOperations()
        {
            Game.Board.Squares = new int[][]
            {
              new int[]{0,0,0,0,0,0,2},
              new int[]{0,0,0,0,0,0,1},
              new int[]{0,0,0,0,0,0,1},
              new int[]{0,0,0,0,0,0,2},
              new int[]{0,0,0,0,0,0,2},
              new int[]{0,0,0,0,0,0,1},
            };

            Assert.ThrowsException<InvalidOperationException>(() => Game.MakeMove(Game.Player1, 6)); // depends on Array Column's Number
        }


        // TogglePlayer(): void
        [TestMethod]
        public void TogglePlayer_ChangePlayer1ToPlayer2()
        {
            Game.Player1 = Game.CurrentPlayer;
            Game.TogglePlayer();
            Assert.IsTrue(Game.CurrentPlayer == Game.Player2);
        }

        [TestMethod]
        public void TogglePlayer_ChangePlayer2ToPlayer1()
        {
            Game.Player2 = Game.CurrentPlayer;
            Game.TogglePlayer();
            Assert.IsTrue(Game.CurrentPlayer == Game.Player1);
        }


        // IsOver(): bool
        [TestMethod]
        public void IsOver_WhenPlayer2Wins_ShouldReturnTrue()
        {
            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,2,0,0,0},
              new int[]{1,0,0,2,0,2,0},
              new int[]{2,0,0,1,0,2,0},
              new int[]{2,0,1,1,2,2,0},
              new int[]{1,0,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.IsTrue(Game.IsOver());
        }

        [TestMethod]
        public void IsOver_WhenPlayer1Wins_ShouldReturnTrue()
        {
            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,2,0,0,0},
              new int[]{1,0,0,2,0,0,0},
              new int[]{2,0,0,1,0,1,0},
              new int[]{1,1,1,1,2,2,0},
              new int[]{1,0,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.IsTrue(Game.IsOver());
        }

        [TestMethod]
        public void IsOver_WhenGameIsDraw_ShouldReturnTrue()
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,2},
              new int[]{1,1,1,2,2,1,1},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,1},
              new int[]{1,1,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };
            Assert.IsTrue(Game.IsOver());
        }



        // IsDraw(): bool
        [TestMethod]
        public void IsDraw_WhenGameBoardIsFullAndNoOneWins_ShouldReturnTrue() // isFalse -> andere Method
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,2},
              new int[]{1,1,1,2,2,1,1},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,1},
              new int[]{1,1,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.IsTrue(Game.IsDraw());
        }
        /// <summary>
        /// Wenn es voll ist aber jemand hat gewonnen -> der letzte Stein -> return False
        /// Board mit leeren Felder -> return False -> noch nicht over
        /// </summary>
        [TestMethod]
        public void IsDraw_WhenTheLastTokenDecidesAWinner_ShouldReturnFalse()
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,0},
              new int[]{1,1,1,2,2,1,2},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,2},
              new int[]{1,1,2,2,1,2,1},
              new int[]{1,2,1,2,2,1,1},
            };
            Game.CurrentPlayer = Game.Player2;
            Game.MakeMove(Game.CurrentPlayer, 6);
            Assert.IsFalse(Game.IsDraw());
        }

        [TestMethod]
        public void IsDraw_WhenTheGameIsNotOver_ShouldReturnFalse()
        {
            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,2,0,0,0},
              new int[]{1,0,0,2,0,0,0},
              new int[]{2,0,0,1,0,1,0},
              new int[]{2,0,1,1,2,2,0},
              new int[]{1,0,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.IsFalse(Game.IsDraw());
        }


        // GetWinner(): ?Player
        [TestMethod]
        public void GetWinner_WhenPlayer1Wins_ShouldReturnPlayer1()
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,2},
              new int[]{1,1,1,1,2,1,1},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,1},
              new int[]{1,1,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.AreSame(Game.Player1, Game.GetWinner());
        }

        [TestMethod]
        public void GetWinner_WhenPlayer2Wins_ShouldReturnPlayer2()
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,2},
              new int[]{1,1,1,2,2,1,1},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,1},
              new int[]{1,1,2,2,1,2,2},
              new int[]{1,2,2,2,2,1,1},
            };

            Assert.AreSame(Game.Player2, Game.GetWinner());
        }

        [TestMethod]
        public void GetWinner_WhenThereIsNoWinner_ShouldReturnNull()
        {
            Game.Board.Squares = new int[][] {
              new int[]{1,2,2,2,1,2,2},
              new int[]{1,1,1,2,2,1,1},
              new int[]{2,2,2,1,1,1,2},
              new int[]{2,1,1,1,2,2,1},
              new int[]{1,1,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.AreSame(null, Game.GetWinner());
        }
        /// <summary>
        /// wenn es noch 0er gibt -> noch nicht fertig und noch keiner gewonnen hat -> null zurückgeben
        /// </summary>
        [TestMethod]
        public void GetWinner_WhenGameIsNotOverAndNoOneWins_ShouldReturnNull()
        {
            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,2,0,0,0},
              new int[]{1,0,0,2,0,0,0},
              new int[]{2,0,0,1,0,1,0},
              new int[]{2,0,1,1,2,2,0},
              new int[]{1,0,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            Assert.AreSame(null, Game.GetWinner());
        }


        // ClearBoard(int col, int row): void
        [TestMethod]
        public void InitBoard_WhenBoardIsNotEmpty() // neues Board alles auf 0er und prüfen mit AreSame die beiden Boards
        {
            int col = 7;
            int row = 6;
            Board emptyBoard = new Board(col, row);

            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,2,0,0,0},
              new int[]{1,0,0,2,0,0,0},
              new int[]{2,0,0,1,0,1,0},
              new int[]{2,0,1,1,2,2,0},
              new int[]{1,0,2,2,1,2,2},
              new int[]{1,2,1,2,2,1,1},
            };

            emptyBoard.Squares = new int[][]
            {
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
            };

            Game.Board.InitBoard(col, row);
            Assert.AreEqual(emptyBoard.Squares, Game.Board.Squares);
        }

        [TestMethod]
        public void InitBoard_WhenBoardIsEmpty()
        {
            int col = 7;
            int row = 6;
            Board emptyBoard = new Board(col, row);

            Game.Board.Squares = new int[][] {
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
            };

            emptyBoard.Squares = new int[][]
            {
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
              new int[]{0,0,0,0,0,0,0},
            };

            Game.Board.InitBoard(col, row);
            Assert.AreEqual(emptyBoard.Squares, Game.Board.Squares);
        }
    }
}
