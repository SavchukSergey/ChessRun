using System.Linq;
using ChessRun.Engine.Moves;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves {
    public class MoveMatrixTest {

        [Test]
        public void WhitePawnTest() {
            Assert.IsNotNull(GetMove(PieceType.WhitePawn, CellName.E2, CellName.E4));
            Assert.IsNotNull(GetMove(PieceType.WhitePawn, CellName.E2, CellName.D3));
            Assert.IsNotNull(GetMove(PieceType.WhitePawn, CellName.E2, CellName.F3));
            Assert.IsNull(GetMove(PieceType.WhitePawn, CellName.E2, CellName.E1));
            Assert.IsNull(GetMove(PieceType.WhitePawn, CellName.E3, CellName.E5));
        }

        [Test]
        public void BlackPawnTest() {
            Assert.IsNotNull(GetMove(PieceType.BlackPawn, CellName.E7, CellName.E5));
            Assert.IsNotNull(GetMove(PieceType.BlackPawn, CellName.E7, CellName.D6));
            Assert.IsNotNull(GetMove(PieceType.BlackPawn, CellName.E7, CellName.F6));
            Assert.IsNull(GetMove(PieceType.BlackPawn, CellName.E7, CellName.E8));
            Assert.IsNull(GetMove(PieceType.BlackPawn, CellName.E6, CellName.E4));
        }

        [Test]
        public void WhitePawnRank5Test() {
            Assert.IsNotNull(GetMove(PieceType.WhitePawn, CellName.E5, CellName.E6));
        }

        [Test]
        public void BlackPawnRank4Test() {
            Assert.IsNotNull(GetMove(PieceType.BlackPawn, CellName.E4, CellName.E3));
        }

        [Test]
        public void WhiteBishopTest() {
            Assert.IsNotNull(GetMove(PieceType.WhiteBishop, CellName.A1, CellName.H8));
            Assert.IsNotNull(GetMove(PieceType.WhiteBishop, CellName.A1, CellName.G7));
            Assert.IsNull(GetMove(PieceType.WhiteBishop, CellName.A1, CellName.G8));
        }

        [Test]
        public void BlackBishopTest() {
            Assert.IsNotNull(GetMove(PieceType.BlackBishop, CellName.A8, CellName.H1));
            Assert.IsNotNull(GetMove(PieceType.BlackBishop, CellName.A8, CellName.G2));
            Assert.IsNull(GetMove(PieceType.BlackBishop, CellName.A8, CellName.G3));
        }

        private static SpeculativeMove GetMove(PieceType piece, CellName from, CellName to) {
            var moves = MoveMatrix.GetDirectMoves(piece, from);
            return moves.FirstOrDefault(item => item.To == to);
        }
    }
}