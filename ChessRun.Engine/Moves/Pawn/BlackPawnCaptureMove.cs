using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnCaptureMove : BlackPawnMove {

        public BlackPawnCaptureMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsWhite() ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearBlackPawn(From);
            board.ClearCell(To);
            board.SetBlackPawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetBlackPawn(From);
        }

        public override bool IsCapture(ChessBoard board) => true;

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }
    }
}