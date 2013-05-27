using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Utils {
    [TestClass]
    public class BFENTest : BaseTestFixture {

        [TestMethod]
        public void WriteInitialPositionTest() {
            var board = new ChessBoard();
            SetInitialBoard(board);
            var bfen = BFEN.GetPackedBFEN(board);
            Assert.AreEqual(BFEN.INITIAL_POSITION, bfen);
        }

        [TestMethod]
        public void ReadInitialPositionTest() {
            var board = new ChessBoard();
            BFEN.Setup(board, BFEN.INITIAL_POSITION);
            AssertInitialBoard(board);
        }

        [TestMethod]
        public void WriteEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            var res = BFEN.GetPackedBFEN(board);
            Assert.AreEqual(@"//////////8P", res);
            board.EnPassantMove = CellName.E3;
            res = BFEN.GetPackedBFEN(board);
            Assert.AreEqual(@"//////////9PQA==", res);
        }

        [TestMethod]
        public void ReadEnpassantTest() {
            var board = new ChessBoard();
            board.EnPassantMove = CellName.None;
            BFEN.Setup(board, @"//////////8P");
            Assert.AreEqual(CellName.None, board.EnPassantMove);
            BFEN.Setup(board, @"//////////9PQA==");
            Assert.AreEqual(CellName.E3, board.EnPassantMove);
        }

        [TestMethod]
        public void WriteTurnTest() {
            var board = new ChessBoard();
            board.Turn = PieceColor.White;
            var res = BFEN.GetPackedBFEN(board);
            Assert.AreEqual(@"//////////8P", res);
            board.Turn = PieceColor.Black;
            res = BFEN.GetPackedBFEN(board);
            Assert.AreEqual(@"//////////+P", res);
        }

        [TestMethod]
        public void ReadTurnTest() {
            var board = new ChessBoard();
            BFEN.Setup(board, @"//////////8P");
            Assert.AreEqual(PieceColor.White, board.Turn);
            BFEN.Setup(board, @"//////////+P");
            Assert.AreEqual(PieceColor.Black, board.Turn);
        }
    }
}
