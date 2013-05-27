namespace ChessRun.Engine.Moves.King {
    public abstract class WhiteKingMove : KingMove {

        protected WhiteKingMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhiteKing;
        }

    }
}