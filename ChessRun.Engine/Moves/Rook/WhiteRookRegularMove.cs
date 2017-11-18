namespace ChessRun.Engine.Moves.Rook {
    public class WhiteRookRegularMove : WhiteRookMove {

        public WhiteRookRegularMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearWhiteRook(From);
            board.ClearCell(To);
            board.SetWhiteRook(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetWhiteRook(From);
        }
    }
}