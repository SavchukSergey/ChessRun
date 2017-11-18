using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests {
    [TestOf(typeof(AlgebraicMoveTest))]
    public class AlgebraicMoveTest : BaseTestFixture {

        [Test]
        public void WhiteQueenTest() {
            AlgebraicMove.ParseMove("Qe6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void BlackQueenTest() {
            AlgebraicMove.ParseMove("Qe6", PieceColor.Black, out AlgebraicMove move);

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

        [Test]
        public void CheckTest() {
            AlgebraicMove.ParseMove("Qe6+", PieceColor.Black, out AlgebraicMove move);
            Assert.IsTrue(move.Check, "Is Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [Test]
        public void CheckmateTest() {
            AlgebraicMove.ParseMove("Qe6#", PieceColor.Black, out AlgebraicMove move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsTrue(move.Checkmate, "Is Checkmate");
            Assert.IsFalse(move.Capture, "Not a Capture");
        }

        [Test]
        public void CaptureTest() {
            AlgebraicMove.ParseMove("Qxe6", PieceColor.Black, out AlgebraicMove move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [Test]
        public void CaptureAndCheckTest() {
            AlgebraicMove.ParseMove("Qxe6+", PieceColor.Black, out AlgebraicMove move);
            Assert.IsTrue(move.Check, "Is Check");
            Assert.IsFalse(move.Checkmate, "Not a Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [Test]
        public void CaptureAndCheckmateTest() {
            AlgebraicMove.ParseMove("Qxe6#", PieceColor.Black, out AlgebraicMove move);
            Assert.IsFalse(move.Check, "Not a Check");
            Assert.IsTrue(move.Checkmate, "Is Checkmate");
            Assert.IsTrue(move.Capture, "Is Capture");
        }

        [Test]
        public void WhiteShortCastleCheck() {
            AlgebraicMove.ParseMove("O-O+", PieceColor.White, out AlgebraicMove move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsTrue(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellName.E1.GetFile());
            Assert.AreEqual(move.HintRankFrom, CellName.E1.GetRank());

            Assert.AreEqual(move.To, CellName.G1);
            Assert.AreEqual(move.Piece, PieceType.WhiteKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsFalse(move.IsLongCastle, "IsLongCastle");
            Assert.IsTrue(move.IsShortCastle, "IsShortCastle");
        }

        [Test]
        public void BlackShortCastleCheck() {
            AlgebraicMove.ParseMove("O-O#", PieceColor.Black, out AlgebraicMove move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsFalse(move.Check, "Check");
            Assert.IsTrue(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellName.E8.GetFile());
            Assert.AreEqual(move.HintRankFrom, CellName.E8.GetRank());

            Assert.AreEqual(move.To, CellName.G8);
            Assert.AreEqual(move.Piece, PieceType.BlackKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsFalse(move.IsLongCastle, "IsLongCastle");
            Assert.IsTrue(move.IsShortCastle, "IsShortCastle");
        }

        [Test]
        public void WhiteLongCastleCheck() {
            AlgebraicMove.ParseMove("O-O-O#", PieceColor.White, out AlgebraicMove move);
            Assert.IsFalse(move.Capture);
            Assert.IsFalse(move.Check);
            Assert.IsTrue(move.Checkmate);

            Assert.AreEqual(move.HintFileFrom, CellName.E1.GetFile());
            Assert.AreEqual(move.HintRankFrom, CellName.E1.GetRank());

            Assert.AreEqual(move.To, CellName.C1);
            Assert.AreEqual(move.Piece, PieceType.WhiteKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsTrue(move.IsLongCastle);
            Assert.IsFalse(move.IsShortCastle);
        }

        [Test]
        public void BlackLongCastleCheck() {
            AlgebraicMove.ParseMove("O-O-O+", PieceColor.Black, out AlgebraicMove move);
            Assert.IsFalse(move.Capture, "Capture");
            Assert.IsTrue(move.Check, "Check");
            Assert.IsFalse(move.Checkmate, "Checkmate");

            Assert.AreEqual(move.HintFileFrom, CellName.E8.GetFile());
            Assert.AreEqual(move.HintRankFrom, CellName.E8.GetRank());

            Assert.AreEqual(move.To, CellName.C8);
            Assert.AreEqual(move.Piece, PieceType.BlackKing);
            Assert.AreEqual(move.Promotion, PieceType.None);

            Assert.IsTrue(move.IsLongCastle);
            Assert.IsFalse(move.IsShortCastle);
        }

        [Test]
        public void WhitePawnPromotionCaptureCheckmateTest() {
            AlgebraicMove.ParseMove("ed=Q#", PieceColor.White, out AlgebraicMove move);
            Assert.AreEqual(PieceType.WhitePawn, move.Piece);
            Assert.AreEqual(true, move.Capture, "Capture");
            Assert.AreEqual(true, move.Checkmate, "Checkmate");
            Assert.AreEqual(false, move.Check, "Check");
            Assert.AreEqual(CellName.E7, move.From, "From");
            Assert.AreEqual(CellName.D8, move.To, "To");
            Assert.AreEqual(PieceType.WhiteQueen, move.Promotion);
        }

        [Test]
        public void BlackPawnPromotionCaptureCheckmateTest() {
            AlgebraicMove.ParseMove("ed=Q#", PieceColor.Black, out AlgebraicMove move);
            Assert.AreEqual(PieceType.BlackPawn, move.Piece);
            Assert.AreEqual(true, move.Capture, "Capture");
            Assert.AreEqual(true, move.Checkmate, "Checkmate");
            Assert.AreEqual(false, move.Check, "Check");
            Assert.AreEqual(CellName.E2, move.From, "From");
            Assert.AreEqual(CellName.D1, move.To, "To");
            Assert.AreEqual(PieceType.BlackQueen, move.Promotion);
        }

        [Test]
        public void FileRankCaptureFileRankTest() {
            AlgebraicMove.ParseMove("Qg8xe6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void FileRankFileRankTest() {
            AlgebraicMove.ParseMove("Bg8e6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void FileCaptureFileRankTest() {
            AlgebraicMove.ParseMove("Qgxe6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void FileFileRankTest() {
            AlgebraicMove.ParseMove("Bge6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void RankCaptureFileRankTest() {
            AlgebraicMove.ParseMove("Q8xe6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void RankFileRankTest() {
            AlgebraicMove.ParseMove("B8e6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void CaptureFileRankTest() {
            AlgebraicMove.ParseMove("Qxe6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void FileRankTest() {
            AlgebraicMove.ParseMove("Be6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void PawnFileRankTest() {
            AlgebraicMove.ParseMove("e6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void PawnFileFileTest() {
            AlgebraicMove.ParseMove("de", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void PawnFileCaptureFileTest() {
            AlgebraicMove.ParseMove("dxe", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void PawnFileFileRankTest() {
            AlgebraicMove.ParseMove("de6", PieceColor.White, out AlgebraicMove move);

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

        [Test]
        public void PawnFileCaptureFileRankTest() {
            AlgebraicMove.ParseMove("dxe6", PieceColor.White, out AlgebraicMove move);

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
