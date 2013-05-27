using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnCaptureMove : BlackPawnMove {

        public BlackPawnCaptureMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return PieceOperations.IsWhite(piece) ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = PieceType.BlackPawn;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.BlackPawn;
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }
    }
}