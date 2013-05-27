using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestClass]
    public class BlackPawnPromotionCaptureMoveTest : BasePawnMoveTest {

        [TestMethod]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPp/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.H2, CellName.G1, PieceType.BlackQueen).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnPromotionCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "hg=Q");
        }

    }
}
