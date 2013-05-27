using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Rook {
    public abstract class WhiteRookMove : RookMove {

        protected WhiteRookMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhiteRook;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (PieceOperations.IsBlack(piece)) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

    }
}