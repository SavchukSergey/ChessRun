using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestClass]
    public class WhitePawnPromotionCaptureMoveTest : BasePawnMoveTest {

        [TestMethod]
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
