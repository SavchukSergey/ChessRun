using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ChessRun.Engine.Debugger {
    public class ChessBoardDebugger : DialogDebuggerVisualizer {

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            var obj = objectProvider.GetObject();
            ChessBoard board;
            if (obj is ChessBoard) {
                board = (ChessBoard)obj;
            } else if (obj is byte[]) {
                board = new ChessBoard();
                BFEN.Setup(board, (byte[])obj);
            } else {
                return;
            }

            var frm = new ChessBoardForm();
            frm.Visualize(board);
            windowService.ShowDialog(frm);
        }

    }
}