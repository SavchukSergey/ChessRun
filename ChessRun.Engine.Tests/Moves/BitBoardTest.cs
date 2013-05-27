using System.Text;
using ChessRun.Engine.Moves;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves {
    [TestClass]
    public class BitBoardTest {

        [TestMethod]
        public void KingA1Test() {
            var mask = BitBoard.KingBitBoards[(int)CellName.A1];
            Assert.AreEqual("00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "11000000" +
                            "01000000",
                            FormatMask(mask));
        }


        [TestMethod]
        public void KingA8Test() {
            var mask = BitBoard.KingBitBoards[(int)CellName.A8];
            Assert.AreEqual("01000000" +
                            "11000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000",
                            FormatMask(mask));
        }

        [TestMethod]
        public void KingH1Test() {
            var mask = BitBoard.KingBitBoards[(int)CellName.H1];
            Assert.AreEqual("00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000011" +
                            "00000010",
                            FormatMask(mask));
        }


        [TestMethod]
        public void KingH8Test() {
            var mask = BitBoard.KingBitBoards[(int)CellName.H8];
            Assert.AreEqual("00000010" +
                            "00000011" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000",
                            FormatMask(mask));
        }

        private static string FormatMask(ulong mask) {
            StringBuilder sb = new StringBuilder();
            for (var row = 7; row >= 0; row--) {
                for (var rank = 0; rank < 8; rank++) {
                    int index = (row * 8 + rank);
                    if ((mask & (1ul << index)) != 0) {
                        sb.Append('1');
                    } else {
                        sb.Append('0');
                    }
                }
                //sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
