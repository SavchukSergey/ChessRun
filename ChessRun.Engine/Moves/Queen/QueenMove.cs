namespace ChessRun.Engine.Moves.Queen {
    public abstract class QueenMove : SpeculativeMove {

        protected QueenMove(CellName from, CellName to)
            : base(from, to) {
        }

        protected override string NotationSymbol => "Q";
    }
}