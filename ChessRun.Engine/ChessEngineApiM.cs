using System;
using System.Collections.Generic;
using System.Threading;
using ChessRun.Engine.Utils.Iterators;

namespace ChessRun.Engine {
    public class ChessEngineApiM : ChessEngineApi, IDisposable {

        private readonly IList<Thread> _threads = new List<Thread>();

        private readonly Queue<Action> _actions = new Queue<Action>();
        private readonly object _sync = new object();
        private readonly Semaphore _semaphore = new Semaphore(0, int.MaxValue);
        private bool _active = true;

        public ChessEngineApiM() {
            for (var i = 0; i < 4; i++) {
                var thread = new Thread(ThreadEntry);
                thread.Start();
                _threads.Add(thread);
            }
        }

        public override ulong Perft(int depth) {
            if (depth <= 4) {
                return base.Perft(depth);
            }
            long nodes = 0;
            int topLevelNodes = 0;
            var done = new Semaphore(0, int.MaxValue);
            var iterator = new DelegateIterator(_board, move => {
                var clonedBoard = _board.Clone();
                Enqueue(() => {
                    if (depth > 1) {
                        var perftIterator = new PerftIterator(clonedBoard, depth - 1);
                        clonedBoard.GenerateValidMoves(perftIterator);
                        Interlocked.Add(ref nodes, (long)perftIterator.CurrentMoveNodes);
                    }
                    done.Release(1);
                });
                topLevelNodes++;
            });
            _board.GenerateValidMoves(iterator);
            for (var i = 0; i < topLevelNodes; i++) {
                done.WaitOne();
            }
            return (ulong)nodes;
        }

        private void ThreadEntry() {
            while (_active) {
                _semaphore.WaitOne();
                Action action;
                lock (_sync) {
                    action = _actions.Dequeue();
                }
                action();
            }
        }

        private void Enqueue(Action action) {
            lock (_sync) {
                _actions.Enqueue(action);
            }
            _semaphore.Release(1);
        }

        public void Dispose() {
            _active = false;
        }
    }
}
