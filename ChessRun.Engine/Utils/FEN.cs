using System;
using System.Text;

namespace ChessRun.Engine.Utils {
    public class FEN {

        private static readonly CellName[] _cellSequence = new[] {
            
            CellName.A8, CellName.B8, CellName.C8, CellName.D8, CellName.E8, CellName.F8, CellName.G8, CellName.H8,
            CellName.None,
            CellName.A7, CellName.B7, CellName.C7, CellName.D7, CellName.E7, CellName.F7, CellName.G7, CellName.H7,
            CellName.None,
            CellName.A6, CellName.B6, CellName.C6, CellName.D6, CellName.E6, CellName.F6, CellName.G6, CellName.H6,
            CellName.None,
            CellName.A5, CellName.B5, CellName.C5, CellName.D5, CellName.E5, CellName.F5, CellName.G5, CellName.H5,
            CellName.None,
            CellName.A4, CellName.B4, CellName.C4, CellName.D4, CellName.E4, CellName.F4, CellName.G4, CellName.H4,
            CellName.None,
            CellName.A3, CellName.B3, CellName.C3, CellName.D3, CellName.E3, CellName.F3, CellName.G3, CellName.H3,
            CellName.None,
            CellName.A2, CellName.B2, CellName.C2, CellName.D2, CellName.E2, CellName.F2, CellName.G2, CellName.H2,
            CellName.None,
            CellName.A1, CellName.B1, CellName.C1, CellName.D1, CellName.E1, CellName.F1, CellName.G1, CellName.H1,
        };

        public const string INITIAL_POSITION = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        #region Writing

        public static string GetFEN(ChessBoard board) {
            var res = new StringBuilder();
            int emptyCount = 0;
            for (var i = 0; i < _cellSequence.Length; i++) {
                var cell = _cellSequence[i];
                if (cell == CellName.None) {
                    if (emptyCount > 0) {
                        res.Append(emptyCount);
                        emptyCount = 0;
                    }
                    res.Append('/');
                    continue;
                }
                var piece = board[cell];
                if (piece != PieceType.None) {
                    if (emptyCount > 0) {
                        res.Append(emptyCount);
                        emptyCount = 0;
                    }
                }
                switch (piece) {
                    case PieceType.None:
                        emptyCount++;
                        break;
                    case PieceType.BlackPawn:
                        res.Append('p');
                        break;
                    case PieceType.BlackKnight:
                        res.Append("n");
                        break;
                    case PieceType.BlackBishop:
                        res.Append("b");
                        break;
                    case PieceType.BlackRook:
                        res.Append("r");
                        break;
                    case PieceType.BlackQueen:
                        res.Append("q");
                        break;
                    case PieceType.BlackKing:
                        res.Append("k");
                        break;
                    case PieceType.WhitePawn:
                        res.Append('P');
                        break;
                    case PieceType.WhiteKnight:
                        res.Append("N");
                        break;
                    case PieceType.WhiteBishop:
                        res.Append("B");
                        break;
                    case PieceType.WhiteRook:
                        res.Append("R");
                        break;
                    case PieceType.WhiteQueen:
                        res.Append("Q");
                        break;
                    case PieceType.WhiteKing:
                        res.Append("K");
                        break;
                    default:
                        throw new InvalidOperationException("Unknown piece " + piece);
                }
            }
            if (emptyCount > 0) {
                res.Append(emptyCount);
            }
            WriteTurn(res, board);
            WriteCastles(res, board);
            WriteEnPassant(res, board);
            return res.ToString();
        }

        private static void WriteTurn(StringBuilder res, ChessBoard board) {
            switch (board.Turn) {
                case PieceColor.White:
                    res.Append(" w");
                    break;
                case PieceColor.Black:
                    res.Append(" b");
                    break;
                default:
                    throw new InvalidOperationException("Turn is not set in board");
            }
        }

        private static void WriteCastles(StringBuilder res, ChessBoard board) {
            res.Append(" ");
            if (board.WhiteCanDoShortCastle) {
                res.Append('K');
            }
            if (board.WhiteCanDoLongCastle) {
                res.Append('Q');
            }
            if (board.BlackCanDoShortCastle) {
                res.Append('k');
            }
            if (board.BlackCanDoLongCastle) {
                res.Append('q');
            }
            if (!board.WhiteCanDoShortCastle && !board.WhiteCanDoLongCastle &&
                !board.BlackCanDoShortCastle && !board.BlackCanDoLongCastle) {
                res.Append('-');
            }
        }

        private static void WriteEnPassant(StringBuilder res, ChessBoard board) {
            var cell = board.EnPassantMove;
            if (cell == CellName.None) res.Append(" -");
            else res.Append(' ').Append(CellOperations.GetCellName(cell));
        }

        #endregion

        #region Reading

