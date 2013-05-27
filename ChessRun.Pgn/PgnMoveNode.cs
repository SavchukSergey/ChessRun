using System.Collections.Generic;

namespace ChessRun.Pgn {
    public class PgnMoveNode {

        public int Ordinal;

        public string Whites;

        public string Blacks;

        private readonly IList<PgnMoveNode> _subNodes = new List<PgnMoveNode>();
        public IList<PgnMoveNode> SubNodes {
            get { return _subNodes; }
        }

        public override string ToString() {
            return string.Format("{0}. {1} {2}", Ordinal, Whites, Blacks);
        }

    }
}
