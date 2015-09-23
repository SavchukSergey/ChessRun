using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    public class WhitePawnCaptureMoveTest : BaseTestFixture {

        [Test]
        public void ExecuteTest() {
            var move = new WhitePawnCaptureMove(CellName.F2, CellName.G3);
            var board = new ChessBoard {
                [move.From] = PieceType.WhitePawn,
                [move.To] = PieceType.BlackKnight
            };
            var rollback = new RollbackData();
            move.Execute(board, ref rollback);
            Assert.AreEqual(PieceType.None, board[move.From]);
            Assert.AreEqual(PieceType.WhitePawn, board[move.To]);
            Assert.AreEqual(PieceType.BlackKnight, rollback.CapturedPiece);
        }

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed");
        }

        [Test]
        public void ToShortNotationDisambiguatingFileTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/3p4/2P1P3/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed");

            move = board.GetValidMoves(PieceType.WhitePawn, CellName.C4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnCaptureMove);
            notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "cd");
        }

        [Test]
        public void ToShortNotationDisambiguatingRankTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/8/3pPp2/3pPp2/3pPp2/8/8/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed5");

            move = board.GetValidMoves(PieceType.WhitePawn, CellName.E5, CellName.D6).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnCaptureMove);
            notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed6");
        }

    }
}
