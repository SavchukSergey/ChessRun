using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    public static class BitBoard {

        public static BitBoardCell[] Cells;
        static BitBoard() {
            Cells = new BitBoardCell[64];
            for (var i = 0; i < 64; i++) {
                var cellName = (CellName)i;
                var cell = new BitBoardCell {
                    CellName = cellName,
                    Bit = 1ul << i,
                    Rank = (byte)(i >> 3),
                    File = (byte)(i & 0x07),
                    NorthWest = GetNorthWestMoves(cellName),
                    NorthEast = GetNorthEastMoves(cellName),
                    SouthWest = GetSouthWestMoves(cellName),
                    SouthEast = GetSouthEastMoves(cellName),
                    Horizontal = GetHorizontalMoves(cellName),
                    Vertical = GetVerticalMoves(cellName),
                    Knights = GetKnightMoves(cellName),
                    Kings = GetKingMoves(cellName),
                    WhitePawn = GetWhitePawnsMoves(cellName),
                    BlackPawn = GetBlackPawnsMoves(cellName),
                    HorizontalAttackers = GetHorizontalAttackers(cellName)
                };
                cell.RankInverted = (byte)(7 - cell.Rank);
                cell.FileInverted = (byte)(7 - cell.File);
                cell.Diagonals = cell.NorthWest | cell.NorthEast | cell.SouthWest | cell.SouthEast;
                cell.HorizontalVertical = cell.Horizontal | cell.Vertical;

                Cells[i] = cell;
            }
        }

        private static ulong[] GetHorizontalAttackers(CellName cell) {
            var res = new ulong[256];
            var i = (int)cell;
            var rank = i >> 3;
            var file = i & 0x07;
            for (var j = 0ul; j < 256; j++) {
                ulong mask = 0;
                for (var k = file + 1; k < 8; k++) {
                    var submask = 1ul << k;
                    mask |= submask;
                    if ((j & submask) != 0) break;
                }
                for (var k = file - 1; k >= 0; k--) {
                    var submask = 1ul << k;
                    mask |= submask;
                    if ((j & submask) != 0) break;
                }
                mask <<= rank * 8;
                res[j] = mask;
            }
            return res;
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

        private static ulong GetHorizontalMoves(CellName from) {
            var res = 0ul;
            var file = (int)from & 0x07;
            var index = from;
            for (var i = file + 1; i < 8; i++) {
                index = index.IncreaseFile();
                res |= index.Bit();
            }
            index = from;
            for (var i = file - 1; i >= 0; i--) {
                index = index.DecreaseFile();
                res |= index.Bit();
            }
            return res;
        }

        private static ulong GetVerticalMoves(CellName from) {
            var res = 0ul;
            var file = (int)from & 0x07;
            var index = from;
            for (var i = file + 1; i < 8; i++) {
                index = index.IncreaseRank();
                res |= index.Bit();
            }
            index = from;
            for (var i = file - 1; i >= 0; i--) {
                index = index.DecreaseRank();
                res |= index.Bit();
            }
            return res;
        }

        private static ulong GetBlackPawnsMoves(CellName from) {
            var res = 0ul;
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            if (rank < 7) {
                if (file > 0) {
                    res |= @from.IncreaseRank().DecreaseFile().Bit();
                }
                if (file < 7) {
                    res |= @from.IncreaseRank().IncreaseFile().Bit();
                }
            }
            return res;
        }

        private static ulong GetWhitePawnsMoves(CellName from) {
            var res = 0ul;
            int rank = (int)from >> 3;
            int file = (int)from & 0x07;
            if (rank > 0) {
                if (file > 0) {
                    res |= @from.DecreaseRank().DecreaseFile().Bit();
                }
                if (file < 7) {
                    res |= @from.DecreaseRank().IncreaseFile().Bit();
                }
            }
            return res;
        }

        private static ulong GetKnightMoves(CellName from) {
            var res = 0ul;
            var rank = @from.GetRank();
            var file = @from.GetFile();
            if (file <= CellFile.G && rank <= CellRank.R6) {
                var to = @from.Shift(1, 2);
                res |= to.Bit();
            }
            if (file <= CellFile.F && rank <= CellRank.R7) {
                var to = @from.Shift(2, 1);
                res |= to.Bit();
            }
            if (file <= CellFile.F && rank >= CellRank.R2) {
                var to = @from.Shift(2, -1);
                res |= to.Bit();
            }
            if (file <= CellFile.G && rank >= CellRank.R3) {
                var to = @from.Shift(1, -2);
                res |= to.Bit();
            }
            if (file >= CellFile.B && rank >= CellRank.R3) {
                var to = @from.Shift(-1, -2);
                res |= to.Bit();
            }
            if (file >= CellFile.C && rank >= CellRank.R2) {
                var to = @from.Shift(-2, -1);
                res |= to.Bit();
            }
            if (file >= CellFile.C && rank <= CellRank.R7) {
                var to = @from.Shift(-2, 1);
                res |= to.Bit();
            }
            if (file >= CellFile.B && rank <= CellRank.R6) {
                var to = @from.Shift(-1, 2);
                res |= to.Bit();
            }
            return res;
        }

        private static ulong GetKingMoves(CellName from) {
            var res = 0ul;
            var file = @from.GetFile();
            var rank = @from.GetRank();

            if (file >= CellFile.B && rank <= CellRank.R7) {
                res |= @from.Shift(-1, 1).Bit();
            }
            if (rank <= CellRank.R7) {
                res |= @from.Shift(0, 1).Bit();
            }
            if (file <= CellFile.G && rank <= CellRank.R7) {
                res |= @from.Shift(1, 1).Bit();
            }
            if (file <= CellFile.G) {
                res |= @from.Shift(1, 0).Bit();
            }
            if (file <= CellFile.G && rank >= CellRank.R2) {
                res |= @from.Shift(1, -1).Bit();
            }
            if (rank >= CellRank.R2) {
                res |= @from.Shift(0, -1).Bit();
            }
            if (file >= CellFile.B && rank >= CellRank.R2) {
                res |= @from.Shift(-1, -1).Bit();
            }
            if (file >= CellFile.B) {
                res |= @from.Shift(-1, 0).Bit();
            }
            return res;
        }

    }

}
