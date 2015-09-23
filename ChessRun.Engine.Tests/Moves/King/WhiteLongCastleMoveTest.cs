using ChessRun.Engine.Moves.King;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    public class WhiteLongCastleMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotation() {
            var move = new WhiteLongCastleMove();
            var board = new ChessBoard {
                [CellName.E1] = PieceType.WhiteKing,
                [CellName.A1] = PieceType.WhiteRook,
                [CellName.E8] = PieceType.BlackKing,
                [CellName.A8] = PieceType.BlackRook,
                Turn = PieceColor.White
            };
            Assert.AreEqual("O-O-O", move.ToShortNotation(board));
        }

    }
}
