using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Rook {
    public abstract class BlackRookMove : RookMove {

        protected BlackRookMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.BlackRook;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (piece.IsWhite()) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

    }
}