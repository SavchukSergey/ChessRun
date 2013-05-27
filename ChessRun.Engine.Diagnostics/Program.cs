using System;
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
            string etalonFileName = @"D:\dev\ChessRun\libs\Sharper\Sharper.exe";
            string testFileName = @"D:\dev\ChessRun\ChessRun.Engine\bin\Debug\ChessRun.exe";

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
                    Console.WriteLine(fen);
                    CheckLineFEN(fen, 4);
                }
            }
            var ended = DateTime.Now;
            Console.Write("Time: {0}", (ended - started).TotalSeconds);

        }

        private static void CheckLineFEN(string fen, int depth) {
            _testClient.ResetBoard();
            _etalonClient.ResetBoard();
            _testClient.SetBoard(fen);
            _etalonClient.SetBoard(fen);
            var expectedItems = _etalonClient.Divide(depth);
            var actualItems = _testClient.Divide(depth);
            for (var j = 0; j < expectedItems.Count; j++) {
                var expectedItem = expectedItems[j];
                var actualItem = actualItems.FirstOrDefault(item => item.Move == expectedItem.Move);
                if (actualItem == null) {
                    WriteError("Problem FEN: {0}, Move: {1} (Missing node)", fen, expectedItem.Move);
                    continue;
                }
                if (expectedItem.Nodes != actualItem.Nodes) {
                    if (depth == 1) {
                        WriteError("Problem FEN: {0}, Move: {1} (Invalid nodes count)", fen, expectedItem.Move);
                    } else {
                        WriteDebug("Debugging: {0}, Move: {1}, ExpectedNodes: {2}, ActualNodes: {3}", fen, expectedItem.Move, expectedItem.Nodes, actualItem.Nodes);

                        string moveString;
                        ChessBoard board = ParseMove(fen, expectedItem.Move, out moveString);
                        var sMove = board.GetMove(moveString);
                        RollbackData rollback;
                        board.Move(sMove, out rollback);
                        var newFen = FEN.GetFEN(board);
                        CheckLineFEN(newFen, depth - 1);
                    }
                }
            }
            for (var j = 0; j < actualItems.Count; j++) {
                var actualItem = actualItems[j];
                var expectedItem = expectedItems.FirstOrDefault(item => item.Move == actualItem.Move);
                if (expectedItem == null) {
                    WriteError("Problem FEN: {0}, Move: {1} (Unexpected node)", fen, actualItem.Move);
                    continue;
                }
            }
        }

        private static void WriteError(string message, params object[] args) {
            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ForegroundColor = cl;
        }

        private static void WriteDebug(string message, params object[] args) {
            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ForegroundColor = cl;
        }

        private static ChessBoard ParseMove(string fen, string move, out string moveString) {
            ChessBoard board = new ChessBoard();
            FEN.Setup(board, fen);
            var from = (CellName)Enum.Parse(typeof(CellName), move.Substring(0, 2), true);
            var to = (CellName)Enum.Parse(typeof(CellName), move.Substring(2, 2), true);
            var piece = board[from];
            moveString = null;
            switch (piece) {
                case PieceType.WhitePawn:
                case PieceType.BlackPawn:
                    moveString = from.ToString().ToLower() + to.ToString().ToLower();
                    //TODO: promotions
                    break;
                case PieceType.WhiteKnight:
                case PieceType.BlackKnight:
                    moveString = "N" + from.ToString().ToLower() + to.ToString().ToLower();
                    break;
                case PieceType.WhiteBishop:
                case PieceType.BlackBishop:
                    moveString = "B" + from.ToString().ToLower() + to.ToString().ToLower();
                    break;
                case PieceType.WhiteRook:
                case PieceType.BlackRook:
                    moveString = "R" + from.ToString().ToLower() + to.ToString().ToLower();
                    break;
                case PieceType.WhiteQueen:
                case PieceType.BlackQueen:
                    moveString = "Q" + from.ToString().ToLower() + to.ToString().ToLower();
                    break;
                case PieceType.WhiteKing:
                case PieceType.BlackKing:
                    if ((from == CellName.E1 && to == CellName.G1) || (from == CellName.E8 && to == CellName.G8)) {
                        moveString = "O-O";
                    } else if ((from == CellName.E1 && to == CellName.C1) || (from == CellName.E8 && to == CellName.C8)) {
                        moveString = "O-O";
                    } else {
                        moveString = "K" + from.ToString().ToLower() + to.ToString().ToLower();
                    }
                    break;
            }
            return board;
        }
    }
}
