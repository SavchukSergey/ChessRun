using System;
using ChessRun.Engine.Utils;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ChessRun.Engine.Debugger {
    public class ChessBoardVisualizerObjectSource : VisualizerObjectSource {

        public override void GetData(object target, System.IO.Stream outgoingData) {
            var board = target as ChessBoard;
            if (board != null) {
                var bfen = BFEN.GetUnpackedBFEN(board);
                base.GetData(bfen, outgoingData);
            } else {
                throw new InvalidOperationException("Invalid object type passed to debugger");
            }
        }
       
    }
}
