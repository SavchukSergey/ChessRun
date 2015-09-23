using System.Linq;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Moves.King;
using ChessRun.Engine.Utils;
using NUnit.Framework;

namespace ChessRun.Engine.Tests.Moves.King {
    public class BlackKingRegularMoveTest : BaseTestFixture {

        [Test]
        public void FastValidateTest() {
            var move = new BlackKingRegularMove(CellName.A1, CellName.B2);

            var board = new ChessBoard {
                [move.From] = PieceType.BlackKing,
                [move.To] = PieceType.WhiteKnight
            };

            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.None;
            Assert.AreEqual(ValidationResult.ValidAndStop, move.FastValidate(board));

            board[move.To] = PieceType.BlackKnight;
            Assert.AreEqual(ValidationResult.Invalid, move.FastValidate(board));
        }

        [Test]
        public void ToShortNotationTest() {
            var board = new ChessBoard();
            FEN.Setup(board, "rnb1k1nr/ppp3pp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq");
            var move = board.GetValidMoves(PieceType.BlackKing, CellName.E8, CellName.D8).FirstOrDefault();
            Assert.IsNotNull(move, "Move cannot be null");
            Assert.IsTrue(move is BlackKingRegularMove);
            var notation = move.ToShortNotation(board);
            Assert.AreEqual(notation, "Kd8");
        }

    }
}
