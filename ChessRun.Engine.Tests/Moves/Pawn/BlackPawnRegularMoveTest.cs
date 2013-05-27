using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestClass]
    public class BlackPawnRegularMoveTest : BaseTestFixture {

        [TestMethod]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.E7, CellName.E6).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnRegularMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "e6");
        }
    }
}
