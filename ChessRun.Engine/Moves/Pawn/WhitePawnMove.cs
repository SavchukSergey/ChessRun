namespace ChessRun.Engine.Moves.Pawn {
    public abstract class WhitePawnMove : PawnMove {

        protected WhitePawnMove(CellName from, CellName to)
            : base(from, to) {
            Piece = PieceType.WhitePawn;
        }

        protected override string NotationSymbol {
            get { return ""; }
        }

    }
}