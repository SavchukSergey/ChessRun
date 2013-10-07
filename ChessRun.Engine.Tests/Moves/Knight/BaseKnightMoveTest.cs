using System.Linq;
using ChessRun.Engine.Moves.Knight;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Knight {
    public abstract class BaseKnightMoveTest<TKnightMoveType> : BaseMoveTest where TKnightMoveType : KnightMove {

        protected void RunToShortNotationCaptureNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p4/5N2/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p4/1N3N2/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nfxd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Nbxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/ppppNppp/8/3p1p2/8/4N3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("N3xd5", notation);

            move = board.GetValidMoves(PieceType, CellName.E7, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("N7xd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1N3N2/3p4/1N3N2/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F3, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nf3xd4", notation);

            move = board.GetValidMoves(PieceType, CellName.F5, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Nf5xd4", notation);
        }

        protected void RunToShortNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/8/5N2/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nd5", notation);
        }

        protected void RunToShortNotationDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/8/1N3N2/8/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nfd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Nbd5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/ppppNppp/8/8/8/4N3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("N3d5", notation);

            move = board.GetValidMoves(PieceType, CellName.E7, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("N7d5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1N3N2/8/1N3N2/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F3, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Nf3d4", notation);

            move = board.GetValidMoves(PieceType, CellName.F5, CellName.D4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TKnightMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Nf5d4", notation);
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
