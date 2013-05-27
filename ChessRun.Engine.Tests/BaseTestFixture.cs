using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests {
    public class BaseTestFixture {

        protected void SetInitialBoard(ChessBoard board) {
            //TODO: don't use FEN.
            FEN.Setup(board, FEN.INITIAL_POSITION);
        }

        protected void AssertInitialBoard(ChessBoard board) {
            Assert.AreEqual(PieceType.BlackRook, board[CellName.A8]);
            Assert.AreEqual(PieceType.BlackKnight, board[CellName.B8]);
            Assert.AreEqual(PieceType.BlackBishop, board[CellName.C8]);
            Assert.AreEqual(PieceType.BlackQueen, board[CellName.D8]);
            Assert.AreEqual(PieceType.BlackKing, board[CellName.E8]);
            Assert.AreEqual(PieceType.BlackBishop, board[CellName.F8]);
            Assert.AreEqual(PieceType.BlackKnight, board[CellName.G8]);
            Assert.AreEqual(PieceType.BlackRook, board[CellName.H8]);

            Assert.AreEqual(PieceType.BlackPawn, board[CellName.A7], "A7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.B7], "B7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.C7], "C7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.D7], "D7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.E7], "E7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.F7], "F7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.G7], "G7");
            Assert.AreEqual(PieceType.BlackPawn, board[CellName.H7], "H7");

            Assert.AreEqual(PieceType.None, board[CellName.A6], "A6");
            Assert.AreEqual(PieceType.None, board[CellName.B6], "B6");
            Assert.AreEqual(PieceType.None, board[CellName.C6], "C6");
            Assert.AreEqual(PieceType.None, board[CellName.D6], "D6");
            Assert.AreEqual(PieceType.None, board[CellName.E6], "E6");
            Assert.AreEqual(PieceType.None, board[CellName.F6], "F6");
            Assert.AreEqual(PieceType.None, board[CellName.G6], "G6");
            Assert.AreEqual(PieceType.None, board[CellName.H6], "H6");

            Assert.AreEqual(PieceType.None, board[CellName.A5], "A5");
            Assert.AreEqual(PieceType.None, board[CellName.B5], "B5");
            Assert.AreEqual(PieceType.None, board[CellName.C5], "C5");
            Assert.AreEqual(PieceType.None, board[CellName.D5], "D5");
            Assert.AreEqual(PieceType.None, board[CellName.E5], "E5");
            Assert.AreEqual(PieceType.None, board[CellName.F5], "F5");
            Assert.AreEqual(PieceType.None, board[CellName.G5], "G5");
            Assert.AreEqual(PieceType.None, board[CellName.H5], "H5");

            Assert.AreEqual(PieceType.None, board[CellName.A4]);
            Assert.AreEqual(PieceType.None, board[CellName.B4]);
            Assert.AreEqual(PieceType.None, board[CellName.C4]);
            Assert.AreEqual(PieceType.None, board[CellName.D4]);
            Assert.AreEqual(PieceType.None, board[CellName.E4]);
            Assert.AreEqual(PieceType.None, board[CellName.F4]);
            Assert.AreEqual(PieceType.None, board[CellName.G4]);
            Assert.AreEqual(PieceType.None, board[CellName.H4]);

            Assert.AreEqual(PieceType.None, board[CellName.A3]);
            Assert.AreEqual(PieceType.None, board[CellName.B3]);
            Assert.AreEqual(PieceType.None, board[CellName.C3]);
            Assert.AreEqual(PieceType.None, board[CellName.D3]);
            Assert.AreEqual(PieceType.None, board[CellName.E3]);
            Assert.AreEqual(PieceType.None, board[CellName.F3]);
            Assert.AreEqual(PieceType.None, board[CellName.G3]);
            Assert.AreEqual(PieceType.None, board[CellName.H3]);

            Assert.AreEqual(PieceType.WhitePawn, board[CellName.A2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.B2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.C2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.D2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.E2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.F2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.G2]);
            Assert.AreEqual(PieceType.WhitePawn, board[CellName.H2]);

            Assert.AreEqual(PieceType.WhiteRook, board[CellName.A1], "A1");
            Assert.AreEqual(PieceType.WhiteKnight, board[CellName.B1], "B1");
            Assert.AreEqual(PieceType.WhiteBishop, board[CellName.C1], "C1");
            Assert.AreEqual(PieceType.WhiteQueen, board[CellName.D1], "D1");
            Assert.AreEqual(PieceType.WhiteKing, board[CellName.E1], "E1");
            Assert.AreEqual(PieceType.WhiteBishop, board[CellName.F1], "F1");
            Assert.AreEqual(PieceType.WhiteKnight, board[CellName.G1], "G1");
            Assert.AreEqual(PieceType.WhiteRook, board[CellName.H1], "H1");

            Assert.AreEqual(CellName.None, board.EnPassantMove);
            Assert.AreEqual(PieceColor.White, board.Turn);

            Assert.IsTrue(board.BlackCanDoLongCastle);
            Assert.IsTrue(board.BlackCanDoShortCastle);
            Assert.IsTrue(board.WhiteCanDoLongCastle);
            Assert.IsTrue(board.WhiteCanDoShortCastle);
        }

        protected virtual ChessBoard CreateBoard(string fen) {
            var board = new ChessBoard();
            FEN.Setup(board, fen);
            return board;
        }

        protected virtual void InvertColor(ChessBoard board) {
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                var piece = board[cell];
                board[cell] = PieceOperations.InvertColor(piece);
            }
            board.SwitchTurn();
        }
    }
}
