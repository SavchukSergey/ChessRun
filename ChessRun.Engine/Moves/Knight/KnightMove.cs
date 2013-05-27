namespace ChessRun.Engine.Moves.Knight {
    public abstract class KnightMove : SpeculativeMove {

        protected KnightMove(CellName from, CellName to)
            : base(from, to) {
        }

        protected override string NotationSymbol {
            get { return "N"; }
        }

    }
}