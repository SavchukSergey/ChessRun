using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    public class BlackPawnPromotionMoveTest : BasePawnMoveTest {

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPp/RNBQKBN1 b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.H2, CellName.H1, PieceType.BlackQueen).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnPromotionMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "h1=Q");
        }

    }
}
