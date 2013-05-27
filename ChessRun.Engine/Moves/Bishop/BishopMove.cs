namespace ChessRun.Engine.Moves.Bishop {
    public abstract class BishopMove : SpeculativeMove {

        protected BishopMove(CellName from, CellName to)
            : base(from, to) {
        }

        protected override string NotationSymbol {
            get { return "B"; }
        }
    }
}