        public static void Setup(ChessBoard board, string fen) {
            int j = 0;
            int emptyCount = 0;
            for (var i = 0; i < _cellSequence.Length; i++) {
                var cell = _cellSequence[i];
                if (emptyCount > 0) {
                    board[cell] = PieceType.None;
                    emptyCount--;
                    continue;
                }
                if (j >= fen.Length) {
                    throw new FormatException("Unexpected end of piece positions part in FEN");
                }
                char ch = fen[j];
                if (cell == CellName.None) {
                    if (ch == '/') {
                        j++;
                        continue;
                    }
                    throw new FormatException("Expected line separator '/' at " + j);
                }
                switch (ch) {
                    case 'r':
                        board[cell] = PieceType.BlackRook;
                        break;
                    case 'n':
                        board[cell] = PieceType.BlackKnight;
                        break;
                    case 'b':
                        board[cell] = PieceType.BlackBishop;
                        break;
                    case 'q':
                        board[cell] = PieceType.BlackQueen;
                        break;
                    case 'k':
                        board[cell] = PieceType.BlackKing;
                        break;
                    case 'p':
                        board[cell] = PieceType.BlackPawn;
                        break;
                    case 'R':
                        board[cell] = PieceType.WhiteRook;
                        break;
                    case 'N':
                        board[cell] = PieceType.WhiteKnight;
                        break;
                    case 'B':
                        board[cell] = PieceType.WhiteBishop;
                        break;
                    case 'Q':
                        board[cell] = PieceType.WhiteQueen;
                        break;
                    case 'K':
                        board[cell] = PieceType.WhiteKing;
                        break;
                    case 'P':
                        board[cell] = PieceType.WhitePawn;
                        break;
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                        board[cell] = PieceType.None;
                        emptyCount = ch - '0' - 1;
                        break;
                    case '/':
                        throw new FormatException("Unexpected row separator '/' at " + j);
                    default:
                        throw new FormatException("Unknown piece type '" + ch + "' at " + j);
                }
                j++;
            }
            if (fen[j] != ' ') throw new FormatException("Should be a space after pieces data");
            j++;
            ReadTurn(board, ref fen, ref j);
            if (fen[j] != ' ') throw new FormatException("Should be a space after turn");
            j++;
            ReadCastles(board, ref fen, ref j);
            SkipWhite(ref fen, ref j);
            ReadEnPassant(board, ref fen, ref j);
        }

        private static void ReadTurn(ChessBoard board, ref string fen, ref int i) {
            char turn = fen[i];
            i++;
            switch (turn) {
                case 'w':
                case 'W':
                    board.Turn = PieceColor.White;
                    break;
                case 'b':
                case 'B':
                    board.Turn = PieceColor.Black;
                    break;
                default:
                    throw new FormatException();
            }
        }

        private static void ReadCastles(ChessBoard board, ref string content, ref int i) {
            bool hasData = false;
            bool hasWhiteK = false;
            bool hasBlackK = false;
            bool hasWhiteQ = false;
            bool hasBlackQ = false;
            for (int j = 0; j < 5; j++) {
                if (i >= content.Length) break;
                char ch = content[i];
                i++;
                if (ch == ' ') break;
                if (ch == '-') {
                    hasData = true;
                    break;
                }
                if (ch == 'K') {
                    if (hasWhiteK) throw new FormatException("White Castle King Side specified more then once");
                    hasWhiteK = true;
                    hasData = true;
                } else if (ch == 'k') {
                    if (hasBlackK) throw new FormatException("Black Castle King Side specified more then once");
                    hasBlackK = true;
                    hasData = true;
                } else if (ch == 'Q') {
                    if (hasWhiteQ) throw new FormatException("White Castle Queen Side specified more then once");
                    hasWhiteQ = true;
                    hasData = true;
                } else if (ch == 'q') {
                    if (hasBlackQ) throw new FormatException("Black Castle Queen Side specified more then once");
                    hasBlackQ = true;
                    hasData = true;
                }
            }
            if (!hasData) throw new FormatException("Missed castle moves data");
            board.WhiteCanDoLongCastle = hasWhiteQ;
            board.WhiteCanDoShortCastle = hasWhiteK;
            board.BlackCanDoLongCastle = hasBlackQ;
            board.BlackCanDoShortCastle = hasBlackK;
        }

        private static void ReadEnPassant(ChessBoard board, ref string fen, ref int i) {
            if (i >= fen.Length) {
                board.EnPassantMove = CellName.None;
                return;
            }
            char ch = fen[i];
            i++;
            if (ch == '-') {
                board.EnPassantMove = CellName.None;
            } else {
                char rank = fen[i];
                i++;
                board.EnPassantMove = CellOperations.GetCell(ch, rank);
            }
        }

        private static void SkipWhite(ref string content, ref int i) {
            while (i < content.Length) {
                char ch = content[i];
                if (!char.IsWhiteSpace(ch)) return;
                i++;
            }
        }

        #endregion

    }
}
