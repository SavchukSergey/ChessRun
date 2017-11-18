using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(WhitePawnDoubleMove))]
    public class WhitePawnDoubleMoveTest : BaseTestFixture {

        [Test]
        public void ExecuteTest() {
            var move = new WhitePawnDoubleMove(CellName.F2);
            var board = new ChessBoard {
                [move.From] = PieceType.WhitePawn,
                [move.MiddleCell] = PieceType.None,
                [move.To] = PieceType.None,
                EnPassantMove = CellName.F6
            };
            RollbackData rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual(PieceType.None, board[move.From]);
            Assert.AreEqual(PieceType.None, board[move.MiddleCell]);
            Assert.AreEqual(PieceType.WhitePawn, board[move.To]);
            Assert.AreEqual(move.MiddleCell, board.EnPassantMove);
        }

        [Test]
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
