using System;
using System.Linq;
using System.Collections.Generic;
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
            Castles = CastleFlags.BlackCanDoLongCastle | CastleFlags.BlackCanDoShortCastle |
                      CastleFlags.WhiteCanDoLongCastle | CastleFlags.WhiteCanDoShortCastle;
        }

        public virtual PieceType this[CellName cell] {
            get { return _cells[(int)cell]; }
            set {
                var oldValue = _cells[(int)cell];
                _cells[(int)cell] = value;
                var mask = 0x1ul << (int)cell;
                var negMask = ~mask;

                switch (oldValue) {
                    case PieceType.WhiteKing:
                        WhiteKings &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.WhiteKnight:
                        WhiteKnights &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.WhiteBishop:
                        WhiteBishops &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.WhiteQueen:
                        WhiteQueens &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.WhiteRook:
                        WhiteRooks &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.WhitePawn:
                        WhitePawns &= negMask;
                        WhitePieces &= negMask;
                        break;
                    case PieceType.BlackKing:
                        BlackKings &= negMask;
                        BlackPieces &= negMask;
                        break;
                    case PieceType.BlackKnight:
                        BlackKnights &= negMask;
                        BlackPieces &= negMask;
                        break;
                    case PieceType.BlackBishop:
                        BlackBishops &= negMask;
                        BlackPieces &= negMask;
                        break;
                    case PieceType.BlackQueen:
                        BlackQueens &= negMask;
                        BlackPieces &= negMask;
                        break;
                    case PieceType.BlackRook:
                        BlackRooks &= negMask;
                        BlackPieces &= negMask;
                        break;
                    case PieceType.BlackPawn:
                        BlackPawns &= negMask;
                        BlackPieces &= negMask;
                        break;
                }

                switch (value) {
                    case PieceType.WhiteKing:
                        WhiteKingPosition = cell;
                        WhiteKings |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.WhiteKnight:
                        WhiteKnights |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.WhiteBishop:
                        WhiteBishops |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.WhiteQueen:
                        WhiteQueens |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.WhiteRook:
                        WhiteRooks |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.WhitePawn:
                        WhitePawns |= mask;
                        WhitePieces |= mask;
                        break;
                    case PieceType.BlackKing:
                        BlackKingPosition = cell;
                        BlackKings |= mask;
                        BlackPieces |= mask;
                        break;
                    case PieceType.BlackKnight:
                        BlackKnights |= mask;
                        BlackPieces |= mask;
                        break;
                    case PieceType.BlackBishop:
                        BlackBishops |= mask;
                        BlackPieces |= mask;
                        break;
                    case PieceType.BlackQueen:
                        BlackQueens |= mask;
                        BlackPieces |= mask;
                        break;
                    case PieceType.BlackRook:
                        BlackRooks |= mask;
                        BlackPieces |= mask;
                        break;
                    case PieceType.BlackPawn:
                        BlackPawns |= mask;
                        BlackPieces |= mask;
                        break;
                }
            }
        }

        public SpeculativeMove GetMove(string moveString) {
            AlgebraicMove algMove;
            AlgebraicMove.ParseMove(moveString, Turn, out algMove);
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
        public virtual void Move(SpeculativeMove move, out RollbackData rollbackData) {
            rollbackData.Castles = Castles;
            rollbackData.EnPassant = EnPassantMove;
            rollbackData.CapturedPiece = PieceType.None;
            EnPassantMove = CellName.None;
            move.Execute(this, ref rollbackData);
            Castles &= move.CastlesMask;
            SwitchTurn();
        }

        public virtual void Unmove(SpeculativeMove move, ref RollbackData rollbackData) {
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
            var pieces = Turn == PieceColor.White ? WhitePieces : BlackPieces;
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
                }
                pieces >>= 1;
            }
        }

        /// <summary>
        /// Geerates possible moves. This method doesn't take into considerations checks.
        /// </summary>
        /// <param name="iterator"></param>
        public void GeneratePossibleMoves(MovesIterator iterator) {
            var pieces = Turn == PieceColor.White ? WhitePieces : BlackPieces;
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

        public bool WhiteUnderCheck {
            get {
                return IsAttackedByBlack(WhiteKingPosition);
            }
        }

        public bool BlackUnderCheck {
            get {
                return IsAttackedByWhite(BlackKingPosition);
            }
        }

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
            var attacks = BitBoard.KnightBitBoards[(int)cell];
            if ((attacks & BlackKnights) != 0) return true;

            attacks = BitBoard.KingBitBoards[(int)cell];
            if ((attacks & BlackKings) != 0) return true;

            attacks = BitBoard.BlackPawnsBitBoards[(int)cell];
            if ((attacks & BlackPawns) != 0) {
                return true;
            }

            var allPieces = WhitePieces | BlackPieces;
            var rqPieces = BlackRooks | BlackQueens;
            var bqPieces = BlackBishops | BlackQueens;

            attacks = BitBoard.HVBitBoards[(int)cell];
            if ((attacks & rqPieces) != 0) {
                var rqAttacks = BitBoard.HorizontalAttackers[(int)cell * 256 + (int)((allPieces >> ((int)cell & 0x38)) & 0xff)] & rqPieces;
                if (rqAttacks != 0) return true;
                if (CheckVertical(cell, rqPieces)) return true;
            }

            attacks = BitBoard.DiagonalBitBoards[(int)cell];
            if ((attacks & bqPieces) != 0) {
                if (CheckDiagonal(cell, bqPieces)) return true;
            }

            return false;
        }

        public bool IsAttackedByWhite(CellName cell) {
            var attacks = BitBoard.KnightBitBoards[(int)cell];
            if ((attacks & WhiteKnights) != 0) return true;

            attacks = BitBoard.KingBitBoards[(int)cell];
            if ((attacks & WhiteKings) != 0) return true;

            attacks = BitBoard.WhitePawnsBitBoards[(int)cell];
            if ((attacks & WhitePawns) != 0) {
                return true;
            }

            var allPieces = WhitePieces | BlackPieces;
            var rqPieces = WhiteRooks | WhiteQueens;
            var bqPieces = WhiteBishops | WhiteQueens;

            attacks = BitBoard.HVBitBoards[(int)cell];
            if ((attacks & rqPieces) != 0) {
                var rqAttacks = BitBoard.HorizontalAttackers[(int)cell * 256 + (int)((allPieces >> ((int)cell & 0x38)) & 0xff)] & rqPieces;
                if (rqAttacks != 0) return true;
                if (CheckVertical(cell, rqPieces)) return true;
            }

            attacks = BitBoard.DiagonalBitBoards[(int)cell];
            if ((attacks & bqPieces) != 0) {
                if (CheckDiagonal(cell, bqPieces)) return true;
            }

            return false;
        }

        private bool CheckDiagonal(CellName cell, ulong attackersMask) {
            var allPieces = WhitePieces | BlackPieces;

            int rank = (int)cell >> 3;
            int file = (int)cell & 0x07;
            int rank7 = 7 - rank;
            int file7 = 7 - file;

            int len;

            int index = (int)cell;
            for (len = rank > file ? rank7 : file7; len > 0; len--) {
                index += 9;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }

            index = (int)cell;
            for (len = rank > file7 ? rank7 : file; len > 0; len--) {
                index += 7;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }

            index = (int)cell;
            for (len = rank < file ? rank : file; len > 0; len--) {
                index -= 9;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }

            index = (int)cell;
            for (len = rank < file7 ? rank : file7; len > 0; len--) {
                index -= 7;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }
            return false;
        }

        private bool CheckVertical(CellName cell, ulong attackersMask) {
            var allPieces = WhitePieces | BlackPieces;

            int rank = (int)cell >> 3;
            int index = (int)cell;
            for (var i = rank + 1; i < 8; i++) {
                index += 8;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }
            index = (int)cell;
            for (var i = rank - 1; i >= 0; i--) {
                index -= 8;
                if ((attackersMask & (1ul << index)) != 0) return true;
                if ((allPieces & (1ul << index)) != 0) break;
            }
            return false;
        }

        #region Pool

        public ulong WhitePieces;
        public ulong WhitePawns;
        public ulong WhiteKnights;
        public ulong WhiteBishops;
        public ulong WhiteRooks;
        public ulong WhiteQueens;
        public ulong WhiteKings;

        public ulong BlackPieces;
        public ulong BlackPawns;
        public ulong BlackKnights;
        public ulong BlackBishops;
        public ulong BlackRooks;
        public ulong BlackQueens;
        public ulong BlackKings;

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