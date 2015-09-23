using System;
using System.Collections.Generic;

namespace ChessRun.Engine.Utils {
    /// <summary>
    /// Represents binary notation of chess board state.
    /// </summary>
    public static class BFEN {

        public const string INITIAL_POSITION = "l4q4eWZmZmb/////AAAAADEkUhMP";

        #region Reading

        public static void Setup(ChessBoard board, string bfen) {
            var array = Convert.FromBase64String(bfen);
            Setup(board, array);
        }

        public static void Setup(ChessBoard board, byte[] bfen) {
            int j = 0;
            int emptyCount = 0;
            for (var i = 0; i < _cellSequence.Length; i++) {
                var cell = _cellSequence[i];
                if (emptyCount > 0) {
                    board[cell] = PieceType.None;
                    emptyCount--;
                    continue;
                }
                if (j >> 1 >= bfen.Length) {
                    throw new FormatException("Unexpected end of piece positions part in BFEN");
                }
                var bt = bfen[j >> 1];
                var ch = (j & 0x01) == 0 ? bt >> 4 : bt & 0x0f;
                j++;
                switch (ch) {
                    case 0:
                        board[cell] = PieceType.WhitePawn;
                        break;
                    case 1:
                        board[cell] = PieceType.WhiteKnight;
                        break;
                    case 2:
                        board[cell] = PieceType.WhiteBishop;
                        break;
                    case 3:
                        board[cell] = PieceType.WhiteRook;
                        break;
                    case 4:
                        board[cell] = PieceType.WhiteQueen;
                        break;
                    case 5:
                        board[cell] = PieceType.WhiteKing;
                        break;
                    case 6:
                        board[cell] = PieceType.BlackPawn;
                        break;
                    case 7:
                        board[cell] = PieceType.BlackKnight;
                        break;
                    case 8:
                        board[cell] = PieceType.BlackBishop;
                        break;
                    case 9:
                        board[cell] = PieceType.BlackRook;
                        break;
                    case 10:
                        board[cell] = PieceType.BlackQueen;
                        break;
                    case 11:
                        board[cell] = PieceType.BlackKing;
                        break;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        board[cell] = PieceType.None;
                        emptyCount = ch - 12;
                        break;
                    default:
                        throw new FormatException("Unknown piece type '" + ch + "' at " + j);
                }
            }
            bool hasEnPassant;
            ReadFlags(board, bfen, ref j, out hasEnPassant);
            ReadCastles(board, bfen, ref j);
            if (hasEnPassant) ReadEnPassant(board, bfen, ref j);
            else board.EnPassantMove = CellName.None;
        }

        #endregion

        #region Writing

        public static string GetPackedBFEN(ChessBoard board) {
            var array = GetUnpackedBFEN(board);
            return Convert.ToBase64String(array);
        }

        public static byte[] GetUnpackedBFEN(ChessBoard board) {
            var list = new List<int>();
            int emptyCount = 0;
            for (var i = 0; i < _cellSequence.Length; i++) {
                var cell = _cellSequence[i];
                var piece = board[cell];
                if (piece != PieceType.None) {
                    FlushEmpty(list, ref emptyCount);
                }
                switch (piece) {
                    case PieceType.None:
                        emptyCount++;
                        while (emptyCount >= 4) {
                            list.Add(15);
                            emptyCount -= 4;
                        }
                        break;
                    case PieceType.WhitePawn:
                        list.Add(0);
                        break;
                    case PieceType.WhiteKnight:
                        list.Add(1);
                        break;
                    case PieceType.WhiteBishop:
                        list.Add(2);
                        break;
                    case PieceType.WhiteRook:
                        list.Add(3);
                        break;
                    case PieceType.WhiteQueen:
                        list.Add(4);
                        break;
                    case PieceType.WhiteKing:
                        list.Add(5);
                        break;
                    case PieceType.BlackPawn:
                        list.Add(6);
                        break;
                    case PieceType.BlackKnight:
                        list.Add(7);
                        break;
                    case PieceType.BlackBishop:
                        list.Add(8);
                        break;
                    case PieceType.BlackRook:
                        list.Add(9);
                        break;
                    case PieceType.BlackQueen:
                        list.Add(10);
                        break;
                    case PieceType.BlackKing:
                        list.Add(11);
                        break;
                    default:
                        throw new InvalidOperationException("Unknown piece " + piece);
                }
            }
            FlushEmpty(list, ref emptyCount);
            WriteFlags(list, board);
            WriteCastles(list, board);
            WriteEnPassant(list, board);

            var res = new byte[(list.Count + 1) >> 1];
            for (var i = 0; i < res.Length; i++) {
                var b = i * 2;
                if (b + 1 < list.Count) {
                    res[i] = (byte)(list[b] * 16 + list[b + 1]);
                } else {
                    res[i] = (byte)(list[b] * 16);
                }
            }
            return res;
        }

