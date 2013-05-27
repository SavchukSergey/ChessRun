using System;
using System.Linq;
using ChessRun.Engine.Moves.Bishop;
using ChessRun.Engine.Moves.King;
using ChessRun.Engine.Moves.Knight;
using ChessRun.Engine.Moves.Pawn;
using ChessRun.Engine.Moves.Queen;
using ChessRun.Engine.Moves.Rook;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    public static class MoveMatrix {

        public static readonly SpeculativeMove[][] DirectMatrix;

        public static readonly SpeculativeMove BlackShortCastle = new BlackShortCastleMove();
        public static readonly SpeculativeMove BlackLongCastle = new BlackLongCastleMove();
        public static readonly SpeculativeMove WhiteShortCastle = new WhiteShortCastleMove();
        public static readonly SpeculativeMove WhiteLongCastle = new WhiteLongCastleMove();

        private class BreakMove : SpeculativeMove {
            public BreakMove(CellName from)
                : base(from, CellName.None) {
            }

            public override ValidationResult FastValidate(ChessBoard board) {
                throw new InvalidOperationException();
            }

            public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
                throw new InvalidOperationException();
            }

            public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
                throw new InvalidOperationException();
            }

            protected override string NotationSymbol
            {
                get { return ""; }
            }
        }

        static MoveMatrix() {
            DirectMatrix = new SpeculativeMove[15 * 64][];
            for (var i=0; i < 64; i++) {
                var cell = (CellName)i;

                AddBlackPawnMoves(cell);
                AddBlackBishopMoves(cell);
                AddBlackKnightMoves(cell);
                AddBlackRookMoves(cell);
                AddBlackQueenMoves(cell);
                AddBlackKingMoves(cell);

                AddWhitePawnMoves(cell);
                AddWhiteBishopMoves(cell);
                AddWhiteKnightMoves(cell);
                AddWhiteRookMoves(cell);
                AddWhiteQueenMoves(cell);
                AddWhiteKingMoves(cell);

            }
            BindMoves();
        }

        private static void BindMoves() {
            for (var piece = 1; piece <= 14; piece++) {
                for (var cell = 0; cell < 64; cell++) {
                    var index = piece * 64 + cell;
                    DirectMatrix[index] = BindMovesInList(DirectMatrix[index]);
                }
            }
        }

        private static SpeculativeMove[] BindMovesInList(SpeculativeMove[] moves) {
            if (moves == null) return null;
            for (var j = 0; j < moves.Length; j++) {
                var move = moves[j];
                var nextIndependent = GetFirstIndpendent(moves, j + 1);
                move.NextGroupMove = nextIndependent;
                move.NextMove = moves.Skip(j + 1).FirstOrDefault(item => !(item is BreakMove));
            }
            moves = moves.Where(item => !(item is BreakMove)).ToArray();
            return moves;
        }

        private static SpeculativeMove GetFirstIndpendent(SpeculativeMove[] moves, int startIndex) {
            return moves.Skip(startIndex).SkipWhile(item => !(item is BreakMove)).FirstOrDefault(item => !(item is BreakMove));
        }

        #region Black Pawns

        private static void AddBlackPawnMoves(CellName from) {
            var rank = CellOperations.GetRank(from);
            switch (rank) {
                case CellRank.R1:
                case CellRank.R8:
                    break;
                case CellRank.R7:
                    AddBlackPawnMovesRank7(from);
                    break;
                case CellRank.R6:
                case CellRank.R5:
                case CellRank.R3:
                    AddBlackPawnMovesRanks653(from);
                    break;
                case CellRank.R4:
                    AddBlackPawnMovesRank4(from);
                    break;
                case CellRank.R2:
                    AddBlackPawnMovesRank2(from);
                    break;
            }
        }

        private static void AddBlackCaptureMoves(CellName from) {
            const PieceType piece = PieceType.BlackPawn;
            var file = CellOperations.GetFile(from);
            var next = CellOperations.DecreaseRank(from);
            if (file > CellFile.A) {
                AddMove(piece, new BlackPawnCaptureMove(from, CellOperations.DecreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
            if (file < CellFile.H) {
                AddMove(piece, new BlackPawnCaptureMove(from, CellOperations.IncreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
        }

        private static void AddBlackPawnMovesRank7(CellName from) {
            const PieceType piece = PieceType.BlackPawn;
            AddMove(piece, new BlackPawnRegularMove(from));
            AddMove(piece, new BlackPawnDoubleMove(from));
            AddMove(piece, new BreakMove(from));
            AddBlackCaptureMoves(from);
        }

        private static void AddBlackPawnMovesRanks653(CellName from) {
            const PieceType piece = PieceType.BlackPawn;
            AddMove(piece, new BlackPawnRegularMove(from));
            AddMove(piece, new BreakMove(from));
            AddBlackCaptureMoves(from);
        }

        private static void AddBlackPawnMovesRank4(CellName from) {
            const PieceType piece = PieceType.BlackPawn;
            var file = CellOperations.GetFile(from);
            var next = CellOperations.DecreaseRank(from);

            AddMove(piece, new BlackPawnRegularMove(from));
            AddMove(piece, new BreakMove(from));
            AddBlackCaptureMoves(from);

            if (file > CellFile.A) {
                AddMove(piece, new BlackPawnEnPassantCaptureMove(from, CellOperations.DecreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
            if (file < CellFile.H) {
                AddMove(piece, new BlackPawnEnPassantCaptureMove(from, CellOperations.IncreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
        }

        private static void AddBlackPawnMovesRank2(CellName from) {
            const PieceType piece = PieceType.BlackPawn;
            var next = CellOperations.DecreaseRank(from);
            AddPromotionMoves(piece, from, next, (to, promotion) => new BlackPawnPromotionMove(from, promotion));
            AddPromotionCaptureMoves(piece, from, next, (to, promotion) => new BlackPawnPromotionCaptureMove(from, to, promotion));
        }

        #endregion

        #region White Pawns

        private static void AddWhitePawnMoves(CellName from) {
            var rank = CellOperations.GetRank(from);
            switch (rank) {
                case CellRank.R1:
                case CellRank.R8:
                    break;
                case CellRank.R2:
                    AddWhitePawnMovesRank2(from);
                    break;
                case CellRank.R3:
                case CellRank.R4:
                case CellRank.R6:
                    AddWhitePawnMovesRanks346(from);
                    break;
                case CellRank.R5:
                    AddWhitePawnMovesRank5(from);
                    break;
                case CellRank.R7:
                    AddWhitePawnMovesRank7(from);
                    break;
            }
        }

        private static void AddWhiteCaptureMoves(CellName from) {
            const PieceType piece = PieceType.WhitePawn;
            var file = CellOperations.GetFile(from);
            var next = CellOperations.IncreaseRank(from);
            if (file > CellFile.A) {
                AddMove(piece, new WhitePawnCaptureMove(from, CellOperations.DecreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
            if (file < CellFile.H) {
                AddMove(piece, new WhitePawnCaptureMove(from, CellOperations.IncreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
        }

        private static void AddWhitePawnMovesRank2(CellName from) {
            const PieceType piece = PieceType.WhitePawn;
            AddMove(piece, new WhitePawnRegularMove(from));
            AddMove(piece, new WhitePawnDoubleMove(from));
            AddMove(piece, new BreakMove(from));
            AddWhiteCaptureMoves(from);
        }

        private static void AddWhitePawnMovesRanks346(CellName from) {
            const PieceType piece = PieceType.WhitePawn;
            AddMove(piece, new WhitePawnRegularMove(from));
            AddMove(piece, new BreakMove(from));
            AddWhiteCaptureMoves(from);
        }

        private static void AddWhitePawnMovesRank5(CellName from) {
            const PieceType piece = PieceType.WhitePawn;
            var file = CellOperations.GetFile(from);
            var next = CellOperations.IncreaseRank(from);

            AddMove(piece, new WhitePawnRegularMove(from));
            AddMove(piece, new BreakMove(from));
            AddWhiteCaptureMoves(from);

            if (file > CellFile.A) {
                AddMove(piece, new WhitePawnEnPassantCaptureMove(from, CellOperations.DecreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
            if (file < CellFile.H) {
                AddMove(piece, new WhitePawnEnPassantCaptureMove(from, CellOperations.IncreaseFile(next)));
                AddMove(piece, new BreakMove(from));
            }
        }

        private static void AddWhitePawnMovesRank7(CellName from) {
            const PieceType piece = PieceType.WhitePawn;
            var next = CellOperations.IncreaseRank(from);
            AddPromotionMoves(piece, from, next, (to, promotion) => new WhitePawnPromotionMove(from, promotion));
            AddPromotionCaptureMoves(piece, from, next, (to, promotion) => new WhitePawnPromotionCaptureMove(from, to, promotion));
        }

        #endregion

        private static void AddPromotionMoves(PieceType piece, CellName from, CellName next, Func<CellName, PieceType, SpeculativeMove> factory) {
            var color = PieceOperations.GetColor(piece);
            AddMove(piece, factory(next, PieceOperations.GetKnight(color)));
            AddMove(piece, factory(next, PieceOperations.GetBishop(color)));
            AddMove(piece, factory(next, PieceOperations.GetRook(color)));
            AddMove(piece, factory(next, PieceOperations.GetQueen(color)));
            AddMove(piece, new BreakMove(from));
        }

        private static void AddPromotionCaptureMoves(PieceType piece, CellName from, CellName next, Func<CellName, PieceType, SpeculativeMove> factory) {
            var file = CellOperations.GetFile(from);
            var color = PieceOperations.GetColor(piece);
            if (file > CellFile.A) {
                var left = CellOperations.DecreaseFile(next);
                AddMove(piece, factory(left, PieceOperations.GetKnight(color)));
                AddMove(piece, factory(left, PieceOperations.GetBishop(color)));
                AddMove(piece, factory(left, PieceOperations.GetRook(color)));
                AddMove(piece, factory(left, PieceOperations.GetQueen(color)));
                AddMove(piece, new BreakMove(from));
            }
            if (file < CellFile.H) {
                var right = CellOperations.IncreaseFile(next);
                AddMove(piece, factory(right, PieceOperations.GetKnight(color)));
                AddMove(piece, factory(right, PieceOperations.GetBishop(color)));
                AddMove(piece, factory(right, PieceOperations.GetRook(color)));
                AddMove(piece, factory(right, PieceOperations.GetQueen(color)));
                AddMove(piece, new BreakMove(from));
            }
        }

        private static void AddWhiteKnightMoves(CellName from) {
            const PieceType piece = PieceType.WhiteKnight;
            AddKnightMoves(piece, from, to => new WhiteKnightMove(from, to));
        }

        private static void AddBlackKnightMoves(CellName from) {
            const PieceType piece = PieceType.BlackKnight;
            AddKnightMoves(piece, from, to => new BlackKnightMove(from, to));
        }

        private static void AddKnightMoves(PieceType piece, CellName from, Func<CellName, KnightMove> factory) {
            var mask = BitBoard.KnightBitBoards[(int)from];
            var scan = 1ul;
            for (var i = 0; i < 64; i++) {
                if ((mask & scan) != 0) {
                    var to = (CellName)i;
                    AddMove(piece, factory(to));
                    AddMove(piece, new BreakMove(from));
                }
                scan <<= 1;
            }
        }

        private static void AddWhiteBishopMoves(CellName from) {
            const PieceType piece = PieceType.WhiteBishop;
            AddBishopMoves(piece, from, to => new WhiteBishopMove(from, to));
        }

        private static void AddBlackBishopMoves(CellName from) {
            const PieceType piece = PieceType.BlackBishop;
            AddBishopMoves(piece, from, to => new BlackBishopMove(from, to));
        }

        private static void AddBishopMoves(PieceType piece, CellName from, Func<CellName, BishopMove> factory) {
            AddDiagonalMoves(piece, from, to => factory(to));
        }

        private static void AddWhiteRookMoves(CellName from) {
            const PieceType piece = PieceType.WhiteRook;
            AddRookMoves(piece, from, to => new WhiteRookRegularMove(from, to));
        }

        private static void AddBlackRookMoves(CellName from) {
            const PieceType piece = PieceType.BlackRook;
            AddRookMoves(piece, from, to => new BlackRookRegularMove(from, to));
        }

        private static void AddRookMoves(PieceType piece, CellName from, Func<CellName, RookMove> factory) {
            AddHorizontalMoves(piece, from, to => factory(to));
            AddVerticalMoves(piece, from, to => factory(to));
        }

        private static void AddWhiteQueenMoves(CellName from) {
            const PieceType piece = PieceType.WhiteQueen;
            AddQueenMoves(piece, from, to => new WhiteQueenMove(from, to));
        }

        private static void AddBlackQueenMoves(CellName from) {
            const PieceType piece = PieceType.BlackQueen;
            AddQueenMoves(piece, from, to => new BlackQueenMove(from, to));
        }

        private static void AddQueenMoves(PieceType piece, CellName cell, Func<CellName, QueenMove> factory) {
            AddDiagonalMoves(piece, cell, to => factory(to));
            AddHorizontalMoves(piece, cell, to => factory(to));
            AddVerticalMoves(piece, cell, to => factory(to));
        }

        private static void AddWhiteKingMoves(CellName from) {
            const PieceType piece = PieceType.WhiteKing;
            if (from == CellName.E1) {
                AddMove(piece, WhiteLongCastle);
                AddMove(piece, new BreakMove(from));
                AddMove(piece, WhiteShortCastle);
                AddMove(piece, new BreakMove(from));
            }
            AddKingRegularMoves(piece, from, to => new WhiteKingRegularMove(from, to));
        }

        private static void AddBlackKingMoves(CellName from) {
            const PieceType piece = PieceType.BlackKing;
            if (from == CellName.E8) {
                AddMove(piece, BlackLongCastle);
                AddMove(piece, new BreakMove(from));
                AddMove(piece, BlackShortCastle);
                AddMove(piece, new BreakMove(from));
            }
            AddKingRegularMoves(piece, from, to => new BlackKingRegularMove(from, to));
        }

        private static void AddKingRegularMoves(PieceType piece, CellName from, Func<CellName, SpeculativeMove> factory) {
            var mask = BitBoard.KingBitBoards[(int)from];
            var scan = 1ul;
            for (var i = 0; i < 64; i++) {
                if ((mask & scan) != 0) {
                    var to = (CellName)i;
                    AddMove(piece, factory(to));
                    AddMove(piece, new BreakMove(from));
                }
                scan <<= 1;
            }
        }

        private static void AddDiagonalMoves(PieceType piece, CellName from, Func<CellName, SpeculativeMove> factory) {
            AddDirectionalMoves(piece, from, 1, 1, factory);
            AddDirectionalMoves(piece, from, 1, -1, factory);
            AddDirectionalMoves(piece, from, -1, 1, factory);
            AddDirectionalMoves(piece, from, -1, -1, factory);
        }

        private static void AddHorizontalMoves(PieceType piece, CellName from, Func<CellName, SpeculativeMove> factory) {
            AddDirectionalMoves(piece, from, 1, 0, factory);
            AddDirectionalMoves(piece, from, -1, 0, factory);
        }

        private static void AddVerticalMoves(PieceType piece, CellName from, Func<CellName, SpeculativeMove> factory) {
            AddDirectionalMoves(piece, from, 0, 1, factory);
            AddDirectionalMoves(piece, from, 0, -1, factory);
        }

        private static void AddDirectionalMoves(PieceType piece, CellName from, int fileShift, int rankShift, Func<CellName, SpeculativeMove> factory) {
            var cell = from;
            for (int i = 0; i < 8; i++) {
                var rank = CellOperations.GetRank(cell);
                var file = CellOperations.GetFile(cell);
                rank += rankShift;
                file += fileShift;
                if (rank >= CellRank.R1 && rank <= CellRank.R8 && file >= CellFile.A && file <= CellFile.H) {
                    cell = CellOperations.GetCell(file, rank);
                    AddMove(piece, factory(cell));
                }
            }
            AddMove(piece, new BreakMove(from));
        }

        private static void AddMove(PieceType piece, SpeculativeMove move) {
            var index = (int)piece * 64 + (int)move.From;
            var arr = DirectMatrix[index];
            if (arr == null) {
                DirectMatrix[index] = new[] { move };
            } else {
                var list = arr.ToList();
                list.Add(move);
                DirectMatrix[index] = list.ToArray();
            }
        }

        public static SpeculativeMove[] GetDirectMoves(PieceType piece, CellName from) {
            var index = (int)piece * 64 + (int)from;
            return DirectMatrix[index];
        }

        public static SpeculativeMove GetDirectFirstMove(PieceType piece, CellName from) {
            var index = (int)piece * 64 + (int)from;
            var moves = DirectMatrix[index];
            return moves != null && moves.Length > 0 ? moves[0] : null;
        }

        public static SpeculativeMove GetSpeculativeMove(PieceType piece, CellName from, CellName to) {
            var index = (int)piece * 64 + (int)from;
            var moves = DirectMatrix[index];
            for (var i = 0; i < moves.Length; i++) {
                var move = moves[i];
                if (move.To == to) return move;
            }
            return null;
        }
    }
}