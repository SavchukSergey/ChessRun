using ChessRun.Engine.Moves.King;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.King {
    [TestClass]
    public class BlackLongCastleMoveTest : BaseTestFixture {

        [TestMethod]
        public void ToShortNotation() {
            var move = new BlackLongCastleMove();
            var board = new ChessBoard();
            board[CellName.E1] = PieceType.WhiteKing;
            board[CellName.A1] = PieceType.WhiteRook;
            board[CellName.E8] = PieceType.BlackKing;
            board[CellName.A8] = PieceType.BlackRook;
            board.Turn = PieceColor.Black;
            Assert.AreEqual("O-O-O", move.ToShortNotation(board));
        }

    }
}
