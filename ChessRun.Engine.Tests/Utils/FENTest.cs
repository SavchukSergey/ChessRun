using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Utils {
    public class FENTest : BaseTestFixture {

        [Test]
        public void WriteInitialPositionTest() {
            var board = new ChessBoard();
            SetInitialBoard(board);
            var fen = FEN.GetFEN(board);
            Assert.IsTrue(FEN.INITIAL_POSITION.StartsWith(fen));
        }

        [Test]
        public void ReadInitialPositionTest() {
            var board = new ChessBoard();
            FEN.Setup(board, FEN.INITIAL_POSITION);
            AssertInitialBoard(board);
        }

        [Test]
        public void MinimalStringTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq");
            Assert.AreEqual(PieceColor.Black, board.Turn);
            Assert.IsTrue(board.WhiteCanDoShortCastle);
            Assert.IsTrue(board.WhiteCanDoLongCastle);
            Assert.IsTrue(board.BlackCanDoShortCastle);
            Assert.IsTrue(board.BlackCanDoLongCastle);
        }

        [Test]
        public void WriteEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            var res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq -", res);
            board.EnPassantMove = CellName.E3;
            res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq e3", res);
        }

        [Test]
        public void ReadEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq -");
            Assert.AreEqual(CellName.None, board.EnPassantMove);
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq e3");
            Assert.AreEqual(CellName.E3, board.EnPassantMove);
        }

        [Test]
        public void WriteTurnTest() {
            var board = new ChessBoard();
            board.Turn = PieceColor.White;
            var res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq -", res);
            board.Turn = PieceColor.Black;
            res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 b KQkq -", res);
        }

        [Test]
        public void ReadTurnTest() {
            var board = new ChessBoard();
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq -");
            Assert.AreEqual(PieceColor.White, board.Turn);
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 b KQkq -");
            Assert.AreEqual(PieceColor.Black, board.Turn);
        }
    }
}
