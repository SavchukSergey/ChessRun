using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Queen {
    public class BlackQueenMove : QueenMove {

        public BlackQueenMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.BlackQueen;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            if (piece == PieceType.None) return ValidationResult.Valid;
            if (piece.IsWhite()) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearBlackQueen(From);
            board.ClearCell(To);
            board.SetBlackQueen(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetBlackQueen(From);
        }

    }
}