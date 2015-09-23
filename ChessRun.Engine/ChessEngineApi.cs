using System;
using System.Diagnostics;
using ChessRun.Engine.Utils;
using ChessRun.Engine.Utils.Iterators;

namespace ChessRun.Engine {
    public class ChessEngineApi {

        protected readonly ChessBoard _board = new ChessBoard();

        public ChessEngineApi() {
            New();
        }

        public void New() {
            FEN.Setup(_board, FEN.INITIAL_POSITION);
        }

        public void SetBoard(string fen) {
            FEN.Setup(_board, fen);
        }

        public virtual ulong Perft(int depth) {
            var iterator = new PerftIterator(_board, depth);
            _board.GenerateValidMoves(iterator);
            return iterator.CurrentMoveNodes;
        }

        public ulong Divide(int depth) {
            var iterator = new DivideIterator(_board, depth);
            _board.GenerateValidMoves(iterator);
            return iterator.TotalMoveNodes;
        }

    }
}
