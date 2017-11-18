using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnCaptureMove : WhitePawnMove {

        public WhitePawnCaptureMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsBlack() ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhitePawn(From);
            board.ClearCell(To);
            board.SetWhitePawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetWhitePawn(From);
        }

        public override bool IsCapture(ChessBoard board) => true;

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }

    }
}