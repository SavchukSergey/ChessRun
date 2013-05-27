using System.Linq;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public abstract class PawnMove : SpeculativeMove {

        protected PawnMove(CellName from, CellName to)
            : base(from, to) {

        }

        public override string CaptureSymbol { get { return ""; } }

        protected string GetCaptureNotationBody(ChessBoard board) {
            var fromRank = CellOperations.GetRank(From);
            var fromFile = CellOperations.GetFile(From);
            var toFile = CellOperations.GetFile(To);

            var moves = board.GetValidMovesList()
                .Where(move => move.Piece == Piece && CellOperations.GetFile(move.From) == fromFile && CellOperations.GetFile(move.To) == toFile)
                .Where(move => move.Promotion == Promotion)
                .ToList();
            if (moves.Count == 1) return CellOperations.GetFileSymbol(From).ToString() + CellOperations.GetFileSymbol(To);

            var byRank = moves.Where(item => CellOperations.GetRank(item.From) == fromRank).ToList();
            if (byRank.Count == 1) return CellOperations.GetFileSymbol(From).ToString() + CellOperations.GetCellName(To);
            return CellOperations.GetCellName(From) + CellOperations.GetCellName(To);
        }
    }
}