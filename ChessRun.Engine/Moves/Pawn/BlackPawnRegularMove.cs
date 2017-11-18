using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnRegularMove : BlackPawnMove {

        public BlackPawnRegularMove(CellName from)
            : base(from, @from.DecreaseRank()) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.Valid : ValidationResult.Invalid;
        }
      
        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackPawn(From);
            board.SetBlackPawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackPawn(To);
            board.SetBlackPawn(From);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return To.GetCellName();
        }

    }
}