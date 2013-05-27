using ChessRun.Engine;

namespace ChessRun.Rasterizer {
    class Program {
        static void Main(string[] args) {
            var rasterizer = new ChessBoardRasterizer();
            var board = new ChessBoard();
            var result = rasterizer.Rasterize(board, 424);
            result.Save("result.png");
        }
    }
}
