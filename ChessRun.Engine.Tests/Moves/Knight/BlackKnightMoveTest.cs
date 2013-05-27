using ChessRun.Engine.Moves.Knight;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.Knight {
    [TestClass]
    public class BlackKnightMoveTest : BaseKnightMoveTest<BlackKnightMove> {

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
            get { return PieceType.BlackKnight; }
        }

    }
}
