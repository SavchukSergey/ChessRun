using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.King {
    public class WhiteKingRegularMove : WhiteKingMove {

        public WhiteKingRegularMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsBlackOrEmpty() ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhiteKing(From);
            board[To] = PieceType.WhiteKing;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.WhiteKing;
        }

    }
}