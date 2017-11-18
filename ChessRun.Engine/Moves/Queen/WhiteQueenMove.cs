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
            if (piece.IsBlack()) return ValidationResult.ValidAndStop;
            return ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhiteQueen(From);
            board.ClearCell(To);
            board.SetWhiteQueen(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetWhiteQueen(From);
        }

    }
}