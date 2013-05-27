using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Chess.Engine.Diagnostics {
    public class EngineClient {

        public class DivideItem {
            public string Move;
            public int Nodes;

            public override string ToString() {
                return Move + " " + Nodes;
            }
        }

        private readonly Process _process;
        private readonly MemoryStream _outputStream = new MemoryStream();
        private readonly StreamReader _outputReader;
        private readonly object _sync = new object();

        public EngineClient(Process process) {
            _process = process;
            _outputReader = new StreamReader(_outputStream);
            Thread thread = new Thread(ReadingThreadLoop);
            thread.Start();
        }

        public void ReadingThreadLoop() {
            var writer = new StreamWriter(_outputStream);
            long writePos = 0;
            while (true) {
                var line = _process.StandardOutput.ReadLine();
                lock (_sync) {
                    var pos = _outputStream.Position;
                    _outputStream.Position = writePos;
                    writer.WriteLine(line);
                    writer.Flush();
                    writePos = _outputStream.Position;
                    _outputStream.Position = pos;
                }
            }
        }

        public void Init() {
            Skip();
        }

        public void SetBoard(string fen) {
            _process.StandardInput.WriteLine("setboard " + fen);
        }

        public void ResetBoard() {
            _process.StandardInput.WriteLine("new");
        }

        public IList<DivideItem> Divide(int depth) {
            _process.StandardInput.WriteLine("divide " + depth);
            Thread.Sleep(2000);
            IList<DivideItem> moves = new List<DivideItem>();
            bool end = false;
            string line;
            do {
                lock (_sync) {
                    line = _outputReader.ReadLine();
                }
                if (line == null) continue;
                string[] args = line.Split(' ');
                if (!line.Contains(":") && args.Length == 2) {
                    var divideItem = new DivideItem();
                    divideItem.Move = args[0];
                    divideItem.Nodes = int.Parse(args[1]);
                    moves.Add(divideItem);
                } else {
                    end = true;
                }
            } while (!end);
            Skip();
            return moves;
        }

        private void Skip() {
            var start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < 1000) {
                while (_outputReader.Peek() >= 0) {
                    _outputReader.Read();
                }
                Thread.Sleep(100);
            }
        }

    }
}
