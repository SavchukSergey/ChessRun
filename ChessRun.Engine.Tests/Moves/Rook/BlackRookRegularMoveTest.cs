using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Rook;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Rook {
    public class BlackRookRegularMoveTest : BaseRookMoveTest<BlackRookMove> {

        [Test]
        public void FastValidateTest() {
            var move = new BlackRookRegularMove(CellName.A1, CellName.A7);

            var board = new ChessBoard {
                [move.From] = PieceType.BlackRook,
                [move.To] = PieceType.WhiteKnight
            };

            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.Valid, move.FastValidate(board));

            board[move.To] = PieceType.BlackKnight;
            Assert.AreEqual(ValidationResult.Invalid, move.FastValidate(board));
        }

        [Test]
        public void ToShortNotationCaptureNotationTest() {
            RunToShortNotationCaptureNotationTest();
        }

        [Test]
        public void ToShortNotationCaptureDisambiguatingFileTest() {
            RunToShortNotationCaptureDisambiguatingFileTest();
        }

        [Test]
        public void ToShortNotationCaptureDisambiguatingRankTest() {
            RunToShortNotationCaptureDisambiguatingRankTest();
        }

        [Test]
        public void ToShortNotationTest() {
            RunToShortNotationTest();
        }

        [Test]
        public void ToShortNotationDisambiguatingFileTest() {
            RunToShortNotationDisambiguatingFileTest();
        }

        [Test]
        public void ToShortNotationDisambiguatingRankTest() {
            RunToShortNotationDisambiguatingRankTest();
        }

        protected override PieceType PieceType => PieceType.BlackRook;

    }
}
