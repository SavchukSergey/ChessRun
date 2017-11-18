using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.King;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    [TestOf(typeof(WhiteLongCastleMove))]
    public class WhiteLongCastleMoveTest : BaseTestFixture {

        [Test]
        public void ExecuteTest() {
            var move = new WhiteLongCastleMove();
            var board = FEN.Setup(new ChessBoard(), "r3k3/8/8/8/8/8/8/R3K3 w KQkq -");
            var rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual("r3k3/8/8/8/8/8/8/2KR4 w KQkq -", FEN.GetFEN(board));
            move.Unexecute(board, ref rollback);
            Assert.AreEqual("r3k3/8/8/8/8/8/8/R3K3 w KQkq -", FEN.GetFEN(board));
        }

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
