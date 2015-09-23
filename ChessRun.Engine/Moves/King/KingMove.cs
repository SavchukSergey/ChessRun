using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.King {
    public abstract class KingMove : SpeculativeMove {

        protected KingMove(CellName from, CellName to)
            : base(from, to) {
        }

        protected override string GetNotationBody(ChessBoard board) {
            return NotationSymbol + To.GetCellName();
        }

        protected override string NotationSymbol => "K";
    }
}