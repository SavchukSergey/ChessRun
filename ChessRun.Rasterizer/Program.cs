using ChessRun.Engine;
using ChessRun.Engine.Utils;

namespace ChessRun.Rasterizer {
    class Program {
        static void Main(string[] args) {
            var rasterizer = new ChessBoardRasterizer();
            var board = new ChessBoard();
            FEN.Setup(board, args[0]);
            var result = rasterizer.Rasterize(board, 424);
            result.Save(args[1]);
        }
    }
}
