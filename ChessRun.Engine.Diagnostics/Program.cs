using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Chess.Engine.Diagnostics;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Diagnostics {
    class Program {

        private static EngineClient _testClient;
        private static EngineClient _etalonClient;

        static void Main(string[] args1) {
            Console.WindowWidth = 100;
            string etalonFileName = @"D:\dev\chess\ChessRun\libs\Sharper\Sharper.exe";
            //string testFileName = @"D:\dev\chess\ChessRun\ChessRun.Engine\bin\Debug\ChessRun.exe";
            string testFileName = @"D:\dev\chess\ChessRunAsm\ChessEngine64.exe";

            var etalonProcessInfo = new ProcessStartInfo();
            etalonProcessInfo.RedirectStandardInput = true;
            etalonProcessInfo.RedirectStandardOutput = true;
            etalonProcessInfo.FileName = etalonFileName;
            etalonProcessInfo.UseShellExecute = false;

            var testProcessInfo = new ProcessStartInfo();
            testProcessInfo.RedirectStandardInput = true;
            testProcessInfo.RedirectStandardOutput = true;
            testProcessInfo.FileName = testFileName;
            testProcessInfo.UseShellExecute = false;

            var etalonProcess = Process.Start(etalonProcessInfo);
            var testProcess = Process.Start(testProcessInfo);

            _testClient = new EngineClient(testProcess);
            _etalonClient = new EngineClient(etalonProcess);

            _testClient.Init();
            _etalonClient.Init();

            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChessRun.Engine.Diagnostics.Resources.perftsuite.epd");
            if (resource == null) throw new InvalidOperationException("Test resource is not found");
            var started = DateTime.Now;
            using (var reader = new StreamReader(resource)) {
                int lineIndex = 0;
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    lineIndex++;
                    line = line.Trim();
                    if (line.StartsWith("#") || line == string.Empty) continue;
                    var args = line.Split(';');
                    string fen = args[0];
                    CheckLineFEN(fen, 0, 4);
                }
            }
            var ended = DateTime.Now;
            Console.Write("Time: {0}", (ended - started).TotalSeconds);

        }

        private static void CheckLineFEN(string fen, int depth, int depthLeft) {
            WriteInfo(depth, fen);

            var matches = MatchMoves(fen, depthLeft);
            var invalidMoves = matches
                .Where(item => item.Actual == null || item.Expected == null || item.Actual.Nodes != item.Expected.Nodes)
                .ToList();

            ClearLine();
            if (invalidMoves.Count == 0) {
                WriteSuccess(depth, fen);
            } else {
                foreach (var invalidMove in invalidMoves) {
                    if (invalidMove.Actual == null && invalidMove.Expected != null) {
                        WriteError(depth, "{0} (missing node {1})", fen, invalidMove.Expected.Move);
                    } else if (invalidMove.Expected == null && invalidMove.Actual != null) {
                        WriteError(depth, "{0} (extra node {1})", fen, invalidMove.Actual.Move);
                    } else {
                        WriteDebug(depth, "{0} (expected: {2}, actual: {1})", fen, invalidMove.Actual.Nodes, invalidMove.Expected.Nodes);
                        var newFen = ExecuteMove(fen, invalidMove.Expected.Move);
                        CheckLineFEN(newFen, depth + 1, depthLeft - 1);
                    }
                }
            }
        }

        private static void ClearLine() {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static IList<MatchInfo> MatchMoves(string fen, int depthLeft) {
            _testClient.ResetBoard();
            _etalonClient.ResetBoard();
            _testClient.SetBoard(fen);
            _etalonClient.SetBoard(fen);

            var expectedItems = _etalonClient.Divide(depthLeft);
            var actualItems = _testClient.Divide(depthLeft);

            var res = expectedItems.Select(expectedItem => new MatchInfo {
                Fen = fen,
                Expected = expectedItem
            }).ToList();
            foreach (var actualItem in actualItems) {
                var matchItem = res.FirstOrDefault(item => item.Expected.Move == actualItem.Move);
                if (matchItem == null) {
                    res.Add(new MatchInfo {
                        Fen = fen,
                        Actual = actualItem
                    });
                } else {
                    matchItem.Actual = actualItem;
                }
            }

            return res;
        }

        private class MatchInfo {
            public string Fen;

            public EngineClient.DivideItem Actual;

            public EngineClient.DivideItem Expected;


        }
        private static string ExecuteMove(string fen, string move) {
            var board = new ChessBoard();
            FEN.Setup(board, fen);
            var from = (CellName)Enum.Parse(typeof(CellName), move.Substring(0, 2), true);
            var to = (CellName)Enum.Parse(typeof(CellName), move.Substring(2, 2), true);
            var moves = board.GetValidMovesList();
            moves = moves.Where(item => item.From == from && item.To == to).ToList();
            if (move.Length > 4) {
                var promotionChar = move[4];
                var promotion = GetPromotionPiece(board.Turn, promotionChar);
                moves = moves.Where(item => item.Promotion == promotion).ToList();
            }
            if (moves.Count == 1) {
                RollbackData rollback;
                board.Move(moves[0], out rollback);
            } else if (moves.Count == 0) {
                WriteError(0, "Couldnt find move {0}", move);
            } else {
                WriteError(0, "Ambigous move {0}", move);
            }

            return FEN.GetFEN(board);
        }

        private static PieceType GetPromotionPiece(PieceColor turn, char ch) {
            switch (ch) {
                case 'Q':
                    return turn == PieceColor.White ? PieceType.WhiteQueen : PieceType.BlackQueen;
                case 'R':
                    return turn == PieceColor.White ? PieceType.WhiteRook : PieceType.BlackRook;
                case 'B':
                    return turn == PieceColor.White ? PieceType.WhiteBishop : PieceType.BlackBishop;
                case 'N':
                    return turn == PieceColor.White ? PieceType.WhiteKnight : PieceType.BlackKnight;
                default:
                    WriteError(0, "Invalid promotion " + ch);
                    return PieceType.None;
            }
        }

        private static void WriteError(int depth, string message, params object[] args) {
            WriteTreePrefix(depth);

            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ForegroundColor = cl;
        }

        private static void WriteDebug(int depth, string message, params object[] args) {
            WriteTreePrefix(depth);

            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ForegroundColor = cl;
        }

        private static void WriteInfo(int depth, string message, params object[] args) {
            WriteTreePrefix(depth);

            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message, args);
            Console.ForegroundColor = cl;
        }

        private static void WriteSuccess(int depth, string message, params object[] args) {
            WriteTreePrefix(depth);

            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message, args);
            Console.ForegroundColor = cl;
        }

        private static void WriteTreePrefix(int depth) {
            for (var i = 0; i < depth; i++) {
                Console.Write("| ");
            }
            Console.Write("+-");
        }
    }
}
