﻿using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestOf(typeof(WhitePawnEnPassantCaptureMove))]
    public class WhitePawnEnPassantCaptureMoveTest : BaseTestFixture {

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/ppp1pppp/8/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.E5, CellName.D6).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnEnPassantCaptureMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "ed");
        }
    }
}
