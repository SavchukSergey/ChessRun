using ChessRun.Engine.Moves.King;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    public class BlackShortCastleMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotation() {
            var move = new BlackShortCastleMove();
            var board = new ChessBoard {
                [CellName.E1] = PieceType.WhiteKing,
                [CellName.H1] = PieceType.WhiteRook,
                [CellName.E8] = PieceType.BlackKing,
                [CellName.H8] = PieceType.BlackRook,
                Turn = PieceColor.Black
            };
            Assert.AreEqual("O-O", move.ToShortNotation(board));
        }

    }
}
