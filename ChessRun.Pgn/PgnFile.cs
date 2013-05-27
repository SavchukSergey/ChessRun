using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRun.Pgn {
    //http://www.chessclub.com/help/PGN-spec
    public class PgnFile {
        private readonly IList<PgnTag> _tags = new List<PgnTag>();
        private readonly IList<PgnMoveNode> _moves = new List<PgnMoveNode>();

        public IList<PgnTag> Tags {
            get { return _tags; }
        }

        public IList<PgnMoveNode> Moves {
            get { return _moves; }
        }

        public PgnGameResult GameResult {
            get;
            set;
        }

        public string Event {
            get {
                return this["Event"];
            }
            set {
                this["Event"] = value;
            }
        }

        public string Site {
            get {
                return this["Site"];
            }
            set {
                this["Site"] = value;
            }
        }

        public string White {
            get {
                return this["White"];
            }
            set {
                this["White"] = value;
            }
        }

        public string Black {
            get {
                return this["Black"];
            }
            set {
                this["Black"] = value;
            }
        }

        public string Result {
            get {
                return this["Result"];
            }
            set {
                this["Result"] = value;
            }
        }

        protected string this[string paramName] {
            get {
                var tag = _tags.FirstOrDefault(item => item.Name == paramName);
                return (tag != null ? tag.Value : "");
            }
            set {
                var tag = _tags.FirstOrDefault(item => item.Name == paramName);
                if (tag == null) {
                    tag = new PgnTag { Name = paramName };
                    _tags.Add(tag);
                }
                tag.Value = value;
            }
        }

        public string Export() {
            var sb = new StringBuilder();
            foreach (var tag in _tags) {
                sb.AppendFormat("[{0} {1}]", tag.Name, QuoteString(tag.Value));
                sb.Append('\n');
            }
            sb.Append('\n');
            foreach (var move in _moves) {
                sb.Append(move.Ordinal + ".");
                sb.Append(move.Whites);
                sb.Append(' ');
                sb.Append(move.Blacks);
                sb.Append(' ');
            }
            return sb.ToString();
        }

        private string QuoteString(string source) {
            return "\"" + source
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                + "\"";

        }
    }
}
