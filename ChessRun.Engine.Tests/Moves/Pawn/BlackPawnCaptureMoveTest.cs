using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(BlackPawnCaptureMove))]
    public class BlackPawnCaptureMoveTest : BaseTestFixture {

        [Test]
        public void ExecuteTest() {
            var move = new BlackPawnCaptureMove(CellName.F7, CellName.G6);
            var board = new ChessBoard {
                [move.From] = PieceType.BlackPawn,
                [move.To] = PieceType.WhiteKnight
            };
            var rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual(PieceType.None, board[move.From]);
            Assert.AreEqual(PieceType.BlackPawn, board[move.To]);
            Assert.AreEqual(PieceType.WhiteKnight, rollback.CapturedPiece);
        }

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.D5, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "de");
        }

        [Test]
        public void ToShortNotationDisambiguatingFileTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/2p1p3/3P4/8/PP1P1PPP/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.E5, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed");

            move = board.GetValidMoves(PieceType.BlackPawn, CellName.C5, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnCaptureMove);
            notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "cd");
        }

        [Test]
        public void ToShortNotationDisambiguatingRankTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/8/3pPp2/3pPp2/3pPp2/8/8/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackPawn, CellName.D6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "de5");

            move = board.GetValidMoves(PieceType.BlackPawn, CellName.F6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackPawnCaptureMove);
            notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "fe5");
        }

    }
}
