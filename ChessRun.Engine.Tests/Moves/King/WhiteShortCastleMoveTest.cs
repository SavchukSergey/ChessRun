using ChessRun.Engine.Moves.King;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    [TestOf(typeof(WhiteShortCastleMove))]
    public class WhiteShortCastleMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotation() {
            var move = new WhiteShortCastleMove();
            var board = new ChessBoard {
                [CellName.E1] = PieceType.WhiteKing,
                [CellName.H1] = PieceType.WhiteRook,
                [CellName.E8] = PieceType.BlackKing,
                [CellName.H8] = PieceType.BlackRook,
                Turn = PieceColor.White
            };
            Assert.AreEqual("O-O", move.ToShortNotation(board));
        }

    }
}
