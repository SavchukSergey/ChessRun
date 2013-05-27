using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests {
    [TestClass]
    public class AlgebraicMoveTest : BaseTestFixture {

        [TestMethod]
        public void WhiteQueenTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qe6", PieceColor.White, out move);

            Assert.AreEqual(CellFile.None, move.HintFileFrom, "Hint is not specified in move");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "Hint is not specified in move");

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.WhiteQueen, move.Piece, "Piece should white queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [TestMethod]
        public void BlackQueenTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qe6", PieceColor.Black, out move);

            Assert.AreEqual(CellFile.None, move.HintFileFrom, "Hint is not specified in move");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "Hint is not specified in move");

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.BlackQueen, move.Piece, "Piece should black queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [TestMethod]
        public void CheckTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qe6+", PieceColor.Black, out move);
            Assert.IsTrue(move.Check, "Is Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [TestMethod]
        public void CheckmateTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qe6#", PieceColor.Black, out move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsTrue(move.Checkmate, "Is Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [TestMethod]
        public void CaptureTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qxe6", PieceColor.Black, out move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [TestMethod]
        public void CaptureAndCheckTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qxe6+", PieceColor.Black, out move);
            Assert.IsTrue(move.Check, "Is Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [TestMethod]
        public void CaptureAndCheckmateTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qxe6#", PieceColor.Black, out move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsTrue(move.Checkmate, "Is Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [TestMethod]
        public void WhiteShortCastleCheck() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("O-O+", PieceColor.White, out move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsTrue(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellOperations.GetFile(CellName.E1));
            Assert.AreEqual(move.HintRankFrom, CellOperations.GetRank(CellName.E1));

            Assert.AreEqual(move.To, CellName.G1);
            Assert.AreEqual(move.Piece, PieceType.WhiteKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsFalse(move.IsLongCastle, "IsLongCastle");
            Assert.IsTrue(move.IsShortCastle, "IsShortCastle");
        }

        [TestMethod]
        public void BlackShortCastleCheck() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("O-O#", PieceColor.Black, out move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsFalse(move.Check, "Check");
            Assert.IsTrue(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellOperations.GetFile(CellName.E8));
            Assert.AreEqual(move.HintRankFrom, CellOperations.GetRank(CellName.E8));

            Assert.AreEqual(move.To, CellName.G8);
            Assert.AreEqual(move.Piece, PieceType.BlackKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsFalse(move.IsLongCastle, "IsLongCastle");
            Assert.IsTrue(move.IsShortCastle, "IsShortCastle");
        }

        [TestMethod]
        public void WhiteLongCastleCheck() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("O-O-O#", PieceColor.White, out move);
            Assert.IsFalse(move.Capture);
            Assert.IsFalse(move.Check);
            Assert.IsTrue(move.Checkmate);

            Assert.AreEqual(move.HintFileFrom, CellOperations.GetFile(CellName.E1));
            Assert.AreEqual(move.HintRankFrom, CellOperations.GetRank(CellName.E1));

            Assert.AreEqual(move.To, CellName.C1);
            Assert.AreEqual(move.Piece, PieceType.WhiteKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsTrue(move.IsLongCastle);
            Assert.IsFalse(move.IsShortCastle);
        }

        [TestMethod]
        public void BlackLongCastleCheck() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("O-O-O+", PieceColor.Black, out move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsTrue(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellOperations.GetFile(CellName.E8));
            Assert.AreEqual(move.HintRankFrom, CellOperations.GetRank(CellName.E8));

            Assert.AreEqual(move.To, CellName.C8);
            Assert.AreEqual(move.Piece, PieceType.BlackKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsTrue(move.IsLongCastle);
            Assert.IsFalse(move.IsShortCastle);
        }

        [TestMethod]
        public void WhitePawnPromotionCaptureCheckmateTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("ed=Q#", PieceColor.White, out move);
            Assert.AreEqual(PieceType.WhitePawn, move.Piece);
            Assert.AreEqual(true, move.Capture, "Capture");
            Assert.AreEqual(true, move.Checkmate, "Checkmate");
            Assert.AreEqual(false, move.Check, "Check");
            Assert.AreEqual(CellName.E7, move.From, "From");
            Assert.AreEqual(CellName.D8, move.To, "To");
            Assert.AreEqual(PieceType.WhiteQueen, move.Promotion);
        }

        [TestMethod]
        public void BlackPawnPromotionCaptureCheckmateTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("ed=Q#", PieceColor.Black, out move);
            Assert.AreEqual(PieceType.BlackPawn, move.Piece);
            Assert.AreEqual(true, move.Capture, "Capture");
            Assert.AreEqual(true, move.Checkmate, "Checkmate");
            Assert.AreEqual(false, move.Check, "Check");
            Assert.AreEqual(CellName.E2, move.From, "From");
            Assert.AreEqual(CellName.D1, move.To, "To");
            Assert.AreEqual(PieceType.BlackQueen, move.Promotion);
        }

        [TestMethod]
        public void FileRankCaptureFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qg8xe6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.WhiteQueen, move.Piece, "Piece should white queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellName.G8, move.From, "Checking destination");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void FileRankFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Bg8e6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Bishop move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Bishop move is not a castle");

            Assert.AreEqual(PieceType.WhiteBishop, move.Piece, "Piece should white bishop");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for bishop");
            Assert.AreEqual(CellName.G8, move.From, "Checking destination");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsFalse(move.Capture, "Capture");
        }

        [TestMethod]
        public void FileCaptureFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qgxe6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.WhiteQueen, move.Piece, "Piece should white queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellFile.G, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void FileFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Bge6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Bishop move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Bishop move is not a castle");

            Assert.AreEqual(PieceType.WhiteBishop, move.Piece, "Piece should white bishop");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for bishop");
            Assert.AreEqual(CellFile.G, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsFalse(move.Capture, "Capture");
        }

        [TestMethod]
        public void RankCaptureFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Q8xe6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.WhiteQueen, move.Piece, "Piece should white queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellFile.None, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.R8, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void RankFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("B8e6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Bishop move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Bishop move is not a castle");

            Assert.AreEqual(PieceType.WhiteBishop, move.Piece, "Piece should white bishop");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for bishop");
            Assert.AreEqual(CellFile.None, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.R8, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsFalse(move.Capture, "Capture");
        }

        [TestMethod]
        public void CaptureFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Qxe6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Queen move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Queen move is not a castle");

            Assert.AreEqual(PieceType.WhiteQueen, move.Piece, "Piece should white queen");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for queen");
            Assert.AreEqual(CellFile.None, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void FileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("Be6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Bishop move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Bishop move is not a castle");

            Assert.AreEqual(PieceType.WhiteBishop, move.Piece, "Piece should white bishop");
            Assert.AreEqual(PieceType.None, move.Promotion, "No promotion for bishop");
            Assert.AreEqual(CellFile.None, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsFalse(move.Capture, "Capture");
        }

        [TestMethod]
        public void PawnFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("e6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Pawn move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Pawn move is not a castle");

            Assert.AreEqual(PieceType.WhitePawn, move.Piece, "Piece should white pawn");
            Assert.AreEqual(PieceType.None, move.Promotion, "Promotion");
            Assert.AreEqual(CellFile.E, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "Checking destination");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsFalse(move.Capture, "Capture");
        }

        [TestMethod]
        public void PawnFileFileTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("de", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Pawn move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Pawn move is not a castle");

            Assert.AreEqual(PieceType.WhitePawn, move.Piece, "Piece should white pawn");
            Assert.AreEqual(PieceType.None, move.Promotion, "Promotion");
            Assert.AreEqual(CellFile.D, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellFile.E, move.HintFileTo, "HintFileTo");
            Assert.AreEqual(CellRank.None, move.HintRankTo, "HintRankTo");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void PawnFileCaptureFileTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("dxe", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Pawn move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Pawn move is not a castle");

            Assert.AreEqual(PieceType.WhitePawn, move.Piece, "Piece should white pawn");
            Assert.AreEqual(PieceType.None, move.Promotion, "Promotion");
            Assert.AreEqual(CellFile.D, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellFile.E, move.HintFileTo, "HintFileTo");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }
        [TestMethod]
        public void PawnFileFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("de6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Pawn move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Pawn move is not a castle");

            Assert.AreEqual(PieceType.WhitePawn, move.Piece, "Piece should white pawn");
            Assert.AreEqual(PieceType.None, move.Promotion, "Promotion");
            Assert.AreEqual(CellFile.D, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "To");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

        [TestMethod]
        public void PawnFileCaptureFileRankTest() {
            AlgebraicMove move;
            AlgebraicMove.ParseMove("dxe6", PieceColor.White, out move);

            Assert.AreEqual(false, move.IsLongCastle, "Pawn move is not a castle");
            Assert.AreEqual(false, move.IsShortCastle, "Pawn move is not a castle");

            Assert.AreEqual(PieceType.WhitePawn, move.Piece, "Piece should white pawn");
            Assert.AreEqual(PieceType.None, move.Promotion, "Promotion");
            Assert.AreEqual(CellFile.D, move.HintFileFrom, "HintFileFrom");
            Assert.AreEqual(CellRank.None, move.HintRankFrom, "HintRankFrom");
            Assert.AreEqual(CellName.E6, move.To, "To");

            Assert.IsFalse(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");
            Assert.IsTrue(move.Capture, "Capture");
        }

    }
}
