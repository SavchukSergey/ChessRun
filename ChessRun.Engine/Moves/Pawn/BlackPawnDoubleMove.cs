using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnDoubleMove : BlackPawnMove {

        private readonly CellName _middleCell;

        public BlackPawnDoubleMove(CellName from)
            : base(from, CellOperations.DecreaseRank(from, 2)) {
            _middleCell = CellOperations.DecreaseRank(from);
        }

        public CellName MiddleCell {
            get { return _middleCell; }
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.None;
            board[To] = PieceType.BlackPawn;
            board.EnPassantMove = _middleCell;
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