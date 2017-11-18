using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.Bishop;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Bishop {
    [TestOf(typeof(WhiteBishopMove))]
    public class WhiteBishopMoveTest : BaseBishopMoveTest<WhiteBishopMove> {

        [Test]
        public void FastValidateTest() {
            var move = new WhiteBishopMove(CellName.A1, CellName.G7);

            var board = new ChessBoard {
                [move.From] = PieceType.WhiteQueen,
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
        public void ToShortNotationCaptureDisambiguatingRankAndFileTest() {
            RunToShortNotationCaptureDisambiguatingRankAndFileTest();
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

        [Test]
        public void ToShortNotationDisambiguatingRankAndFileTest() {
            RunToShortNotationDisambiguatingRankAndFileTest();
        }

        protected override PieceType PieceType {
            get { return PieceType.WhiteBishop; }
        }

    }
}
