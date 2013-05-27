using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.King;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessRun.Engine.Tests.Moves.King {
    [TestClass]
    public class WhiteKingRegularMoveTest : BaseTestFixture {

        [TestMethod]
        public void FastValidateTest() {
            var move = new WhiteKingRegularMove(CellName.A1, CellName.B2);

            var board = new ChessBoard();
            board[move.From] = PieceType.WhiteKing;

            board[move.To] = PieceType.BlackKnight;
            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.WhiteKnight;
            Assert.AreEqual(ValidationResult.Invalid, move.FastValidate(board));
        }

        [TestMethod]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnbqkbnr/pppppppp/8/8/8/8/PPP3PP/RNB1K1NR w KQkq");
            var move = board.GetValidMoves(PieceType.WhiteKing, CellName.E1, CellName.D1).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is WhiteKingRegularMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "Kd1");
        }
    }
}
