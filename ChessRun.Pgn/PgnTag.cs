namespace ChessRun.Pgn {
    public class PgnTag {

        public string Name;

        public string Value;

        public override string ToString() {
            return string.Format("[{0} = {1}]", Name, Value);
        }
    }
}
