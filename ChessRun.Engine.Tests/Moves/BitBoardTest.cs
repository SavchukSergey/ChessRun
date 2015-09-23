using System.Text;
using ChessRun.Engine.Moves;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves {
    public class BitBoardTest {

        [Test]
        public void KingA1Test() {
            var mask = BitBoard.Cells[(int)CellName.A1].Kings;
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


        [Test]
        public void KingA8Test() {
            var mask = BitBoard.Cells[(int)CellName.A8].Kings;
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

        [Test]
        public void KingH1Test() {
            var mask = BitBoard.Cells[(int)CellName.H1].Kings;
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


        [Test]
        public void KingH8Test() {
            var mask = BitBoard.Cells[(int)CellName.H8].Kings;
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

        [Test]
        public void VerticalB4Test() {
            var mask = BitBoard.Cells[(int)CellName.B4].Vertical;
            Assert.AreEqual("01000000" +
                            "01000000" +
                            "01000000" +
                            "01000000" +
                            "00000000" +
                            "01000000" +
                            "01000000" +
                            "01000000",
                            FormatMask(mask));
        }

        [Test]
        public void HorizontalB4Test() {
            var mask = BitBoard.Cells[(int)CellName.B4].Horizontal;
            Assert.AreEqual("00000000" +
                            "00000000" +
                            "00000000" +
                            "00000000" +
                            "10111111" +
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
