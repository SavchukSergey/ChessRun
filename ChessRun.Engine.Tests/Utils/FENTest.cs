using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Utils {
    [TestClass]
    public class FENTest : BaseTestFixture {

        [TestMethod]
        public void WriteInitialPositionTest() {
            var board = new ChessBoard();
            SetInitialBoard(board);
            var fen = FEN.GetFEN(board);
            Assert.IsTrue(FEN.INITIAL_POSITION.StartsWith(fen));
        }

        [TestMethod]
        public void ReadInitialPositionTest() {
            var board = new ChessBoard();
            FEN.Setup(board, FEN.INITIAL_POSITION);
            AssertInitialBoard(board);
        }

        [TestMethod]
        public void MinimalStringTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq");
            Assert.AreEqual(PieceColor.Black, board.Turn);
            Assert.IsTrue(board.WhiteCanDoShortCastle);
            Assert.IsTrue(board.WhiteCanDoLongCastle);
            Assert.IsTrue(board.BlackCanDoShortCastle);
            Assert.IsTrue(board.BlackCanDoLongCastle);
        }

        [TestMethod]
        public void WriteEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            var res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq -", res);
            board.EnPassantMove = CellName.E3;
            res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq e3", res);
        }

        [TestMethod]
        public void ReadEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq -");
            Assert.AreEqual(CellName.None, board.EnPassantMove);
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq e3");
            Assert.AreEqual(CellName.E3, board.EnPassantMove);
        }

        [TestMethod]
        public void WriteTurnTest() {
            var board = new ChessBoard();
            board.Turn = PieceColor.White;
            var res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 w KQkq -", res);
            board.Turn = PieceColor.Black;
            res = FEN.GetFEN(board);
            Assert.AreEqual(@"8/8/8/8/8/8/8/8 b KQkq -", res);
        }

        [TestMethod]
        public void ReadTurnTest() {
            var board = new ChessBoard();
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 w KQkq -");
            Assert.AreEqual(PieceColor.White, board.Turn);
            FEN.Setup(board, @"8/8/8/8/8/8/8/8 b KQkq -");
            Assert.AreEqual(PieceColor.Black, board.Turn);
        }
    }
}
