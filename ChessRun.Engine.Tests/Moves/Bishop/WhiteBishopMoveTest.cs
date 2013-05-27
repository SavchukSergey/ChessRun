using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Bishop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Bishop {
    [TestClass]
    public class WhiteBishopMoveTest : BaseBishopMoveTest<WhiteBishopMove> {

        [TestMethod]
        public void FastValidateTest() {
            var move = new WhiteBishopMove(CellName.A1, CellName.G7);

            var board = new ChessBoard();
            board[move.From] = PieceType.WhiteQueen;

            board[move.To] = PieceType.BlackKnight;
            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.Valid, move.FastValidate(board));

            board[move.To] = PieceType.WhiteKnight;
            Assert.AreEqual(ValidationResult.Invalid, move.FastValidate(board));
        }

        [TestMethod]
        public void ToShortNotationCaptureNotationTest() {
            RunToShortNotationCaptureNotationTest();
        }

        [TestMethod]
        public void ToShortNotationCaptureDisambiguatingFileTest() {
            RunToShortNotationCaptureDisambiguatingFileTest();
        }

        [TestMethod]
        public void ToShortNotationCaptureDisambiguatingRankTest() {
            RunToShortNotationCaptureDisambiguatingRankTest();
        }

        [TestMethod]
        public void ToShortNotationCaptureDisambiguatingRankAndFileTest() {
            RunToShortNotationCaptureDisambiguatingRankAndFileTest();
        }

        [TestMethod]
        public void ToShortNotationTest() {
            RunToShortNotationTest();
        }

        [TestMethod]
        public void ToShortNotationDisambiguatingFileTest() {
            RunToShortNotationDisambiguatingFileTest();
        }

        [TestMethod]
        public void ToShortNotationDisambiguatingRankTest() {
            RunToShortNotationDisambiguatingRankTest();
        }

        [TestMethod]
        public void ToShortNotationDisambiguatingRankAndFileTest() {
            RunToShortNotationDisambiguatingRankAndFileTest();
        }

        protected override PieceType PieceType {
            get { return PieceType.WhiteBishop; }
        }

    }
}
