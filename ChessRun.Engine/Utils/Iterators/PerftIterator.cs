using ChessRun.Engine.Moves;

namespace ChessRun.Engine.Utils.Iterators {
    public class PerftIterator : MovesIterator {
        private int _depth;

        public PerftIterator(ChessBoard board, int depth)
            : base(board) {
            _depth = depth;
        }

        public ulong CurrentMoveNodes;

        public sealed override void Handle(SpeculativeMove move) {
            if (_depth > 1) {
                _depth--;
                _board.GenerateValidMoves(this);
                _depth++;
            } else {
                CurrentMoveNodes++;
            }
        }
    }

}
