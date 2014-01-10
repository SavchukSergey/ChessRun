using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    public static class BitBoard {

        public static ulong[] KnightBitBoards = GetKnightBitBoards();
        public static ulong[] KingBitBoards = GetKingBitBoards();
        public static ulong[] DiagonalBitBoards = GetBitboards(GetDiagonalMoves);
        public static ulong[] HVBitBoards = GetHVBitBoards();
        public static ulong[] BlackPawnsBitBoards = GetBlackPawnsBitBoards();
        public static ulong[] WhitePawnsBitBoards = GetWhitePawnsBitBoards();
        public static ulong[] HorizontalAttackers = GetHorizontalAttackers();

        public static ulong[] NorthEast = GetBitboards(GetNorthEastMoves);
        public static ulong[] NorthWest = GetBitboards(GetNorthWestMoves);
        public static ulong[] SouthEast = GetBitboards(GetSouthEastMoves);
        public static ulong[] SouthWest = GetBitboards(GetSouthWestMoves);

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

        private static ulong[] GetBitboards(Func<CellName, ulong> builder) {
            var res = new ulong[64];
            for (var i = 0; i < 64; i++) {
                var cell = (CellName)i;
                res[i] = builder(cell);
            }
            return res;
        }

        private static ulong GetDiagonalMoves(CellName from) {
            var nw = GetNorthWestMoves(from);
            var ne = GetNorthEastMoves(from);
            var sw = GetSouthWestMoves(from);
            var se =  GetSouthEastMoves(from);
            return nw | ne | sw | se;
        }

        private static ulong GetSouthEastMoves(CellName from) {
            var res = 0ul;
            while (from.GetRank() > CellRank.R1 && from.GetFile() < CellFile.H) {
                from = from.DecreaseRank().IncreaseFile();
                res |= from.Bit();
            }

            return res;
        }

        private static ulong GetSouthWestMoves(CellName from) {
            var res = 0ul;
            while (from.GetRank() > CellRank.R1 && from.GetFile() > CellFile.A) {
                from = from.DecreaseRank().DecreaseFile();
                res |= from.Bit();
            }

            return res;
        }

        private static ulong GetNorthEastMoves(CellName from) {
            var res = 0ul;
            while (from.GetRank() < CellRank.R8 && from.GetFile() < CellFile.H) {
                from = from.IncreaseRank().IncreaseFile();
                res |= from.Bit();
            }

            return res;
        }

        private static ulong GetNorthWestMoves(CellName from) {
            var res = 0ul;
            while (from.GetRank() < CellRank.R8 && from.GetFile() > CellFile.A) {
                from = from.IncreaseRank().DecreaseFile();
                res |= from.Bit();
            }

            return res;
        }

        private static void AddGeneralHVMoves(ulong[] bitboards, CellName from) {
            var rank = (int)from >> 3;
            var file = (int)from & 0x07;
            var index = (int)from;
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
            var rank = @from.GetRank();
            var file = @from.GetFile();
            if (file <= CellFile.G && rank <= CellRank.R6) {
                var to = @from.Shift(1, 2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.F && rank <= CellRank.R7) {
                var to = @from.Shift(2, 1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.F && rank >= CellRank.R2) {
                var to = @from.Shift(2, -1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file <= CellFile.G && rank >= CellRank.R3) {
                var to = @from.Shift(1, -2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.B && rank >= CellRank.R3) {
                var to = @from.Shift(-1, -2);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.C && rank >= CellRank.R2) {
                var to = @from.Shift(-2, -1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.C && rank <= CellRank.R7) {
                var to = @from.Shift(-2, 1);
                AddGeneralMove(bitboards, from, to);
            }
            if (file >= CellFile.B && rank <= CellRank.R6) {
                var to = @from.Shift(-1, 2);
                AddGeneralMove(bitboards, from, to);
            }
        }

        private static void AddGeneralKingMoves(ulong[] bitboards, CellName from) {
            var file = @from.GetFile();
            var rank = @from.GetRank();

            if (file >= CellFile.B && rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, @from.Shift(-1, 1));
            }
            if (rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, @from.Shift(0, 1));
            }
            if (file <= CellFile.G && rank <= CellRank.R7) {
                AddGeneralMove(bitboards, from, @from.Shift(1, 1));
            }
            if (file <= CellFile.G) {
                AddGeneralMove(bitboards, from, @from.Shift(1, 0));
            }
            if (file <= CellFile.G && rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, @from.Shift(1, -1));
            }
            if (rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, @from.Shift(0, -1));
            }
            if (file >= CellFile.B && rank >= CellRank.R2) {
                AddGeneralMove(bitboards, from, @from.Shift(-1, -1));
            }
            if (file >= CellFile.B) {
                AddGeneralMove(bitboards, from, @from.Shift(-1, 0));
            }
        }

        private static void AddGeneralMove(ulong[] bitboards, CellName from, CellName to) {
            var mask = to.Bit();
            bitboards[(int)from] |= mask;
        }

    }

}
