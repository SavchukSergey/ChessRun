﻿using System.Linq;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Pawn {
    [TestClass]
    public class WhitePawnPromotionMoveTest : BasePawnMoveTest {

        [TestMethod]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbn1/pppppppP/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
            var move = board.GetValidMoves(PieceType.WhitePawn, CellName.H7, CellName.H8, PieceType.WhiteQueen).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhitePawnPromotionMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "h8=Q");
        }

    }
}