        private static void FlushEmpty(ICollection<int> sb, ref int emptyCount) {
            while (emptyCount >= 4) {
                sb.Add(15);
                emptyCount -= 4;
            }
            switch (emptyCount) {
                case 1:
                    sb.Add(12);
                    break;
                case 2:
                    sb.Add(13);
                    break;
                case 3:
                    sb.Add(14);
                    break;
            }
            emptyCount = 0;
        }

        #endregion

        private static void ReadFlags(ChessBoard board, byte[] bfen, ref int j, out bool hasEnPassant) {
            var bt = bfen[j >> 1];
            var ch = (j & 0x01) == 0 ? bt >> 4 : bt & 0x0f;
            j++;
            board.Turn = (ch & 8) != 0 ? PieceColor.Black : PieceColor.White;
            hasEnPassant = ((ch & 4) != 0);
        }

        private static void WriteFlags(ICollection<int> list, ChessBoard board) {
            byte res = 0;
            if (board.Turn == PieceColor.None) throw new InvalidOperationException("Turn is not set in board");
            if (board.Turn == PieceColor.Black) res += 8;
            if (board.EnPassantMove != CellName.None) res += 4;
            list.Add(res);
        }

        private static void WriteCastles(ICollection<int> list, ChessBoard board) {
            byte res = 0;
            if (board.WhiteCanDoShortCastle) {
                res += 8;
            }
            if (board.WhiteCanDoLongCastle) {
                res += 4;
            }
            if (board.BlackCanDoShortCastle) {
                res += 2;
            }
            if (board.BlackCanDoLongCastle) {
                res += 1;
            }
            list.Add(res);
        }

        private static void ReadCastles(ChessBoard board, byte[] bfen, ref int j) {
            var bt = bfen[j >> 1];
            var ch = (j & 0x01) == 0 ? bt >> 4 : bt & 0x0f;
            j++;

            board.WhiteCanDoShortCastle = (ch & 8) != 0;
            board.WhiteCanDoLongCastle = (ch & 4) != 0;
            board.BlackCanDoShortCastle = (ch & 2) != 0;
            board.BlackCanDoLongCastle = (ch & 1) != 0;
        }

        private static void ReadEnPassant(ChessBoard board, byte[] bfen, ref int j) {
            var bt = bfen[j >> 1];
            var ch = (j & 0x01) == 0 ? bt >> 4 : bt & 0x0f;
            j++;
            board.EnPassantMove = _enPassants[ch];
        }

        private static void WriteEnPassant(ICollection<int> list, ChessBoard board) {
            var cell = board.EnPassantMove;
            if (cell != CellName.None) {
                var index = _enPassants.IndexOf(cell);
                if (index < 0) throw new InvalidOperationException("Invalid EnPassant Cell");
                list.Add(index);
            }
        }


        private static readonly List<CellName> _enPassants = new List<CellName> {
            CellName.A3, CellName.B3, CellName.C3, CellName.D3, CellName.E3, CellName.F3, CellName.G3, CellName.H3,
            CellName.A6, CellName.B6, CellName.C6, CellName.D6, CellName.E6, CellName.F6, CellName.G6, CellName.H6,
        };

        private static readonly CellName[] _cellSequence = {
            CellName.A8, CellName.B8, CellName.C8, CellName.D8, CellName.E8, CellName.F8, CellName.G8, CellName.H8,
            CellName.A7, CellName.B7, CellName.C7, CellName.D7, CellName.E7, CellName.F7, CellName.G7, CellName.H7,
            CellName.A6, CellName.B6, CellName.C6, CellName.D6, CellName.E6, CellName.F6, CellName.G6, CellName.H6,
            CellName.A5, CellName.B5, CellName.C5, CellName.D5, CellName.E5, CellName.F5, CellName.G5, CellName.H5,
            CellName.A4, CellName.B4, CellName.C4, CellName.D4, CellName.E4, CellName.F4, CellName.G4, CellName.H4,
            CellName.A3, CellName.B3, CellName.C3, CellName.D3, CellName.E3, CellName.F3, CellName.G3, CellName.H3,
            CellName.A2, CellName.B2, CellName.C2, CellName.D2, CellName.E2, CellName.F2, CellName.G2, CellName.H2,
            CellName.A1, CellName.B1, CellName.C1, CellName.D1, CellName.E1, CellName.F1, CellName.G1, CellName.H1
                                                                 };

    }
}
