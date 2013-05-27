using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.King {
    public class BlackKingRegularMove : BlackKingMove {

        public BlackKingRegularMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece == PieceType.None || PieceOperations.IsWhite(piece) ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = PieceType.BlackKing;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.BlackKing;
        }

    }
}