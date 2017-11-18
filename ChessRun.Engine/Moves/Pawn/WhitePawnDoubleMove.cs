using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnDoubleMove : WhitePawnMove {

        private readonly CellName _middleCell;

        public WhitePawnDoubleMove(CellName from)
            : base(from, @from.IncreaseRank(2)) {
            _middleCell = @from.IncreaseRank();
        }

        public CellName MiddleCell {
            get { return _middleCell; }
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearWhitePawn(From);
            board.SetWhitePawn(To);
            board.EnPassantMove = _middleCell;
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