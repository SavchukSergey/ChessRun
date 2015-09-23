using ChessRun.Engine.Moves.King;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    public class BlackLongCastleMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotation() {
            var move = new BlackLongCastleMove();
            var board = new ChessBoard {
                [CellName.E1] = PieceType.WhiteKing,
                [CellName.A1] = PieceType.WhiteRook,
                [CellName.E8] = PieceType.BlackKing,
                [CellName.A8] = PieceType.BlackRook,
                Turn = PieceColor.Black
            };
            Assert.AreEqual("O-O-O", move.ToShortNotation(board));
        }

    }
}
