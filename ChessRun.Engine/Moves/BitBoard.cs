using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    public static class BitBoard {

        public static ulong[] KnightBitBoards = GetKnightBitBoards();
        public static ulong[] KingBitBoards = GetKingBitBoards();
        public static ulong[] DiagonalBitBoards = GetDiagonalBitBoards();
        public static ulong[] HVBitBoards = GetHVBitBoards();
        public static ulong[] BlackPawnsBitBoards = GetBlackPawnsBitBoards();
        public static ulong[] WhitePawnsBitBoards = GetWhitePawnsBitBoards();
        public static ulong[] HorizontalAttackers = GetHorizontalAttackers();

        public static CellName[] Rotation90Map = GetRotated90Map();

        private static CellName[] GetRotated90Map() {
            var res = new CellName[64];
            for (var i = 0; i < 64; i++) {
                var rank = i >> 3;
                var file = i & 0x07;
                var target = (CellName)(file * 8 + rank);
                res[i] = target;
            }
            return res;
        }

        private static ulong[] GetHorizontalAttackers() {
            var res = new ulong[64 * 256];
            for (var i = 0; i < 64; i++) {
                var rank = i >> 3;
                var file = i & 0x07;
                for (var j = 0; j < 256; j++) {
                    ulong mask = 0;
                    for (var k = file + 1; k < 8; k++) {
                        var submask = 1 << k;
                        mask |= (ulong)submask;
                        if ((j & submask) != 0) break;
                    }
                    for (var k = file - 1; k >= 0; k--) {
                        var submask = 1 << k;
                        mask |= (ulong)submask;
                        if ((j & submask) != 0) break;
                    }
                    mask <<= rank * 8;
                    res[i * 256 + j] = mask;
                }
            }
            return res;
        }

        private static ulong[] GetKnightBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralKnightMoves(res, cell);
            }
            return res;
        }

        private static ulong[] GetKingBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralKingMoves(res, cell);
            }
            return res;
        }

        private static ulong[] GetDiagonalBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralDiagonalMoves(res, cell);
            }
            return res;
        }

        private static ulong[] GetHVBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralHVMoves(res, cell);
            }
            return res;
        }

        private static ulong[] GetBlackPawnsBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralBlackPawnsMoves(res, cell);
            }
            return res;
        }

        private static ulong[] GetWhitePawnsBitBoards() {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                AddGeneralWhitePawnsMoves(res, cell);
            }
            return res;
        }

        private static void AddGeneralDiagonalMoves(ulong[] bitboards, CellName from) {
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            int rank7 = 7 - rank;
            int file7 = 7 - file;

            int len;
            int index = (int)from;
            for (len = rank > file ? rank7 : file7; len > 0; len--) {
                index += 9;
                AddGeneralMove(bitboards, from, (CellName)index);
            }

            index = (int)from;
            for (len = rank > file7 ? rank7 : file; len > 0; len--) {
                index += 7;
                AddGeneralMove(bitboards, from, (CellName)index);
            }

            index = (int)from;
            for (len = rank < file ? rank : file; len > 0; len--) {
                index -= 9;
                AddGeneralMove(bitboards, from, (CellName)index);
            }

            index = (int)from;
            for (len = rank < file7 ? rank : file7; len > 0; len--) {
                index -= 7;
                AddGeneralMove(bitboards, from, (CellName)index);
            }
        }

        private static void AddGeneralHVMoves(ulong[] bitboards, CellName from) {
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            int index = (int)from;
            for (var i = file + 1; i < 8; i++) {
                index++;
                AddGeneralMove(bitboards, from, (CellName)index);
            }
            index = (int)from;
            for (var i = file - 1; i >= 0; i--) {
                index--;
                AddGeneralMove(bitboards, from, (CellName)index);
            }
            index = (int)from;
            for (var i = rank + 1; i < 8; i++) {
                index += 8;
                AddGeneralMove(bitboards, from, (CellName)index);
            }
            index = (int)from;
            for (var i = rank - 1; i >= 0; i--) {
                index -= 8;
                AddGeneralMove(bitboards, from, (CellName)index);
            }
        }

        private static void AddGeneralBlackPawnsMoves(ulong[] bitboards, CellName from) {
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            if (rank < 7) {
                if (file > 0) {
                    AddGeneralMove(bitboards, from, (CellName)(int)(from) + 8 - 1);
                }
                if (file < 7) {
                    AddGeneralMove(bitboards, from, (CellName)(int)(from) + 8 + 1);
                }
            }
        }

        private static void AddGeneralWhitePawnsMoves(ulong[] bitboards, CellName from) {
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            if (rank > 0) {
                if (file > 0) {
                    AddGeneralMove(bitboards, from, (CellName)(int)(from) - 8 - 1);
                }
                if (file < 7) {
                    AddGeneralMove(bitboards, from, (CellName)(int)(from) - 8 + 1);
                }
            }
        }

        private static void AddGeneralKnightMoves(ulong[] bitboards, CellName from) {
            var rank = CellOperations.GetRank(from);
            var file = CellOperations.GetFile(from);
            if (file <= CellFile.G && rank <= CellRank.R6) {
                var to = CellOperations.Shift(from, 1, 2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.F && rank <= CellRank.R7) {
                var to = CellOperations.Shift(from, 2, 1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.F && rank >= CellRank.R2) {
                var to = CellOperations.Shift(from, 2, -1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.G && rank >= CellRank.R3) {
                var to = CellOperations.Shift(from, 1, -2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.B && rank >= CellRank.R3) {
                var to = CellOperations.Shift(from, -1, -2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.C && rank >= CellRank.R2) {
                var to = CellOperations.Shift(from, -2, -1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.C && rank <= CellRank.R7) {
                var to = CellOperations.Shift(from, -2, 1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.B && rank <= CellRank.R6) {
                var to = CellOperations.Shift(from, -1, 2);
                AddGeneralMove(bitboards, from, to);
            }
        }

        private static void AddGeneralKingMoves(ulong[] bitboards, CellName from) {
            var file = CellOperations.GetFile(from);
            var rank = CellOperations.GetRank(from);

            if (file >= CellFile.B && rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, -1, 1));
            }
            if (rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, 0, 1));
            }
            if (file <= CellFile.G && rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, 1, 1));
            }
            if (file <= CellFile.G) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, 1, 0));
            }
            if (file <= CellFile.G && rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, 1, -1));
            }
            if (rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, 0, -1));
            }
            if (file >= CellFile.B && rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, -1, -1));
            }
            if (file >= CellFile.B) {
                AddGeneralMove(bitboards, from, CellOperations.Shift(from, -1, 0));
            }
        }

        private static void AddGeneralMove(ulong[] bitboards, CellName from, CellName to) {
            var mask = 0x1ul << (int)to;
            bitboards[(int)from] |= mask;
        }

    }

}
