using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(WhitePawnRegularMove))]
    public class WhitePawnRegularMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E2, CellName.E3).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnRegularMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "e3");
        }

    }
}
