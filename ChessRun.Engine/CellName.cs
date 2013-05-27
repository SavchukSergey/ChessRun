namespace ChessRun.Engine {
    public enum CellName : sbyte {
        None = -1,

        A1 = 0x00,
        A2 = 0x08,
        A3 = 0x10,
        A4 = 0x18,
        A5 = 0x20,
        A6 = 0x28,
        A7 = 0x30,
        A8 = 0x38,

        B1 = 0x01,
        B2 = 0x09,
        B3 = 0x11,
        B4 = 0x19,
        B5 = 0x21,
        B6 = 0x29,
        B7 = 0x31,
        B8 = 0x39,

        C1 = 0x02,
        C2 = 0x0a,
        C3 = 0x12,
        C4 = 0x1a,
        C5 = 0x22,
        C6 = 0x2a,
        C7 = 0x32,
        C8 = 0x3a,

        D1 = 0x03,
        D2 = 0x0b,
        D3 = 0x13,
        D4 = 0x1b,
        D5 = 0x23,
        D6 = 0x2b,
        D7 = 0x33,
        D8 = 0x3b,

        E1 = 0x04,
        E2 = 0x0c,
        E3 = 0x14,
        E4 = 0x1c,
        E5 = 0x24,
        E6 = 0x2c,
        E7 = 0x34,
        E8 = 0x3c,

        F1 = 0x05,
        F2 = 0x0d,
        F3 = 0x15,
        F4 = 0x1d,
        F5 = 0x25,
        F6 = 0x2d,
        F7 = 0x35,
        F8 = 0x3d,

        G1 = 0x06,
        G2 = 0x0e,
        G3 = 0x16,
        G4 = 0x1e,
        G5 = 0x26,
        G6 = 0x2e,
        G7 = 0x36,
        G8 = 0x3e,

        H1 = 0x07,
        H2 = 0x0f,
        H3 = 0x17,
        H4 = 0x1f,
        H5 = 0x27,
        H6 = 0x2f,
        H7 = 0x37,
        H8 = 0x3f

    }

    public enum CellFile {
        None = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8
    }

    public enum CellRank {
        None = 0,
        R1 = 1,
        R2 = 2,
        R3 = 3,
        R4 = 4,
        R5 = 5,
        R6 = 6,
        R7 = 7,
        R8 = 8
    }
}