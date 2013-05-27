using System.Linq;
using ChessRun.Engine.Moves.Bishop;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Bishop {
    public abstract class BaseBishopMoveTest<TBishopMoveType> : BaseMoveTest where TBishopMoveType : BishopMove {

        protected void RunToShortNotationCaptureNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p4/4B3/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Bxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/3p4/2B1B3/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Bexd5", notation);

            move = board.GetValidMoves(PieceType, CellName.C4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Bcxd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/8/3pBp2/3pBp2/3pBp2/8/8/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("B4xd5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("B6xd5", notation);
        }

        protected void RunToShortNotationCaptureDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/2BpBp2/3pBp2/2BpBp2/8/8/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Be4xd5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Be6xd5", notation);
        }

        protected void RunToShortNotationTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/8/4B3/8/PPPP1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Bd5", notation);
        }

        protected void RunToShortNotationDisambiguatingFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/8/8/2B1B3/8/PP1P1PPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Bed5", notation);

            move = board.GetValidMoves(PieceType, CellName.C4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Bcd5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankTest() {
            var board = CreateBoard("rnbqkbnr/8/3pBp2/4Bp2/3pBp2/8/8/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("B4d5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("B6d5", notation);
        }

        protected void RunToShortNotationDisambiguatingRankAndFileTest() {
            var board = CreateBoard("rnbqkbnr/pppppppp/2BpBp2/4Bp2/2BpBp2/8/8/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType, CellName.E4, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual("Be4d5", notation);

            move = board.GetValidMoves(PieceType, CellName.E6, CellName.D5).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is TBishopMoveType);
            notation = move.ToShortNotation(board);
            Assert.AreEqual("Be6d5", notation);
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
