using System.Linq;
using ChessRun.Engine.Moves.Rook;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Rook {
    public abstract class BaseRookMoveTest<TRookMoveType> : BaseMoveTest where TRookMoveType : RookMove {

        protected void RunToShortNotationCaptureNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p1R2/8/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Rxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1R1p1R2/8/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Rfxd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Rbxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/4R3/4p3/8/4R3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("R3xe5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("R6xe5", notation);
        }

        protected void RunToShortNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/5R2/8/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Rd5", notation);
        }

        protected void RunToShortNotationDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1R3R2/8/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Rfd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Rbd5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/4R3/8/8/4R3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("R3e5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TRookMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("R6e5", notation);
        }

        protected abstract PieceType PieceType { get; }

        protected override ChessBoard CreateBoard(string fen) {
            var board = base.CreateBoard(fen);

            if (PieceType.GetColor() == PieceColor.Black) {
                InvertColor(board);
            }
            return board;
        }


    }
}
