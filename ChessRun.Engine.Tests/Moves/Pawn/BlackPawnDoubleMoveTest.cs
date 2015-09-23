using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    public class BlackPawnDoubleMoveTest : BaseTestFixture {

        [Test]
        public void ExecuteTest() {
            var move = new BlackPawnDoubleMove(CellName.F7);
            var board = new ChessBoard {
                [move.From] = PieceType.BlackPawn,
                [move.MiddleCell] = PieceType.None,
                [move.To] = PieceType.None,
                EnPassantMove = CellName.F3
            };
            var rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual(PieceType.None, board[move.From]);
            Assert.AreEqual(PieceType.None, board[move.MiddleCell]);
            Assert.AreEqual(PieceType.BlackPawn, board[move.To]);
            Assert.AreEqual(move.MiddleCell, board.EnPassantMove);
        }

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.E7, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnDoubleMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "e5");
        }

    }
}
