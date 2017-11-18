using System;
using NUnit.Framework;

namespace ChessRun.Pgn.Tests {
    [TestOf(typeof(PgnFile))]
    public class PgnFileTest {

        [Test]
        public void EventTest() {
            var file = new PgnFile();
            var expected = Guid.NewGuid().ToString();
            file.Event = expected;
            Assert.AreEqual(expected, file.Event);
        }

        [Test]
        public void SiteTest() {
            var file = new PgnFile();
            var expected = Guid.NewGuid().ToString();
            file.Site = expected;
            Assert.AreEqual(expected, file.Site);
        }

        [Test]
        public void WhiteTest() {
            var file = new PgnFile();
            var expected = Guid.NewGuid().ToString();
            file.White = expected;
            Assert.AreEqual(expected, file.White);
        }

        [Test]
        public void BlackTest() {
            var file = new PgnFile();
            var expected = Guid.NewGuid().ToString();
            file.Black = expected;
            Assert.AreEqual(expected, file.Black);
        }

        [Test]
        public void ResultTest() {
            var file = new PgnFile();
            var expected = Guid.NewGuid().ToString();
            file.Result = expected;
            Assert.AreEqual(expected, file.Result);
        }

        [Test]
        public void MovesWhitespaceTest() {
            string pgnString = @"
[Event ""Live Chess""]

1.e4 e5 2.f4 exf4 3.Nf3 Bc5 4.c3 d6 5.d4 Bb6 6.Bxf4 Bg4 7.Be2 Bxf3 8.Bxf3 Nc6 9.O-O g5 10.Bg3 Nf6
 11.e5 Nxe5 12.Bxe5 dxe5 13.Bxb7 exd4 14.Re1+ Kf8 15.Bxa8 dxc3+ 16.Kh1 Qxd1 17.Rxd1 Kg7 18.Nxc3 Rxa8 19.Rf1 Re8 20.Rae1 Rxe1
 21.Rxe1 Bd4 22.Re7 Bxc3 23.bxc3 Nd5 24.Re5 Nxc3 25.Rxg5+ Kf6 26.Ra5 c5 27.Rxc5 Nxa2 28.Ra5 Nc3 29.Rxa7 Ne4 30.Ra5 Ng5
 31.h3 h6 32.Kh2 Kg6 33.g4 f6 34.h4 Nf3+ 35.Kg3 Ne5 36.h5+ Kg5 37.Ra6 Nxg4 38.Ra5+ Ne5 39.Ra6 f5 40.Ra5 Nc4
 41.Ra4 Nd6 42.Ra6 Ne4+ 43.Kh3 Kxh5 44.Ra8 Ng5+ 45.Kg3 Ne4+ 46.Kf4 Kg6 47.Rf8 Nd6 48.Rg8+ Kh7 49.Rg1 h5 50.Ke5
 1-0
";
            var pgn = new PgnReader().Read(pgnString);
            Assert.AreEqual(50, pgn.Moves.Count);
        }

        [Test]
        public void LongCastleWithFlagsTest() {
            string pgnString = "1.O-O-O+ O-O-O#";
            var pgn = new PgnReader().Read(pgnString);
            Assert.AreEqual(1, pgn.Moves.Count);
            var move = pgn.Moves[0];
            Assert.AreEqual("O-O-O+", move.Whites);
            Assert.AreEqual("O-O-O#", move.Blacks);
        }

        [Test]
        public void ShortCastleWithFlagsTest() {
            string pgnString = "1.O-O+ O-O#";
            var pgn = new PgnReader().Read(pgnString);
            Assert.AreEqual(1, pgn.Moves.Count);
            var move = pgn.Moves[0];
            Assert.AreEqual("O-O+", move.Whites);
            Assert.AreEqual("O-O#", move.Blacks);
        }
    }
}
