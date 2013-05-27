using System.Linq;
using ChessRun.Engine.Moves.Queen;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Queen {
    public abstract class BaseQueenMoveTest<TQueenMoveType> : BaseMoveTest where TQueenMoveType : QueenMove {

        protected void RunToShortNotationCaptureNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p1Q2/8/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1Q1p1Q2/8/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qfxd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Qbxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/4Q3/4p3/8/4Q3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Q3xe5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Q6xe5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3QQQ2/3QpQ2/3QQQ2/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qf4xe4", notation);

            move = board.GetValidMoves(PieceType, CellName.D4, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Qd4xe4", notation);
        }

        protected void RunToShortNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/5Q2/8/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qd5", notation);
        }

        protected void RunToShortNotationDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/1Q3Q2/8/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qfd5", notation);

            move = board.GetValidMoves(PieceType, CellName.B5, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Qbd5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/4Q3/8/8/4Q3/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E3, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Q3e5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.E5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Q6e5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3QQQ2/3Q1Q2/3QQQ2/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.F4, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Qf4e4", notation);

            move = board.GetValidMoves(PieceType, CellName.D4, CellName.E4).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TQueenMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Qd4e4", notation);
        }

        protected abstract PieceType PieceType { get; }

        protected override ChessBoard CreateBoard(string fen) {
            var board = base.CreateBoard(fen);

            if (PieceOperations.GetColor(PieceType) == PieceColor.Black) {
                InvertColor(board);
            }
            return board;
        }


    }
}
