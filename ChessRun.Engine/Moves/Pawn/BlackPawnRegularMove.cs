using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnRegularMove : BlackPawnMove {

        public BlackPawnRegularMove(CellName from)
            : base(from, CellOperations.DecreaseRank(from)) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.Valid : ValidationResult.Invalid;
        }
      
        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.None;
            board[To] = PieceType.BlackPawn;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = PieceType.None;
            board[From] = PieceType.BlackPawn;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return CellOperations.GetCellName(To);
        }

    }
}