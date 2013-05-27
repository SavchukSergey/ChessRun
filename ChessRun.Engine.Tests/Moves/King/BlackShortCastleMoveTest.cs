using ChessRun.Engine.Moves.King;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.King {
    [TestClass]
    public class BlackShortCastleMoveTest : BaseTestFixture {

        [TestMethod]
        public void ToShortNotation() {
            var move = new BlackShortCastleMove();
            var board = new ChessBoard();
            board[CellName.E1] = PieceType.WhiteKing;
            board[CellName.H1] = PieceType.WhiteRook;
            board[CellName.E8] = PieceType.BlackKing;
            board[CellName.H8] = PieceType.BlackRook;
            board.Turn = PieceColor.Black;
            Assert.AreEqual("O-O", move.ToShortNotation(board));
        }

    }
}
