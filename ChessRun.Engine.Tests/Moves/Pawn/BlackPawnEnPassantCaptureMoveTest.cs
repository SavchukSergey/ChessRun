using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(BlackPawnEnPassantCaptureMove))]
    public class BlackPawnEnPassantCaptureMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/ppp1pppp/8/8/3pP3/8/PPPP1PPP/RNBQKBNR b KQkq e3");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.D4, CellName.E3).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnEnPassantCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "de");
        }

    }
}
