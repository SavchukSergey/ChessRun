using System;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Utils {
    [TestClass]
    public class CellOperationsTest : BaseTestFixture {

        [TestMethod]
        public void IncreaseRankTest() {
            foreach (CellName cell in Enum.GetValues(typeof(CellName))) {
                if (cell == CellName.None) continue;
                var rank = cell.GetRank();
                if (rank == CellRank.R8) continue;
                var res = cell.IncreaseRank();
                Assert.AreEqual(rank + 1, res.GetRank());
            }
        }

        [TestMethod]
        public void DecreaseRankTest() {
            foreach (CellName cell in Enum.GetValues(typeof(CellName))) {
                if (cell == CellName.None) continue;
                var rank = cell.GetRank();
                if (rank == CellRank.R1) continue;
                var res = cell.DecreaseRank();
                Assert.AreEqual(rank - 1, res.GetRank());
            }
        }

        [TestMethod]
        public void IncreaseFileTest() {
            foreach (CellName cell in Enum.GetValues(typeof(CellName))) {
                if (cell == CellName.None) continue;
                var file = cell.GetFile();
                if (file == CellFile.H) continue;
                var res = cell.IncreaseFile();
                Assert.AreEqual(file + 1, res.GetFile());
            }
        }

        [TestMethod]
        public void DecreaseFileTest() {
            foreach (CellName cell in Enum.GetValues(typeof(CellName))) {
                if (cell == CellName.None) continue;
                var file = cell.GetFile();
                if (file == CellFile.A) continue;
                var res = cell.DecreaseFile();
                Assert.AreEqual(file - 1, res.GetFile());
            }
        }

        [TestMethod]
        public void GetCellTest() {
            Assert.AreEqual(CellName.A1, CellOperations.GetCell('A', '1'));
            Assert.AreEqual(CellName.A8, CellOperations.GetCell('a', '8'));
            Assert.AreEqual(CellName.H1, CellOperations.GetCell('H', '1'));
            Assert.AreEqual(CellName.H8, CellOperations.GetCell('h', '8'));

            Assert.AreEqual(CellName.A1, CellOperations.GetCell(1, 1));
            Assert.AreEqual(CellName.H8, CellOperations.GetCell(8, 8));

            try {
                CellOperations.GetCell('I', '1');
                Assert.Fail();
            } catch (FormatException) {

            }

            try {
                CellOperations.GetCell('A', '9');
                Assert.Fail();
            } catch (FormatException) {

            }
        }

    }
}