using System;
using ChessRun.Engine.Moves;

namespace ChessRun.Engine.Utils.Iterators {
    public class DelegateIterator : MovesIterator {
        private readonly Action<SpeculativeMove> _handler;

        public DelegateIterator(ChessBoard board, Action<SpeculativeMove> handler)
            : base(board) {
            _handler = handler;
        }

        public override void Handle(SpeculativeMove move) {
            _handler(move);
        }
    }
}
