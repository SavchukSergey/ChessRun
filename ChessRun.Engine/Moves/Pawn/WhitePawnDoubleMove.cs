using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnDoubleMove : WhitePawnMove {

        private readonly CellName _middleCell;

        public WhitePawnDoubleMove(CellName from)
            : base(from, CellOperations.IncreaseRank(from, 2)) {
            _middleCell = CellOperations.IncreaseRank(from);
        }

        public CellName MiddleCell {
            get { return _middleCell; }
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.None;
            board[To] = PieceType.WhitePawn;
            board.EnPassantMove = _middleCell;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = PieceType.None;
            board[From] = PieceType.WhitePawn;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return CellOperations.GetCellName(To);
        }

    }
}