namespace ChessRun.Engine.Moves.Pawn {
    public abstract class BlackPawnMove : PawnMove {

        protected BlackPawnMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.BlackPawn;
        }

        protected override string NotationSymbol {
            get { return ""; }
        }
    }
}