using ChessRun.Engine.Moves.King;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests {
    [TestOf(typeof(ChessBoard))]
    public class ChessBoardTest : BaseTestFixture {

        [Test]
        public void IsAttackedByWhiteHorizontalCheckTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "r4k1Q/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N4p/PPPBBPPP/R3K2R b KQ -");
            Assert.IsTrue(board.IsAttackedByWhite(CellName.G8));
        }

        [Test]
        public void IsAttackedByBlackVerticalCheckTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "5rk1/8/8/8/8/8/5K2/8 w - -");
            Assert.IsTrue(board.IsAttackedByBlack(CellName.F3));
        }

        [Test]
        public void ResetTest() {
            var board = new ChessBoard();
            board.Reset();
            Assert.AreEqual(PieceType.WhiteRook, board[CellName.A1]);
            Assert.AreEqual(PieceType.WhiteKnight, board[CellName.B1]);
            Assert.AreEqual(PieceType.WhiteBishop, board[CellName.C1]);
            Assert.AreEqual(PieceType.WhiteQueen, board[CellName.D1]);
            Assert.AreEqual(PieceType.WhiteKing, board[CellName.E1]);
            Assert.AreEqual(PieceType.WhiteBishop, board[CellName.F1]);
            Assert.AreEqual(PieceType.WhiteKnight, board[CellName.G1]);
            Assert.AreEqual(PieceType.WhiteRook, board[CellName.H1]);

            Assert.AreEqual(PieceType.WhitePawn, board[CellName.A2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.B2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.C2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.D2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.E2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.F2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.G2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.H2]);

            Assert.AreEqual(PieceType.None, board[CellName.A3]);
            Assert.AreEqual(PieceType.None, board[CellName.B3]);
            Assert.AreEqual(PieceType.None, board[CellName.C3]);
            Assert.AreEqual(PieceType.None, board[CellName.D3]);
            Assert.AreEqual(PieceType.None, board[CellName.E3]);
            Assert.AreEqual(PieceType.None, board[CellName.F3]);
            Assert.AreEqual(PieceType.None, board[CellName.G3]);
            Assert.AreEqual(PieceType.None, board[CellName.H3]);
        }

        [Test]
        public void GetMoveWhiteShortCastleTest() {
            var chessBoard = new ChessBoard();
            chessBoard.Reset();
            chessBoard[CellName.F1] = PieceType.None;
            chessBoard[CellName.G1] = PieceType.None;
            chessBoard.Turn = PieceColor.White;
            var move = chessBoard.GetMove("O-O");
            Assert.IsTrue(move is WhiteShortCastleMove);
        }

        [Test]
        public void GetMoveBlackShortCastleTest() {
            var chessBoard = new ChessBoard();
            chessBoard.Reset();
            chessBoard[CellName.F8] = PieceType.None;
            chessBoard[CellName.G8] = PieceType.None;
            chessBoard.Turn = PieceColor.Black;
            var move = chessBoard.GetMove("O-O");
            Assert.IsTrue(move is BlackShortCastleMove);
        }

        [Test]
        public void GetMoveWhiteLongCastleTest() {
            var chessBoard = new ChessBoard();
            chessBoard.Reset();
            chessBoard[CellName.D1] = PieceType.None;
            chessBoard[CellName.C1] = PieceType.None;
            chessBoard[CellName.B1] = PieceType.None;
            chessBoard.Turn = PieceColor.White;
            var move = chessBoard.GetMove("O-O-O");
            Assert.IsTrue(move is WhiteLongCastleMove);
        }

        [Test]
        public void GetMoveBlackLongCastleTest() {
            var chessBoard = new ChessBoard();
            chessBoard.Reset();
            chessBoard[CellName.D8] = PieceType.None;
            chessBoard[CellName.C8] = PieceType.None;
            chessBoard[CellName.B8] = PieceType.None;
            chessBoard.Turn = PieceColor.Black;
            var move = chessBoard.GetMove("O-O-O");
            Assert.IsTrue(move is BlackLongCastleMove);
        }
    }
}