using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnRegularMove : WhitePawnMove {

        public WhitePawnRegularMove(CellName from)
            : base(from, @from.IncreaseRank()) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.Valid : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.None;
            board[To] = PieceType.WhitePawn;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = PieceType.None;
            board[From] = PieceType.WhitePawn;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return To.GetCellName();
        }

    }
}