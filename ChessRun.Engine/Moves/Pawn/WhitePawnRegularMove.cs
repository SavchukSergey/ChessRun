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
            board.ClearWhitePawn(From);
            board.SetWhitePawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearWhitePawn(To);
            board.SetWhitePawn(From);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return To.GetCellName();
        }

    }
}