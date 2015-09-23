namespace ChessRun.Engine.Moves.Rook {
    public abstract class RookMove : SpeculativeMove {

        protected RookMove(CellName from, CellName to)
            : base(from, to) {
        }

        protected override string NotationSymbol => "R";
    }
}