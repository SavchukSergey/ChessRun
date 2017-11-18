using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Bishop {
    public class BlackBishopMove : BishopMove {

        public BlackBishopMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.BlackBishop;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (piece.IsWhite()) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearBlackBishop(From);
            board.ClearCell(To);
            board.SetBlackBishop(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetBlackBishop(From);
        }

    }
}