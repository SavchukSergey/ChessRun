using System;

namespace ChessRun.Engine.Moves {
    [Flags]
    public enum CastleFlags : byte {

        WhiteCanDoShortCastle = 0x01,
        WhiteCanDoLongCastle = 0x02,
        BlackCanDoShortCastle = 0x04,
        BlackCanDoLongCastle = 0x08

    }
}
