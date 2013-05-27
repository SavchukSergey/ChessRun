using ChessRun.Engine.Moves;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests {
    [TestClass]
    public class CastleTest : BaseTestFixture {

        [TestMethod]
        public void RookCaptureWhiteA1A8Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1");
            var move = board.GetMove("Ra1a8");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoLongCastle);
            Assert.IsFalse(board.WhiteCanDoLongCastle);
        }

        [TestMethod]
        public void RookCaptureBlackA8A1Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1");
            var move = board.GetMove("Ra8a1");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoLongCastle);
            Assert.IsFalse(board.WhiteCanDoLongCastle);
        }

        [TestMethod]
        public void RookCaptureWhiteH1H8Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1");
            var move = board.GetMove("Rh1h8");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoShortCastle);
            Assert.IsFalse(board.WhiteCanDoShortCastle);
        }

        [TestMethod]
        public void RookCaptureBlackH8H1Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1");
            var move = board.GetMove("Rh8h1");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoShortCastle);
            Assert.IsFalse(board.WhiteCanDoShortCastle);
        }

        [TestMethod]
        public void RookCaptureWhiteG8H8Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3knRr/8/8/8/8/8/8/R3K2R w KQkq - 0 1");
            var move = board.GetMove("Rg8h8");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoShortCastle);
            Assert.IsTrue(board.WhiteCanDoShortCastle);
        }

        [TestMethod]
        public void RookCaptureBlackG1H1Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R3KNrR b KQkq - 0 1");
            var move = board.GetMove("Rg1h1");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.WhiteCanDoShortCastle);
            Assert.IsTrue(board.BlackCanDoShortCastle);
        }

        [TestMethod]
        public void RookCaptureWhiteC8A8Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r1Rnk2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1");
            var move = board.GetMove("Rc8a8");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.BlackCanDoLongCastle);
            Assert.IsTrue(board.WhiteCanDoLongCastle);
        }

        [TestMethod]
        public void RookCaptureBlackC1A1Test() {
            var board = new ChessBoard();
            FEN.Setup(board, "r3k2r/8/8/8/8/8/8/R1rNK2R b KQkq - 0 1");
            var move = board.GetMove("Rc1a1");
            RollbackData rollback;
            board.Move(move, out rollback);
            Assert.IsFalse(board.WhiteCanDoLongCastle);
            Assert.IsTrue(board.BlackCanDoLongCastle);
        }
    }
}