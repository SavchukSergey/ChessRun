using System;

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
                        _engine.Divide(depth);
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

        public static void Perft(int depth) {
            var started = DateTime.Now;
            ulong nodes = _engine.Perft(depth);
            var ended = DateTime.Now;
            var time = (ended - started).TotalSeconds;
            Console.WriteLine("Time: {0}", time);
            Console.WriteLine("Nodes: {0}", nodes);
            Console.WriteLine("Speed: {0}", time != 0 ? Math.Round(nodes / 1000.0 / time, 2) + "kN/Sec" : "Time is too small");
        }

    }
}
