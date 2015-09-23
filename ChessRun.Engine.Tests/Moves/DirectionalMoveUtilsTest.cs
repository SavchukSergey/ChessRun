using ChessRun.Engine.Moves;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves {
    public class DirectionalMoveUtilsTest : BaseTestFixture {

        [Test]
        public void GetMiddleCellsForMainDiagonal1Test() {
            var cells = DirectionalMoveUtils.GetMiddleCells(CellName.A1, CellName.H8);
            Assert.AreEqual(6, cells.Length);
            Assert.AreEqual(CellName.B2, cells[0]);
            Assert.AreEqual(CellName.C3, cells[1]);
            Assert.AreEqual(CellName.D4, cells[2]);
            Assert.AreEqual(CellName.E5, cells[3]);
            Assert.AreEqual(CellName.F6, cells[4]);
            Assert.AreEqual(CellName.G7, cells[5]);
        }

        [Test]
        public void GetMiddleCellsForMainDiagonal2Test() {
            var cells = DirectionalMoveUtils.GetMiddleCells(CellName.A8, CellName.H1);
            Assert.AreEqual(6, cells.Length);
            Assert.AreEqual(CellName.B7, cells[0]);
            Assert.AreEqual(CellName.C6, cells[1]);
            Assert.AreEqual(CellName.D5, cells[2]);
            Assert.AreEqual(CellName.E4, cells[3]);
            Assert.AreEqual(CellName.F3, cells[4]);
            Assert.AreEqual(CellName.G2, cells[5]);
        }

        [Test]
        public void GetMiddleCellsForHorizonralTest() {
            var cells = DirectionalMoveUtils.GetMiddleCells(CellName.C4, CellName.G4);
            Assert.AreEqual(3, cells.Length);
            Assert.AreEqual(CellName.D4, cells[0]);
            Assert.AreEqual(CellName.E4, cells[1]);
            Assert.AreEqual(CellName.F4, cells[2]);
        }

        [Test]
        public void GetMiddleCellsForVerticalTest() {
            var cells = DirectionalMoveUtils.GetMiddleCells(CellName.C7, CellName.C2);
            Assert.AreEqual(4, cells.Length);
            Assert.AreEqual(CellName.C6, cells[0]);
            Assert.AreEqual(CellName.C5, cells[1]);
            Assert.AreEqual(CellName.C4, cells[2]);
            Assert.AreEqual(CellName.C3, cells[3]);
        }
    }
}
