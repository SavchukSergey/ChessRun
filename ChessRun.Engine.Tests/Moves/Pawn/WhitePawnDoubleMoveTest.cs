using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestClass]
    public class WhitePawnDoubleMoveTest : BaseTestFixture {

        [TestMethod]
        public void ExecuteTest() {
            var move = new WhitePawnDoubleMove(CellName.F2);
            var board = new ChessBoard();
            board[move.From] = PieceType.WhitePawn;
            board[move.MiddleCell] = PieceType.None;
            board[move.To] = PieceType.None;
            board.EnPassantMove = CellName.F6;
            RollbackData rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual(PieceType.None, board[move.From]);
            Assert.AreEqual(PieceType.None, board[move.MiddleCell]);
            Assert.AreEqual(PieceType.WhitePawn, board[move.To]);
            Assert.AreEqual(move.MiddleCell, board.EnPassantMove);
        }

        [TestMethod]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E2, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnDoubleMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "e4");
        }

    }
}
