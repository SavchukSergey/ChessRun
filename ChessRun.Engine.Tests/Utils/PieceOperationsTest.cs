using System;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Utils {
    public class PieceOperationsTest : BaseTestFixture {

        [Test]
        public void GetPawnTest() {
            Assert.AreEqual(PieceType.WhitePawn, PieceOperations.GetPawn(PieceColor.White));
            Assert.AreEqual(PieceType.BlackPawn, PieceOperations.GetPawn(PieceColor.Black));
            try {
                PieceOperations.GetPawn(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void GetKnightTest() {
            Assert.AreEqual(PieceType.WhiteKnight, PieceOperations.GetKnight(PieceColor.White));
            Assert.AreEqual(PieceType.BlackKnight, PieceOperations.GetKnight(PieceColor.Black));
            try {
                PieceOperations.GetKnight(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void GetBishopTest() {
            Assert.AreEqual(PieceType.WhiteBishop, PieceOperations.GetBishop(PieceColor.White));
            Assert.AreEqual(PieceType.BlackBishop, PieceOperations.GetBishop(PieceColor.Black));
            try {
                PieceOperations.GetBishop(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void GetRookTest() {
            Assert.AreEqual(PieceType.WhiteRook, PieceOperations.GetRook(PieceColor.White));
            Assert.AreEqual(PieceType.BlackRook, PieceOperations.GetRook(PieceColor.Black));
            try {
                PieceOperations.GetRook(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void GetQueenTest() {
            Assert.AreEqual(PieceType.WhiteQueen, PieceOperations.GetQueen(PieceColor.White));
            Assert.AreEqual(PieceType.BlackQueen, PieceOperations.GetQueen(PieceColor.Black));
            try {
                PieceOperations.GetQueen(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void GetKingTest() {
            Assert.AreEqual(PieceType.WhiteKing, PieceOperations.GetKing(PieceColor.White));
            Assert.AreEqual(PieceType.BlackKing, PieceOperations.GetKing(PieceColor.Black));
            try {
                PieceOperations.GetKing(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [Test]
        public void IsWhiteTest() {
            Assert.IsFalse(PieceType.None.IsWhite());
            Assert.IsTrue(PieceType.WhiteBishop.IsWhite());
            Assert.IsTrue(PieceType.WhiteKing.IsWhite());
            Assert.IsTrue(PieceType.WhiteKnight.IsWhite());
            Assert.IsTrue(PieceType.WhitePawn.IsWhite());
            Assert.IsTrue(PieceType.WhiteQueen.IsWhite());
            Assert.IsTrue(PieceType.WhiteRook.IsWhite());
            Assert.IsFalse(PieceType.BlackBishop.IsWhite());
            Assert.IsFalse(PieceType.BlackKing.IsWhite());
            Assert.IsFalse(PieceType.BlackKnight.IsWhite());
            Assert.IsFalse(PieceType.BlackPawn.IsWhite());
            Assert.IsFalse(PieceType.BlackQueen.IsWhite());
            Assert.IsFalse(PieceType.BlackRook.IsWhite());
        }


        [Test]
        public void IsWhiteOrEmptyTest() {
            Assert.IsTrue(PieceType.None.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhiteBishop.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhiteKing.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhiteKnight.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhitePawn.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhiteQueen.IsWhiteOrEmpty());
            Assert.IsTrue(PieceType.WhiteRook.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackBishop.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackKing.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackKnight.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackPawn.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackQueen.IsWhiteOrEmpty());
            Assert.IsFalse(PieceType.BlackRook.IsWhiteOrEmpty());
        }

        [Test]
        public void IsBlackTest() {
            Assert.IsFalse(PieceType.None.IsBlack());
            Assert.IsFalse(PieceType.WhiteBishop.IsBlack());
            Assert.IsFalse(PieceType.WhiteKing.IsBlack());
            Assert.IsFalse(PieceType.WhiteKnight.IsBlack());
            Assert.IsFalse(PieceType.WhitePawn.IsBlack());
            Assert.IsFalse(PieceType.WhiteQueen.IsBlack());
            Assert.IsFalse(PieceType.WhiteRook.IsBlack());
            Assert.IsTrue(PieceType.BlackBishop.IsBlack());
            Assert.IsTrue(PieceType.BlackKing.IsBlack());
            Assert.IsTrue(PieceType.BlackKnight.IsBlack());
            Assert.IsTrue(PieceType.BlackPawn.IsBlack());
            Assert.IsTrue(PieceType.BlackQueen.IsBlack());
            Assert.IsTrue(PieceType.BlackRook.IsBlack());
        }

        [Test]
        public void IsBlackOrEmptyTest() {
            Assert.IsTrue(PieceType.None.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhiteBishop.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhiteKing.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhiteKnight.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhitePawn.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhiteQueen.IsBlackOrEmpty());
            Assert.IsFalse(PieceType.WhiteRook.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackBishop.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackKing.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackKnight.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackPawn.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackQueen.IsBlackOrEmpty());
            Assert.IsTrue(PieceType.BlackRook.IsBlackOrEmpty());
        }

        [Test]
        public void GetColorTest() {
            Assert.AreEqual(PieceColor.None, PieceType.None.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhiteBishop.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhiteKing.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhiteKnight.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhitePawn.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhiteQueen.GetColor());
            Assert.AreEqual(PieceColor.White, PieceType.WhiteRook.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackBishop.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackKing.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackKnight.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackPawn.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackQueen.GetColor());
            Assert.AreEqual(PieceColor.Black, PieceType.BlackRook.GetColor());
        }

        [Test]
        public void GetPromotionPieceTest() {
            Assert.AreEqual(PieceType.WhiteKnight, PieceOperations.GetPromotionPiece('N', PieceColor.White));
            Assert.AreEqual(PieceType.WhiteBishop, PieceOperations.GetPromotionPiece('B', PieceColor.White));
            Assert.AreEqual(PieceType.WhiteRook, PieceOperations.GetPromotionPiece('R', PieceColor.White));
            Assert.AreEqual(PieceType.WhiteQueen, PieceOperations.GetPromotionPiece('Q', PieceColor.White));
            Assert.AreEqual(PieceType.BlackKnight, PieceOperations.GetPromotionPiece('N', PieceColor.Black));
            Assert.AreEqual(PieceType.BlackBishop, PieceOperations.GetPromotionPiece('B', PieceColor.Black));
            Assert.AreEqual(PieceType.BlackRook, PieceOperations.GetPromotionPiece('R', PieceColor.Black));
            Assert.AreEqual(PieceType.BlackQueen, PieceOperations.GetPromotionPiece('Q', PieceColor.Black));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceFailTest() {
            PieceOperations.GetPromotionPiece('x', PieceColor.White);
        }

        [Test]
        public void GetPromotionPieceSymbolTest() {
            Assert.AreEqual("N", PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteKnight));
            Assert.AreEqual("B", PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteBishop));
            Assert.AreEqual("R", PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteRook));
            Assert.AreEqual("Q", PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteQueen));
            Assert.AreEqual("N", PieceOperations.GetPromotionPieceSymbol(PieceType.BlackKnight));
            Assert.AreEqual("B", PieceOperations.GetPromotionPieceSymbol(PieceType.BlackBishop));
            Assert.AreEqual("R", PieceOperations.GetPromotionPieceSymbol(PieceType.BlackRook));
            Assert.AreEqual("Q", PieceOperations.GetPromotionPieceSymbol(PieceType.BlackQueen));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceSymbolPawnFailTest() {
            PieceOperations.GetPromotionPieceSymbol(PieceType.WhitePawn);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceSymbolKingFailTest() {
            PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteKing);
        }

    }
}