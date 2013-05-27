using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Queen {
    public class WhiteQueenMove : QueenMove {

        public WhiteQueenMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhiteQueen;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (PieceOperations.IsBlack(piece)) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = PieceType.WhiteQueen;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.WhiteQueen;
        }

    }
}