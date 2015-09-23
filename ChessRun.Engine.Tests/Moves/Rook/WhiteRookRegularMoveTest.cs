using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Rook;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Rook {
    public class WhiteRookRegularMoveTest : BaseRookMoveTest<WhiteRookMove> {

        [Test]
        public void FastValidateTest() {
            var move = new WhiteRookRegularMove(CellName.A1, CellName.A7);

            var board = new ChessBoard {
                [move.From] = PieceType.WhiteRook,
                [move.To] = PieceType.BlackKnight
            };

            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.Valid, move.FastValidate(board));

            board[move.To] = PieceType.WhiteKnight;
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

        protected override PieceType PieceType => PieceType.WhiteRook;

    }
}
