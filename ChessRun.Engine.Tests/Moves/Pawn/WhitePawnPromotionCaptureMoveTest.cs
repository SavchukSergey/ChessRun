using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(WhitePawnPromotionCaptureMove))]
    public class WhitePawnPromotionCaptureMoveTest : BasePawnMoveTest {

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppP/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.H7, CellName.G8, PieceType.WhiteQueen).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnPromotionCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "hg=Q");
        }

    }
}
