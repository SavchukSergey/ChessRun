using System;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Utils {
    [TestClass]
    public class PieceOperationsTest : BaseTestFixture {

        [TestMethod]
        public void GetPawnTest() {
            Assert.AreEqual(PieceType.WhitePawn, PieceOperations.GetPawn(PieceColor.White));
            Assert.AreEqual(PieceType.BlackPawn, PieceOperations.GetPawn(PieceColor.Black));
            try {
                PieceOperations.GetPawn(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void GetKnightTest() {
            Assert.AreEqual(PieceType.WhiteKnight, PieceOperations.GetKnight(PieceColor.White));
            Assert.AreEqual(PieceType.BlackKnight, PieceOperations.GetKnight(PieceColor.Black));
            try {
                PieceOperations.GetKnight(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void GetBishopTest() {
            Assert.AreEqual(PieceType.WhiteBishop, PieceOperations.GetBishop(PieceColor.White));
            Assert.AreEqual(PieceType.BlackBishop, PieceOperations.GetBishop(PieceColor.Black));
            try {
                PieceOperations.GetBishop(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void GetRookTest() {
            Assert.AreEqual(PieceType.WhiteRook, PieceOperations.GetRook(PieceColor.White));
            Assert.AreEqual(PieceType.BlackRook, PieceOperations.GetRook(PieceColor.Black));
            try {
                PieceOperations.GetRook(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void GetQueenTest() {
            Assert.AreEqual(PieceType.WhiteQueen, PieceOperations.GetQueen(PieceColor.White));
            Assert.AreEqual(PieceType.BlackQueen, PieceOperations.GetQueen(PieceColor.Black));
            try {
                PieceOperations.GetQueen(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void GetKingTest() {
            Assert.AreEqual(PieceType.WhiteKing, PieceOperations.GetKing(PieceColor.White));
            Assert.AreEqual(PieceType.BlackKing, PieceOperations.GetKing(PieceColor.Black));
            try {
                PieceOperations.GetKing(PieceColor.None);
                Assert.Fail();
            } catch (InvalidOperationException) {

            }
        }

        [TestMethod]
        public void IsWhiteTest() {
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.None));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhiteBishop));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhiteKing));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhiteKnight));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhitePawn));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhiteQueen));
            Assert.IsTrue(PieceOperations.IsWhite(PieceType.WhiteRook));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackBishop));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackKing));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackKnight));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackPawn));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackQueen));
            Assert.IsFalse(PieceOperations.IsWhite(PieceType.BlackRook));
        }

        [TestMethod]
        public void IsBlackTest() {
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.None));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhiteBishop));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhiteKing));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhiteKnight));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhitePawn));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhiteQueen));
            Assert.IsFalse(PieceOperations.IsBlack(PieceType.WhiteRook));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackBishop));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackKing));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackKnight));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackPawn));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackQueen));
            Assert.IsTrue(PieceOperations.IsBlack(PieceType.BlackRook));
        }

        [TestMethod]
        public void GetColorTest() {
            Assert.AreEqual(PieceColor.None, PieceOperations.GetColor(PieceType.None));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhiteBishop));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhiteKing));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhiteKnight));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhitePawn));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhiteQueen));
            Assert.AreEqual(PieceColor.White, PieceOperations.GetColor(PieceType.WhiteRook));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackBishop));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackKing));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackKnight));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackPawn));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackQueen));
            Assert.AreEqual(PieceColor.Black, PieceOperations.GetColor(PieceType.BlackRook));
        }

        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceFailTest() {
            PieceOperations.GetPromotionPiece('x', PieceColor.White);
        }

        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceSymbolPawnFailTest() {
            PieceOperations.GetPromotionPieceSymbol(PieceType.WhitePawn);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPromotionPieceSymbolKingFailTest() {
            PieceOperations.GetPromotionPieceSymbol(PieceType.WhiteKing);
        }

    }
}