using System;

namespace ChessRun.Pgn {
    public class PgnParseException : Exception {

        public PgnParseException(string message)
            : base(message) {

        }
    }
}
