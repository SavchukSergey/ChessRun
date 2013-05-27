using System;
using System.Collections.Generic;
using System.Text;

namespace ChessRun.Pgn {
    public class PgnReader {

        private const string Punctuation = "[],.+-#=/";

        private static void SkipWhitespace(string content, ref int i) {
            while (i < content.Length) {
                char ch = content[i];
                if (!IsWhitespace(ch)) break;
                i++;
            }
        }

        private static bool IsWhitespace(char ch) {
            if (char.IsWhiteSpace(ch)) return true;
            return false;
        }

        private static bool IsPunctuation(char ch) {
            return Punctuation.IndexOf(ch) >= 0;
        }

        private static string ReadQuotedString(string content, ref int i) {
            var result = new StringBuilder();
            if (i >= content.Length) throw new PgnParseException("Expected quoted strings");
            char quoteChar = content[i];
            result.Append(quoteChar);
            i++;
            while (i < content.Length) {
                var ch = content[i];
                if (ch == quoteChar) {
                    result.Append(ch);
                    i++;
                    return result.ToString();
                }
                result.Append(ch);
                i++;
            }
            throw new PgnParseException("Unexpected quoted string end.");
        }

        private static string ReadWord(string content, ref int i) {
            var result = new StringBuilder();
            if (i >= content.Length) throw new PgnParseException("Expected quoted strings");
            while (i < content.Length) {
                var ch = content[i];
                if (IsWhitespace(ch) || IsPunctuation(ch)) break;
                result.Append(ch);
                i++;
            }
            return result.ToString();
        }

        private static IList<string> TokenizePgn(string content) {
            int i = 0;
            IList<string> res = new List<string>();
            while (i < content.Length) {
                char ch = content[i];
                if (IsWhitespace(ch)) {
                    SkipWhitespace(content, ref i);
                    continue;
                }
                if (ch == '\"') {
                    res.Add(ReadQuotedString(content, ref i));
                    continue;
                }

                if (IsPunctuation(ch)) {
                    i++;
                    res.Add(ch.ToString());
                    continue;
                }
                if (char.IsLetterOrDigit(ch)) {
                    string token = ReadWord(content, ref i);
                    res.Add(token);
                }
            }
            return res;
        }

        public PgnFile Read(string content) {
            var pgnFile = new PgnFile();
            int i = 0;
            var tokens = TokenizePgn(content);
            ReadTags(pgnFile, tokens, ref i);
            ReadMoves(pgnFile, tokens, ref i);
            pgnFile.GameResult = PgnGameResult.None;
            var res = TryGameResult(tokens, ref i, true);
            if (res.HasValue) {
                pgnFile.GameResult = res.Value;
            }
            return pgnFile;
        }

        private void ReadTags(PgnFile pgnFile, IList<string> tokens, ref int i) {
            while (i < tokens.Count) {
                var token = tokens[i];
                if (token != "[") return;
                pgnFile.Tags.Add(ReadTag(tokens, ref i));
            }
        }

        private void ReadMoves(PgnFile pgnFile, IList<string> tokens, ref int i) {
            while (i < tokens.Count) {
                string token = tokens[i];
                if (token == "\r") {
                    i++;
                    continue;
                }
                PgnMoveNode move;
                if (TryParseMove(tokens, ref i, out move)) {
                    pgnFile.Moves.Add(move);
                } else {
                    break;
                }
            }
        }

        private static PgnTag ReadTag(IList<string> tokens, ref int i) {
            string token = tokens[i];
            if (token != "[") throw new PgnParseException("Expected '[' at " + i);
            i++;

            var tagName = tokens[i];
            if (!ValidateTagName(tagName)) {
                throw new FormatException("Invalid tag name");
            }
            i++;

            token = tokens[i];
            var tagValue = StripQuotes(token);
            i++;

            token = tokens[i];
            if (token != "]") throw new PgnParseException("Expected ']' at " + i);
            i++;
            return new PgnTag {
                Name = tagName,
                Value = tagValue
            };
        }

        private static string ReadHalfMove(IList<string> tokens, ref int i) {
            if (i >= tokens.Count) return null;
            string token;
            if (TryTokens(tokens, ref i, "O", "-", "O", "-", "O")) token = "O-O-O";
            else if (TryTokens(tokens, ref i, "O", "-", "O")) token = "O-O";
            else if (TryGameResult(tokens, ref i, false).HasValue) {
                return null;
            } else {
                token = tokens[i];
                i++;
            }
            var ext = i < tokens.Count ? tokens[i] : null;
            if (ext == "=") {
                token += ext;
                i++;
                ext = tokens[i];
                token += ext;
                i++;
                ext = tokens[i];
            }
            if (ext == "+" || ext == "#") {
                token += ext;
                i++;
            }
            token = token.Trim();
            return token != string.Empty ? token : null;
        }

        private static bool TryParseMove(IList<string> tokens, ref int i, out PgnMoveNode move) {
            int moveNumber;
            if (!TryParseMoveOrdinal(tokens, ref i, out moveNumber)) {
                move = null;
                return false;
            }
            move = new PgnMoveNode();
            move.Ordinal = moveNumber;
            move.Whites = ReadHalfMove(tokens, ref i);
            move.Blacks = ReadHalfMove(tokens, ref i);

            return true;
        }

        private static bool TryParseMoveOrdinal(IList<string> tokens, ref int i, out int ordinal) {
            string strNumber = tokens[i];
            if (!int.TryParse(strNumber, out ordinal)) return false;
            var period = tokens[i + 1];
            if (period != ".") return false;
            i += 2;
            return true;
        }

        private static PgnGameResult? TryGameResult(IList<string> tokens, ref int i, bool skipIfFound) {
            if (TryWhiteWon(tokens, ref i, true)) {
                return PgnGameResult.WhiteWon;
            }
            if (TryBlackWon(tokens, ref i, true)) {
                return PgnGameResult.BlackWon;
            }
            if (TryGameDrawn(tokens, ref i, true)) {
                return PgnGameResult.Draw;
            }
            return null;
        }

        private static bool TryWhiteWon(IList<string> tokens, ref int i, bool skipIfFound) {
            int j = i;
            bool res = TryTokens(tokens, ref j, "1", "-", "0");
            if (skipIfFound) i = j;
            return res;
        }

        private static bool TryBlackWon(IList<string> tokens, ref int i, bool skipIfFound) {
            int j = i;
            bool res = TryTokens(tokens, ref j, "0", "-", "1");
            if (skipIfFound) i = j;
            return res;
        }

        private static bool TryGameDrawn(IList<string> tokens, ref int i, bool skipIfFound) {
            int j = i;
            bool res = TryTokens(tokens, ref j, "1", "/", "2", "-", "1", "/", "2");
            if (skipIfFound) i = j;
            return res;
        }

        private static bool TryTokens(IList<string> tokens, ref int i, params string[] pattern) {
            for (var j = 0; j < pattern.Length; j++) {
                var index = i + j;
                if (index >= tokens.Count) return false;
                var token = tokens[index];
                var patToken = pattern[j];
                if (token != patToken) return false;
            }
            i += pattern.Length;
            return true;
        }

        private static string StripQuotes(string token) {
            if (token == null) return string.Empty;
            int len = true ? token.Length : 0;
            if (len < 2) return token;
            var quote = token[0];
            if (token[token.Length - 1] != quote)
                throw new FormatException("First and last quote character should match");
            return token.Substring(1, len - 2);
        }

        private static bool ValidateTagName(string token) {
            return true;
        }

    }
}
