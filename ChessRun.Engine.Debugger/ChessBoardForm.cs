using System.Windows.Forms;
using ChessRun.Rasterizer;

namespace ChessRun.Engine.Debugger {
    public partial class ChessBoardForm : Form {

        private readonly ChessBoardRasterizer _rasterizer = new ChessBoardRasterizer();

        public ChessBoardForm() {
            InitializeComponent();
        }

        public void Visualize(ChessBoard board)
        {
            var bmp = _rasterizer.Rasterize(board, 400);
            //Invoke( new Action(() => {
            pctBoard.Image = bmp;
            switch (board.Turn)
            {
                case PieceColor.White:
                    lblTurnValue.Text = "White";
                    break;
                case PieceColor.Black:
                    lblTurnValue.Text = "Black";
                    break;
                case PieceColor.None:
                    lblTurnValue.Text = "None";
                    break;
            }
            //}));
        }
     
    }
}
