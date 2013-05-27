namespace ChessRun.Engine.Moves.King {
    public abstract class BlackKingMove : KingMove {

        protected BlackKingMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.BlackKing;
        }

    }
}