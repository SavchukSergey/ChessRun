using System;

namespace ChessRun.Engine.Utils {
    public static class PieceOperations {

        public static bool IsWhite(PieceType piece) {
            return ((int)piece & 0x08) == 0 && (int)piece != 0;
        }

        public static bool IsBlack(PieceType piece) {
            return ((int)piece & 0x08) != 0;
        }

        public static PieceColor GetColor(PieceType piece) {
            if (IsWhite(piece)) return PieceColor.White;
            if (IsBlack(piece)) return PieceColor.Black;
            return PieceColor.None;
        }

        public static PieceType GetPawn(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhitePawn;
                case PieceColor.Black:
                    return PieceType.BlackPawn;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static PieceType GetBishop(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhiteBishop;
                case PieceColor.Black:
                    return PieceType.BlackBishop;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static PieceType GetKnight(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhiteKnight;
                case PieceColor.Black:
                    return PieceType.BlackKnight;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static PieceType GetRook(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhiteRook;
                case PieceColor.Black:
                    return PieceType.BlackRook;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static PieceType GetQueen(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhiteQueen;
                case PieceColor.Black:
                    return PieceType.BlackQueen;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static PieceType GetKing(PieceColor color) {
            switch (color) {
                case PieceColor.White:
                    return PieceType.WhiteKing;
                case PieceColor.Black:
                    return PieceType.BlackKing;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static bool IsValidPiece(char pieceSymbol) {
            switch (pieceSymbol) {
                case 'R':
                case 'N':
                case 'B':
                case 'Q':
                case 'K':
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsValidPromotionPiece(char pieceSymbol) {
            switch (pieceSymbol) {
                case 'R':
                case 'N':
                case 'B':
                case 'Q':
                    return true;
                default:
                    return false;
            }
        }

        public static PieceType GetPromotionPiece(char pieceSymbol, PieceColor color) {
            switch (pieceSymbol) {
                case 'R':
                    return GetRook(color);
                case 'N':
                    return GetKnight(color);
                case 'B':
                    return GetBishop(color);
                case 'Q':
                    return GetQueen(color);
                default:
                    throw new InvalidOperationException("Invalid promotion piece " + pieceSymbol);
            }
        }

        public static string GetPromotionPieceSymbol(PieceType piece) {
            switch (piece) {
                case PieceType.WhiteKnight:
                case PieceType.BlackKnight:
                    return "N";
                case PieceType.WhiteBishop:
                case PieceType.BlackBishop:
                    return "B";
                case PieceType.WhiteRook:
                case PieceType.BlackRook:
                    return "R";
                case PieceType.WhiteQueen:
                case PieceType.BlackQueen:
                    return "Q";
                default:
                    throw new InvalidOperationException("Invalid promotion piece " + piece);
            }
        }

        public static PieceType GetPiece(char pieceSymbol, PieceColor color) {
            switch (pieceSymbol) {
                case 'R':
                case 'r':
                    return GetRook(color);
                case 'N':
                case 'n':
                    return GetKnight(color);
                case 'B':
                case 'b':
                    return GetBishop(color);
                case 'Q':
                case 'q':
                    return GetQueen(color);
                case 'K':
                case 'k':
                    return GetKing(color);
                default:
                    throw new FormatException("Unknown piece type");
            }
        }

        public static PieceType InvertColor(PieceType piece) {
            switch (piece) {
                case PieceType.None:
                    return PieceType.None;
                case PieceType.WhitePawn:
                    return PieceType.BlackPawn;
                case PieceType.WhiteKnight:
                    return PieceType.BlackKnight;
                case PieceType.WhiteBishop:
                    return PieceType.BlackBishop;
                case PieceType.WhiteRook:
                    return PieceType.BlackRook;
                case PieceType.WhiteQueen:
                    return PieceType.BlackQueen;
                case PieceType.WhiteKing:
                    return PieceType.BlackKing;
                case PieceType.BlackPawn:
                    return PieceType.WhitePawn;
                case PieceType.BlackKnight:
                    return PieceType.WhiteKnight;
                case PieceType.BlackBishop:
                    return PieceType.WhiteBishop;
                case PieceType.BlackRook:
                    return PieceType.WhiteRook;
                case PieceType.BlackQueen:
                    return PieceType.WhiteQueen;
                case PieceType.BlackKing:
                    return PieceType.WhiteKing;
                default:
                    throw new InvalidOperationException("Unknown piece " + piece);
            }
        }

        public static PieceType GetPieceFromShort(string pieceName) {
            switch (pieceName) {
                case "no":
                    return PieceType.None;
                case "wp":
                    return PieceType.WhitePawn;
                case "wn":
                    return PieceType.WhiteKnight;
                case "wb":
                    return PieceType.WhiteBishop;
                case "wr":
                    return PieceType.WhiteRook;
                case "wq":
                    return PieceType.WhiteQueen;
                case "wk":
                    return PieceType.WhiteKing;
                case "bp":
                    return PieceType.BlackPawn;
                case "bn":
                    return PieceType.BlackKnight;
                case "bb":
                    return PieceType.BlackBishop;
                case "br":
                    return PieceType.BlackRook;
                case "bq":
                    return PieceType.BlackQueen;
                case "bk":
                    return PieceType.BlackKing;
                default:
                    throw new InvalidOperationException("Unknown piece " + pieceName);
            }
        }

        public static string GetPieceName(PieceType piece) {
            switch (piece) {
                case PieceType.None:
                    return "no";
                case PieceType.WhitePawn:
                    return "wp";
                case PieceType.WhiteKnight:
                    return "wn";
                case PieceType.WhiteBishop:
                    return "wb";
                case PieceType.WhiteRook:
                    return "wr";
                case PieceType.WhiteQueen:
                    return "wq";
                case PieceType.WhiteKing:
                    return "wk";
                case PieceType.BlackPawn:
                    return "bp";
                case PieceType.BlackKnight:
                    return "bn";
                case PieceType.BlackBishop:
                    return "bb";
                case PieceType.BlackRook:
                    return "br";
                case PieceType.BlackQueen:
                    return "bq";
                case PieceType.BlackKing:
                    return "bk";
                default:
                    throw new InvalidOperationException("Unknown piece " + piece);
            }
        }
    }
}