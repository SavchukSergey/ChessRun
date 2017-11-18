using ChessRun.Engine.Moves.Knight;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.Knight {
    [TestOf(typeof(BlackKnightMove))]
    public class BlackKnightMoveTest : BaseKnightMoveTest<BlackKnightMove> {

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
            get { return PieceType.BlackKnight; }
        }

    }
}
