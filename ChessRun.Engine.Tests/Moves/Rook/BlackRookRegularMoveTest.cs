using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Rook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Rook {
    [TestClass]
    public class BlackRookRegularMoveTest : BaseRookMoveTest<BlackRookMove> {

        [TestMethod]
        public void FastValidateTest() {
            var move = new BlackRookRegularMove(CellName.A1, CellName.A7);

            var board = new ChessBoard();
            board[move.From] = PieceType.BlackRook;

            board[move.To] = PieceType.WhiteKnight;
            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.Valid, move.FastValidate(board));

            board[move.To] = PieceType.BlackKnight;
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

        protected override PieceType PieceType {
            get { return PieceType.BlackRook; }
        }
    }
}
