using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Knight {
    public class WhiteKnightMove : KnightMove {

        public WhiteKnightMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhiteKnight;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsBlackOrEmpty() ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhiteKnight(From);
            board[To] = PieceType.WhiteKnight;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.WhiteKnight;
        }

    }
}