using System;
using ChessRun.Engine.Moves;

namespace ChessRun.Engine.Utils.Iterators {
    public class DivideIterator : MovesIterator {

        private int _depth;
        private readonly int _originalDepth;
        private ulong _currentMoveNodes;
        public ulong TotalMoveNodes;

        public DivideIterator(ChessBoard board, int depth)
            : base(board) {
            _depth = depth;
            _originalDepth = _depth;
        }


        public sealed override void Handle(SpeculativeMove move) {
            if (_depth == _originalDepth) {
                var sn = move.From.ToString().ToLower() + move.To.ToString().ToLower();
                Console.Write(sn + " ");
                _currentMoveNodes = 0;
            }
            if (_depth > 1) {
                _depth--;
                _board.GenerateValidMoves(this);
                _depth++;
            } else {
                _currentMoveNodes++;
                TotalMoveNodes++;
            }
            if (_depth == _originalDepth) {
                Console.WriteLine(_currentMoveNodes);
            }

        }

    }

}
