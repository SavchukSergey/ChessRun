using ChessRun.Engine.Moves;

namespace ChessRun.Engine {
    public class MovesIterator {
        protected readonly ChessBoard _board;

        public MovesIterator(ChessBoard board) {
            _board = board;
        }

        public virtual void Handle(SpeculativeMove move) {

        }
    }
}
