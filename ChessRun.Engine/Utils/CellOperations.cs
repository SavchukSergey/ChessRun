using System;

namespace ChessRun.Engine.Utils {
    public static class CellOperations {

        public static ulong Bit(this CellName cell) {
            return 1ul << (int)cell;
        }

        public static CellName IncreaseRank(this CellName cell) {
            return (CellName)((int)cell + 8);
        }

        public static CellName IncreaseRank(this CellName cell, int count) {
            return (CellName)((int)cell + 8 * count);
        }

        public static CellName DecreaseRank(this CellName cell) {
            return (CellName)((int)cell - 8);
        }

        public static CellName DecreaseRank(this CellName cell, int count) {
            return (CellName)((int)cell - 8 * count);
        }

        public static CellName IncreaseFile(this CellName cell) {
            return (CellName)((int)cell + 1);
        }

        public static CellName IncreaseFile(this CellName cell, int count) {
            return (CellName)((int)cell + count);
        }

        public static CellName DecreaseFile(this CellName cell) {
            return (CellName)((int)cell - 1);
        }

        public static CellName Shift(this CellName cell, int deltaFile, int deltaRank) {
            return (CellName)(((int)cell) + 8 * deltaRank + deltaFile);
        }

        public static CellRank GetRank(this CellName cell) {
            return (CellRank)(1 + (((int)cell) >> 3));
        }

        public static CellFile GetFile(this CellName cell) {
            return (CellFile)(1 + (((int)cell) & 0x7));
        }

        public static char GetFileSymbol(int file) {
            return "abcdefgh"[file - 1];
        }

        public static char GetFileSymbol(CellFile file) {
            return "abcdefgh"[(int)file - 1];
        }

        public static char GetRankSymbol(int rank) {
            return "12345678"[rank - 1];
        }

        public static char GetRankSymbol(this CellRank rank) {
            return "12345678"[(int)rank - 1];
        }

        public static char GetFileSymbol(this CellName cell) {
            var file = GetFile(cell);
            return GetFileSymbol(file);
        }

        public static char GetRankSymbol(this CellName cell) {
            var file = GetRank(cell);
            return GetRankSymbol(file);
        }

        public static CellName GetCell(string cellName) {
            return GetCell(cellName[0], cellName[1]);
        }

        public static CellName GetCell(char column, char row) {
            int c, r;
            if (column >= 'a' && column <= 'h') {
                c = column - 'a';
            } else if (column >= 'A' && column <= 'H') {
                c = column - 'A';
            } else {
                throw new FormatException("Invalid column");
            }
            if (row >= '1' && row <= '8') {
                r = row - '1';
            } else {
                throw new FormatException("Invalid row");
            }
            return (CellName)(r * 8 + c);
        }

        public static CellName GetCell(int file, int rank) {
            return (CellName)((rank - 1) * 8 + (file - 1));
        }

        public static CellName GetCell(CellFile file, CellRank rank) {
            return (CellName)(((int)rank - 1) * 8 + (file - 1));
        }

        public static readonly CellName[] AllCells = {
            CellName.A1, CellName.A2, CellName.A3, CellName.A4, CellName.A5, CellName.A6, CellName.A7, CellName.A8,
            CellName.B1, CellName.B2, CellName.B3, CellName.B4, CellName.B5, CellName.B6, CellName.B7, CellName.B8,
            CellName.C1, CellName.C2, CellName.C3, CellName.C4, CellName.C5, CellName.C6, CellName.C7, CellName.C8,
            CellName.D1, CellName.D2, CellName.D3, CellName.D4, CellName.D5, CellName.D6, CellName.D7, CellName.D8,
            CellName.E1, CellName.E2, CellName.E3, CellName.E4, CellName.E5, CellName.E6, CellName.E7, CellName.E8,
            CellName.F1, CellName.F2, CellName.F3, CellName.F4, CellName.F5, CellName.F6, CellName.F7, CellName.F8,
            CellName.G1, CellName.G2, CellName.G3, CellName.G4, CellName.G5, CellName.G6, CellName.G7, CellName.G8,
            CellName.H1, CellName.H2, CellName.H3, CellName.H4, CellName.H5, CellName.H6, CellName.H7, CellName.H8
        };

        public static bool IsValidRank(char ch) {
            return ch >= '1' && ch <= '8';
        }

        //TODO: Unit test: upper case is not allowed
        public static bool IsValidFile(char ch) {
            return ch >= 'a' && ch <= 'h';
        }

        public static CellRank GetRank(char ch) {
            return (CellRank)(ch - '1' + 1);
        }

        public static CellFile GetFile(char ch) {
            return (CellFile)(ch - 'a' + 1);
        }

        public static string GetCellName(this CellName cell) {
            const string ranks = "12345678";
            const string files = "abcdefgh";
            int rank = ((int)cell) >> 3;
            int file = ((int)cell) & 0x7;
            string res = string.Empty;
            res += files[file];
            res += ranks[rank];
            return res;
        }

        public static CellName GetLongCastleToCell(PieceColor pieceColor) {
            switch (pieceColor) {
                case PieceColor.White:
                    return CellName.C1;
                case PieceColor.Black:
                    return CellName.C8;
                default:
                    throw new InvalidOperationException("Invalid piece color");
            }
        }

        public static CellName GetShortCastleToCell(PieceColor pieceColor) {
            switch (pieceColor) {
                case PieceColor.White:
                    return CellName.G1;
                case PieceColor.Black:
                    return CellName.G8;
                default:
                    throw new InvalidOperationException("Invalid piece color");
            }
        }

        public static CellName GetCastleFromCell(PieceColor pieceColor) {
            switch (pieceColor) {
                case PieceColor.White:
                    return CellName.E1;
                case PieceColor.Black:
                    return CellName.E8;
                default:
                    throw new InvalidOperationException("Invalid piece color");
            }
        }

    }
}