using System;
using System.Diagnostics;

namespace ChessRun.Engine {
    class Program {

        private static readonly ChessEngineApi _engine = new ChessEngineApiM();

        static void Main() {
            while (true) {
                var line = Console.ReadLine();
                if (line == null) break;
                var lineArgs = line.Split(' ');
                var command = lineArgs[0];
                int depth;
                switch (command) {
                    case "new":
                        _engine.New();
                        break;
                    case "setboard":
                        var fen = line.Substring(9).Trim();
                        _engine.SetBoard(fen);
                        break;
                    case "divide":
                        depth = int.Parse(lineArgs[1]);
                        Divide(depth);
                        break;
                    case "perft":
                        depth = int.Parse(lineArgs[1]);
                        Perft(depth);
                        break;
                    case "quit":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void Divide(int depth) {
            var watch = new Stopwatch();
            watch.Start();
            var nodes = _engine.Divide(depth);
            watch.Stop();
            var time = watch.Elapsed;
            var ts = time.TotalSeconds;
            Console.WriteLine("Time: {0} sec", ts);
            Console.WriteLine("Nodes: {0}", nodes);
            Console.WriteLine("Speed: {0}", ts != 0 ? Math.Round(nodes / 1000.0 / ts, 2) + "kN/Sec" : "Time is too small");

        }

        public static void Perft(int depth) {
            var watch = new Stopwatch();
            watch.Start();
            var nodes = _engine.Perft(depth);
            var time = watch.Elapsed;
            var ts = time.TotalSeconds;
            Console.WriteLine("Time: {0} sec", ts);
            Console.WriteLine("Nodes: {0}", nodes);
            Console.WriteLine("Speed: {0}", ts != 0 ? Math.Round(nodes / 1000.0 / ts, 2) + "kN/Sec" : "Time is too small");
        }

    }
}
