using System.Linq;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public abstract class PawnMove : SpeculativeMove {

        protected PawnMove(CellName from, CellName to)
            : base(from, to) {

        }

        public override string CaptureSymbol { get { return ""; } }

        protected string GetCaptureNotationBody(ChessBoard board) {
            var fromRank = From.GetRank();
            var fromFile = From.GetFile();
            var toFile = To.GetFile();

            var moves = board.GetValidMovesList()
                .Where(move => move.Piece == Piece && move.From.GetFile() == fromFile && move.To.GetFile() == toFile)
                .Where(move => move.Promotion == Promotion)
                .ToList();
            if (moves.Count == 1) return From.GetFileSymbol().ToString() + To.GetFileSymbol();

            var byRank = moves.Where(item => item.From.GetRank() == fromRank).ToList();
            if (byRank.Count == 1) return From.GetFileSymbol().ToString() + To.GetCellName();
            return From.GetCellName() + To.GetCellName();
        }
    }
}