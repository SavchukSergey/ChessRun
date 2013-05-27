using System.Drawing;
using ChessRun.Engine;

namespace ChessRun.Rasterizer {
    public class ChessBoardRasterizer {

        private readonly Color boardWhite = Color.FromArgb(unchecked((int)0xffF0D9B5));
        private readonly Color boardBlack = Color.FromArgb(unchecked((int)0xffB58863));

        public Bitmap Rasterize(ChessBoard board, int size) {
            var bmp = new Bitmap(size, size);
            Graphics gr = Graphics.FromImage(bmp);
            DrawBoard(gr, board);
            return bmp;
        }

        private void DrawBoard(Graphics gr, ChessBoard board) {
            gr.Clear(boardWhite);
            var rect = gr.VisibleClipBounds;
            var cellHeight = (rect.Bottom - rect.Top) / 8;
            var cellWidth = (rect.Right - rect.Left) / 8;
            var boardBlackBrush = new SolidBrush(boardBlack);
            for (int i = 0; i < 8; i++) {
                var y0 = i * cellHeight;
                for (var j = 0; j < 8; j++) {
                    var x0 = j * cellWidth;
                    if ((i + j) % 2 == 1) {
                        gr.FillRectangle(boardBlackBrush, x0, y0, cellWidth, cellHeight);
                    }
                    var piece = board[GetCell(j + 1, 8 - i)];
                    Image img = GetPieceSymbol(piece);
                    if (img != null) {
                        var pieceW = cellWidth * 0.9f;
                        var pieceH = cellHeight * 0.9f;
                        var strRect = new SizeF(pieceW, pieceH);
                        var sx = x0 + (cellWidth - strRect.Width) / 2;
                        var sy = y0 + (cellHeight - strRect.Height) / 2;
                        gr.DrawImage(img, sx, sy, pieceW, pieceH);
                    }
                }
            }
        }

        private static CellName GetCell(int file, int rank) {
            return (CellName)((rank - 1) * 8 + (file - 1));
        }

        private Image GetPieceSymbol(PieceType piece) {
            string resourceName = null;
            switch (piece) {
                case PieceType.BlackPawn:
                    resourceName = "bp.png";
                    break;
                case PieceType.BlackBishop:
                    resourceName = "bb.png";
                    break;
                case PieceType.BlackKnight:
                    resourceName = "bn.png";
                    break;
                case PieceType.BlackRook:
                    resourceName = "br.png";
                    break;
                case PieceType.BlackQueen:
                    resourceName = "bq.png";
                    break;
                case PieceType.BlackKing:
                    resourceName = "bk.png";
                    break;
                case PieceType.WhitePawn:
                    resourceName = "wp.png";
                    break;
                case PieceType.WhiteBishop:
                    resourceName = "wb.png";
                    break;
                case PieceType.WhiteKnight:
                    resourceName = "wn.png";
                    break;
                case PieceType.WhiteRook:
                    resourceName = "wr.png";
                    break;
                case PieceType.WhiteQueen:
                    resourceName = "wq.png";
                    break;
                case PieceType.WhiteKing:
                    resourceName = "wk.png";
                    break;
            }
            if (resourceName != null) {
                resourceName = "ChessRun.Rasterizer.Resources." + resourceName;
                var assembly = GetType().Assembly;
                var stream = assembly.GetManifestResourceStream(resourceName);
                return Image.FromStream(stream);
            }
            return null;
        }
    }
}
