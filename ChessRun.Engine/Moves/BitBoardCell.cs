namespace ChessRun.Engine.Moves {
    public class BitBoardCell {

        public ulong NorthEast;

        public ulong NorthWest;

        public ulong SouthEast;

        public ulong SouthWest;

        public ulong Knights;

        public ulong Kings;

        public ulong BlackPawn;

        public ulong Bit;

        public CellName CellName;

        public byte Rank;

        public byte File;

        public byte RankInverted;

        public byte FileInverted;


    }
}
