using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ChessRun.Engine.Moves;
using ChessRun.Engine.Utils;
using ChessRun.Engine.Utils.Iterators;

namespace ChessRun.Engine {
    [Serializable]
    public class ChessBoard {
        //TODO: precacluate check after opponent move

        private readonly PieceType[] _cells = new PieceType[64];

        public CastleFlags Castles;
        public CellName EnPassantMove;

        public void Reset() {
            BFEN.Setup(this, BFEN.INITIAL_POSITION);
            EnPassantMove = CellName.None;
        }

        public ChessBoard() {
            Turn = PieceColor.White;
            EnPassantMove = CellName.None;
            Castles = CastleFlags.All;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhitePawn(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Pawns &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhiteKnight(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Knights &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhiteBishop(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Bishops &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhiteRook(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Rooks &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhiteQueen(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Queens &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearWhiteKing(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Whites.Kings &= negMask;
            Whites.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackPawn(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Pawns &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackKnight(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Knights &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackBishop(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Bishops &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackRook(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Rooks &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackQueen(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Queens &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearBlackKing(CellName cell) {
            var mask = 0x1ul << (int)cell;
            var negMask = ~mask;
            Blacks.Kings &= negMask;
            Blacks.Pieces &= negMask;
            _cells[(int)cell] = PieceType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhitePawn(CellName cell) {
            _cells[(int)cell] = PieceType.WhitePawn;
            var mask = 0x1ul << (int)cell;
            Whites.Pawns |= mask;
            Whites.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhiteKnight(CellName cell) {
            _cells[(int)cell] = PieceType.WhiteKnight;
            var mask = 0x1ul << (int)cell;
            Whites.Knights |= mask;
            Whites.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhiteBishop(CellName cell) {
            _cells[(int)cell] = PieceType.WhiteBishop;
            var mask = 0x1ul << (int)cell;
            Whites.Bishops |= mask;
            Whites.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhiteRook(CellName cell) {
            _cells[(int)cell] = PieceType.WhiteRook;
            var mask = 0x1ul << (int)cell;
            Whites.Rooks |= mask;
            Whites.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhiteQueen(CellName cell) {
            _cells[(int)cell] = PieceType.WhiteQueen;
            var mask = 0x1ul << (int)cell;
            Whites.Queens |= mask;
            Whites.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWhiteKing(CellName cell) {
            _cells[(int)cell] = PieceType.WhiteKing;
            var mask = 0x1ul << (int)cell;
            Whites.Kings |= mask;
            Whites.Pieces |= mask;
            WhiteKingPosition = cell;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackPawn(CellName cell) {
            _cells[(int)cell] = PieceType.BlackPawn;
            var mask = 0x1ul << (int)cell;
            Blacks.Pawns |= mask;
            Blacks.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackKnight(CellName cell) {
            _cells[(int)cell] = PieceType.BlackKnight;
            var mask = 0x1ul << (int)cell;
            Blacks.Knights |= mask;
            Blacks.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackBishop(CellName cell) {
            _cells[(int)cell] = PieceType.BlackBishop;
            var mask = 0x1ul << (int)cell;
            Blacks.Bishops |= mask;
            Blacks.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackRook(CellName cell) {
            _cells[(int)cell] = PieceType.BlackRook;
            var mask = 0x1ul << (int)cell;
            Blacks.Rooks |= mask;
            Blacks.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackQueen(CellName cell) {
            _cells[(int)cell] = PieceType.BlackQueen;
            var mask = 0x1ul << (int)cell;
            Blacks.Queens |= mask;
            Blacks.Pieces |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlackKing(CellName cell) {
            _cells[(int)cell] = PieceType.BlackKing;
            var mask = 0x1ul << (int)cell;
            Blacks.Kings |= mask;
            Blacks.Pieces |= mask;
            BlackKingPosition = cell;
        }

        public void ClearCell(CellName cell) {
            var oldValue = _cells[(int)cell];
            switch (oldValue) {
                case PieceType.WhiteKing: ClearWhiteKing(cell); break;
                case PieceType.WhiteKnight: ClearWhiteKnight(cell); break;
                case PieceType.WhiteBishop: ClearWhiteBishop(cell); break;
                case PieceType.WhiteQueen: ClearWhiteQueen(cell); break;
                case PieceType.WhiteRook: ClearWhiteRook(cell); break;
                case PieceType.WhitePawn: ClearWhitePawn(cell); break;
                case PieceType.BlackKing: ClearBlackKing(cell); break;
                case PieceType.BlackKnight: ClearBlackKnight(cell); break;
                case PieceType.BlackBishop: ClearBlackBishop(cell); break;
                case PieceType.BlackQueen: ClearBlackQueen(cell); break;
                case PieceType.BlackRook: ClearBlackRook(cell); break;
                case PieceType.BlackPawn: ClearBlackPawn(cell); break;
            }
            _cells[(int)cell] = PieceType.None;
        }

        public PieceType this[CellName cell] {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _cells[(int)cell]; }
            set {
                ClearCell(cell);
                _cells[(int)cell] = value;
                var mask = 0x1ul << (int)cell;

                switch (value) {
                    case PieceType.WhiteKing: SetWhiteKing(cell); break;
                    case PieceType.WhiteKnight: SetWhiteKnight(cell); break;
                    case PieceType.WhiteBishop: SetWhiteBishop(cell); break;
                    case PieceType.WhiteQueen: SetWhiteQueen(cell); break;
                    case PieceType.WhiteRook: SetWhiteRook(cell); break;
                    case PieceType.WhitePawn: SetWhitePawn(cell); break;
                    case PieceType.BlackKing: SetBlackKing(cell); break;
                    case PieceType.BlackKnight: SetBlackKnight(cell); break;
                    case PieceType.BlackBishop: SetBlackBishop(cell); break;
                    case PieceType.BlackQueen: SetBlackQueen(cell); break;
                    case PieceType.BlackRook: SetBlackRook(cell); break;
                    case PieceType.BlackPawn: SetBlackPawn(cell); break;
                }
            }
        }

        public SpeculativeMove GetMove(string moveString) {
            AlgebraicMove.ParseMove(moveString, Turn, out AlgebraicMove algMove);
            IEnumerable<SpeculativeMove> moves = GetValidMovesList();
            if (algMove.HintFileFrom > 0) {
                moves = moves.Where(move => move.From.GetFile() == algMove.HintFileFrom);
            }
            if (algMove.HintRankFrom > 0) {
                moves = moves.Where(move => move.From.GetRank() == algMove.HintRankFrom);
            }
            if (algMove.HintFileTo > 0) {
                moves = moves.Where(move => move.To.GetFile() == algMove.HintFileTo);
            }
            if (algMove.HintRankTo > 0) {
                moves = moves.Where(move => move.To.GetRank() == algMove.HintRankTo);
            }
            moves = moves.Where(move => move.Piece == algMove.Piece && move.Promotion == algMove.Promotion);
            var movesList = moves.ToList();
            if (movesList.Count == 0) throw new InvalidOperationException("Move is not found");
            if (movesList.Count > 1) throw new InvalidOperationException("Move string is ambigious");
            return movesList[0];
        }

        #region Turn

        public PieceColor Turn;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SwitchTurn() {
            Turn = (PieceColor)(1 - (byte)Turn);
        }

        #endregion

        #region Move

        /// <summary>
        /// Performs speculative move
        /// </summary>
        /// <param name="move"></param>
        /// <param name="rollbackData"></param>
        public void Move(SpeculativeMove move, out RollbackData rollbackData) {
            rollbackData.Castles = Castles;
            rollbackData.EnPassant = EnPassantMove;
            rollbackData.CapturedPiece = PieceType.None;
            EnPassantMove = CellName.None;
            move.Execute(this, ref rollbackData);
            Castles &= move.CastlesMask;
            SwitchTurn();
        }

        public void Unmove(SpeculativeMove move, ref RollbackData rollbackData) {
            move.Unexecute(this, ref rollbackData);
            SwitchTurn();
            EnPassantMove = rollbackData.EnPassant;
            Castles = rollbackData.Castles;
        }

        public bool ValidateAfterMove() {
            switch (Turn) {
                case PieceColor.White:
                    return !IsAttackedByWhite(BlackKingPosition);
                case PieceColor.Black:
                    return !IsAttackedByBlack(WhiteKingPosition);
                default:
                    throw new InvalidOperationException();
            }
        }

        #endregion

        #region Iterators

        public IList<SpeculativeMove> GetValidMovesList() {
            IList<SpeculativeMove> availableMoves = new List<SpeculativeMove>();
            GenerateValidMoves(new DelegateIterator(this, availableMoves.Add));
            return availableMoves;
        }

        public IList<SpeculativeMove> GetPossibleMovesList() {
            IList<SpeculativeMove> availableMoves = new List<SpeculativeMove>();
            GeneratePossibleMoves(new DelegateIterator(this, availableMoves.Add));
            return availableMoves;
        }

        public virtual IList<SpeculativeMove> GetValidMoves(CellName from, CellName to, PieceType promotionPiece = PieceType.None) {
            return GetValidMovesList()
                        .Where(move => move.From == from && move.To == to && (promotionPiece == PieceType.None || move.Promotion == promotionPiece))
                        .ToList();
        }

        public virtual IList<SpeculativeMove> GetValidMoves(PieceType piece, CellName from, CellName to, PieceType promotionPiece = PieceType.None) {
            return GetValidMovesList()
                        .Where(move => move.Piece == piece && move.From == from && move.To == to && (promotionPiece == PieceType.None || move.Promotion == promotionPiece))
                        .ToList();
        }

        public void GenerateValidMoves(MovesIterator iterator) {
            var pieces = Turn == PieceColor.White ? Whites.Pieces : Blacks.Pieces;
            RollbackData rollback;
            while (pieces != 0) {
                var firstBitSet = pieces & (ulong)(-(long)pieces);
                var i = 0x0;
                if ((firstBitSet & 0xffffffff00000000ul) != 0) i |= 32;
                if ((firstBitSet & 0xffff0000ffff0000ul) != 0) i |= 16;
                if ((firstBitSet & 0xff00ff00ff00ff00ul) != 0) i |= 8;
                if ((firstBitSet & 0xf0f0f0f0f0f0f0f0ul) != 0) i |= 4;
                if ((firstBitSet & 0xccccccccccccccccul) != 0) i |= 2;
                if ((firstBitSet & 0xaaaaaaaaaaaaaaaaul) != 0) i |= 1;

                var currentMove = MoveMatrix.GetDirectFirstMove(_cells[i], (CellName)i);
                while (currentMove != null) {
                    var res = currentMove.FastValidate(this);
                    switch (res) {
                        case ValidationResult.Invalid:
                            currentMove = currentMove.NextGroupMove;
                            break;
                        case ValidationResult.Valid:
                            Move(currentMove, out rollback);
                            if (ValidateAfterMove()) {
                                iterator.Handle(currentMove);
                            }
                            Unmove(currentMove, ref rollback);
                            currentMove = currentMove.NextMove;
                            break;
                        case ValidationResult.ValidAndStop:
                            Move(currentMove, out rollback);
                            if (ValidateAfterMove()) {
                                iterator.Handle(currentMove);
                            }
                            Unmove(currentMove, ref rollback);
                            currentMove = currentMove.NextGroupMove;
                            break;
                    }
                }

                pieces ^= firstBitSet;
            }
        }

        /// <summary>
        /// Geerates possible moves. This method doesn't take into considerations checks.
        /// </summary>
        /// <param name="iterator"></param>
        public void GeneratePossibleMoves(MovesIterator iterator) {
            var pieces = Turn == PieceColor.White ? Whites.Pieces : Blacks.Pieces;
            RollbackData rollback;
            for (var i = 0; i < 64; i++) {
                if ((pieces & 0x01) != 0) {
                    var currentMove = MoveMatrix.GetDirectFirstMove(_cells[i], (CellName)i);
                    while (currentMove != null) {
                        var res = currentMove.FastValidate(this);
                        switch (res) {
                            case ValidationResult.Invalid:
                                currentMove = currentMove.NextGroupMove;
                                break;
                            case ValidationResult.Valid:
                                Move(currentMove, out rollback);
                                iterator.Handle(currentMove);
                                Unmove(currentMove, ref rollback);
                                currentMove = currentMove.NextMove;
                                break;
                            case ValidationResult.ValidAndStop:
                                Move(currentMove, out rollback);
                                iterator.Handle(currentMove);
                                Unmove(currentMove, ref rollback);
                                currentMove = currentMove.NextGroupMove;
                                break;
                        }
                    }
                }
                pieces >>= 1;
            }
        }
        #endregion

        public bool WhiteUnderCheck => IsAttackedByBlack(WhiteKingPosition);

        public bool BlackUnderCheck => IsAttackedByWhite(BlackKingPosition);

        public bool UnderCheck {
            get {
                switch (Turn) {
                    case PieceColor.White:
                        return WhiteUnderCheck;
                    case PieceColor.Black:
                        return BlackUnderCheck;
                    default:
                        return false;
                }
            }
        }

        public bool IsAttackedByBlack(CellName cell) {
            var bbc = BitBoard.Cells[(int)cell];

            if ((bbc.Knights & Blacks.Knights) != 0) return true;

            if ((bbc.Kings & Blacks.Kings) != 0) return true;

            if ((bbc.BlackPawn & Blacks.Pawns) != 0) {
                return true;
            }

            var allPieces = Whites.Pieces | Blacks.Pieces;
            var rqPieces = Blacks.Rooks | Blacks.Queens;
            var bqPieces = Blacks.Bishops | Blacks.Queens;

            if ((bbc.Horizontal & rqPieces) != 0) {
                var rqAttacks = bbc.HorizontalAttackers[(int)((allPieces >> ((int)cell & 0x38)) & 0xff)] & rqPieces;
                if (rqAttacks != 0) return true;
            }

            if ((bbc.Vertical & rqPieces) != 0) {
                if (CheckVertical(bbc, rqPieces)) return true;
            }

            if ((bbc.Diagonals & bqPieces) != 0) {
                if (CheckDiagonal(bbc, bqPieces)) return true;
            }

            return false;
        }

        public bool IsAttackedByWhite(CellName cell) {
            var bbc = BitBoard.Cells[(int)cell];

            if ((bbc.Knights & Whites.Knights) != 0) return true;

            if ((bbc.Kings & Whites.Kings) != 0) return true;

            if ((bbc.WhitePawn & Whites.Pawns) != 0) {
                return true;
            }

            var allPieces = Whites.Pieces | Blacks.Pieces;
            var rqPieces = Whites.Rooks | Whites.Queens;
            var bqPieces = Whites.Bishops | Whites.Queens;

            if ((bbc.Horizontal & rqPieces) != 0) {
                var rqAttacks = bbc.HorizontalAttackers[(int)((allPieces >> ((int)cell & 0x38)) & 0xff)] & rqPieces;
                if (rqAttacks != 0) return true;
            }

            if ((bbc.Vertical & rqPieces) != 0) {
                if (CheckVertical(bbc, rqPieces)) return true;
            }

            if ((bbc.Diagonals & bqPieces) != 0) {
                if (CheckDiagonal(bbc, bqPieces)) return true;
            }

            return false;
        }

        private bool CheckDiagonal(BitBoardCell bbc, ulong attackersMask) {
            var allPieces = Whites.Pieces | Blacks.Pieces;

            int rank = bbc.Rank;
            int file = bbc.File;
            int rank7 = bbc.RankInverted;
            int file7 = bbc.FileInverted;

            int len;
            ulong mask;

            var ne = bbc.NorthEast;
            if ((attackersMask & ne) > 0) {
                mask = bbc.Bit;
                for (len = rank > file ? rank7 : file7; len > 0; len--) {
                    mask <<= 9;
                    if ((attackersMask & mask) != 0) return true;
                    if ((allPieces & mask) != 0) break;
                }
            }

            var nw = bbc.NorthWest;
            if ((attackersMask & nw) > 0) {
                mask = bbc.Bit;
                for (len = rank > file7 ? rank7 : file; len > 0; len--) {
                    mask <<= 7;
                    if ((attackersMask & mask) != 0) return true;
                    if ((allPieces & mask) != 0) break;
                }
            }

            var se = bbc.SouthEast;
            if ((attackersMask & se) > 0) {
                mask = bbc.Bit;
                for (len = rank < file7 ? rank : file7; len > 0; len--) {
                    mask >>= 7;
                    if ((attackersMask & mask) != 0) return true;
                    if ((allPieces & mask) != 0) break;
                }
            }

            var sw = bbc.SouthWest;
            if ((attackersMask & sw) > 0) {
                mask = bbc.Bit;
                for (len = rank < file ? rank : file; len > 0; len--) {
                    mask >>= 9;
                    if ((attackersMask & mask) != 0) return true;
                    if ((allPieces & mask) != 0) break;
                }
            }

            return false;
        }

        private bool CheckVertical(BitBoardCell bbc, ulong attackersMask) {
            var allPieces = Whites.Pieces | Blacks.Pieces;

            int rank = bbc.Rank;
            var mask = bbc.Bit;
            for (var i = rank + 1; i < 8; i++) {
                mask <<= 8;
                if ((attackersMask & mask) != 0) return true;
                if ((allPieces & mask) != 0) break;
            }
            mask = bbc.Bit;
            for (var i = rank - 1; i >= 0; i--) {
                mask >>= 8;
                if ((attackersMask & mask) != 0) return true;
                if ((allPieces & mask) != 0) break;
            }
            return false;
        }

        #region Pool

        public PiecesPool Whites;
        public PiecesPool Blacks;

        public CellName WhiteKingPosition = CellName.None;
        public CellName BlackKingPosition = CellName.None;

        #endregion

        #region Castles

        public bool WhiteCanDoLongCastle {
            get { return (Castles & CastleFlags.WhiteCanDoLongCastle) != 0; }
            set {
                if (value) {
                    Castles |= CastleFlags.WhiteCanDoLongCastle;
                } else {
                    Castles &= ~CastleFlags.WhiteCanDoLongCastle;
                }
            }
        }

        public bool WhiteCanDoShortCastle {
            get { return (Castles & CastleFlags.WhiteCanDoShortCastle) != 0; }
            set {
                if (value) {
                    Castles |= CastleFlags.WhiteCanDoShortCastle;
                } else {
                    Castles &= ~CastleFlags.WhiteCanDoShortCastle;
                }
            }
        }

        public bool BlackCanDoLongCastle {
            get { return (Castles & CastleFlags.BlackCanDoLongCastle) != 0; }
            set {
                if (value) {
                    Castles |= CastleFlags.BlackCanDoLongCastle;
                } else {
                    Castles &= ~CastleFlags.BlackCanDoLongCastle;
                }
            }
        }

        public bool BlackCanDoShortCastle {
            get { return (Castles & CastleFlags.BlackCanDoShortCastle) != 0; }
            set {
                if (value) {
                    Castles |= CastleFlags.BlackCanDoShortCastle;
                } else {
                    Castles &= ~CastleFlags.BlackCanDoShortCastle;
                }
            }
        }

        #endregion

        public override string ToString() {
            const string template = "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "8 |/a8|/b8|/c8|/d8|/e8|/f8|/g8|/h8| Material: 0\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "7 |/a7|/b7|/c7|/d7|/e7|/f7|/g7|/h7| Castling: KQkq\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "6 |/a6|/b6|/c6|/d6|/e6|/f6|/g6|/h6| Moves   : 0 1\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "5 |/a5|/b5|/c5|/d5|/e5|/f5|/g5|/h5| To move : White\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "4 |/a4|/b4|/c4|/d4|/e4|/f4|/g4|/h4| Checkers: None\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "3 |/a3|/b3|/c3|/d3|/e3|/f3|/g3|/h3|\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "2 |/a2|/b2|/c2|/d2|/e2|/f2|/g2|/h2|\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "1 |/a1|/b1|/c1|/d1|/e1|/f1|/g1|/h1|\r\n" +
                                    "  +---+---+---+---+---+---+---+---+\r\n" +
                                    "    A   B   C   D   E   F   G   H  ";
            return template;
        }


        #region ICloneable Members

        public virtual ChessBoard Clone() {
            var clonedBoard = new ChessBoard();
            CloneTo(clonedBoard);
            return clonedBoard;
        }

        public ChessBoard CloneToSimple() {
            var board = new ChessBoard();
            CloneTo(board);
            return board;
        }

        public virtual void CloneTo(ChessBoard board) {
            var bfen = BFEN.GetUnpackedBFEN(this);
            BFEN.Setup(board, bfen);
        }

        #endregion
    }
}