using System;
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
            var started = DateTime.Now;

            var iterator = new DivideIterator(_board, depth);

            _board.GenerateValidMoves(iterator);

            var ended = DateTime.Now;
            var time = (ended - started).TotalSeconds;
            Console.WriteLine("Time: {0}", time);
            Console.WriteLine("Nodes: {0}", iterator.TotalMoveNodes);
            Console.WriteLine("Speed: {0}", time != 0 ? Math.Round(iterator.TotalMoveNodes / 1000.0 / time, 2) + "kN/Sec" : "Time is too small");
            return iterator.TotalMoveNodes;
        }

    }
}
