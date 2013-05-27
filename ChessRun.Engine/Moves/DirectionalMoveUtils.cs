using System;
using System.Collections.Generic;
using System.Linq;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    public static class DirectionalMoveUtils {

        public static CellName[] GetMiddleCells(CellName from, CellName to) {
            var rankFrom = CellOperations.GetRank(from);
            var fileFrom = CellOperations.GetFile(from);

            var rankTo = CellOperations.GetRank(to);
            var fileTo = CellOperations.GetFile(to);

            var dFile = fileTo - fileFrom;
            var dRank = rankTo - rankFrom;
            if (dRank == 0 || dFile == 0 || Math.Abs(dRank) == Math.Abs(dFile)) {
                if (dFile > 0) dFile = 1;
                if (dFile < 0) dFile = -1;
                if (dRank > 0) dRank = 1;
                if (dRank < 0) dRank = -1;
                var diff = dRank * 8 + dFile;
                IList<CellName> middle = new List<CellName>();
                while (to != from) {
                    from = (CellName)((int)from + diff);
                    if (from != to) middle.Add(from);
                }
                return middle.ToArray();
            }
            throw new InvalidOperationException("Only horizontal, vertical or diagonal lines allowed");
        }

    }
}