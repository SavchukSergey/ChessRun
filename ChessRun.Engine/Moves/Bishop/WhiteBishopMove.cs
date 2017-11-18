using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Bishop {
    public class WhiteBishopMove : BishopMove {

        public WhiteBishopMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhiteBishop;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (piece.IsBlack()) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhiteBishop(From);
            board.ClearCell(To);
            board.SetWhiteBishop(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetWhiteBishop(From);
        }

    }
}