﻿using System.Diagnostics;
using System.Linq;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    /// <summary>
    /// Represents base pirce move
    /// </summary>
    [DebuggerDisplay("Piece: {Piece}, From: {From}, To: {To}")]
    public abstract class SpeculativeMove {

        /// <summary>
        /// Gets or sets piece type
        /// </summary>
        public PieceType Piece;

        /// <summary>
        /// Gets or sets cell from which move is performed
        /// </summary>
        public readonly CellName From;

        /// <summary>
        /// Gets or sets cell to which move is performed
        /// </summary>
        public readonly CellName To;

        /// <summary>
        /// Gets or sets pawn promotion piece
        /// </summary>
        public PieceType Promotion = PieceType.None;

        /// <summary>
        /// Gets or sets castles mask
        /// </summary>
        public CastleFlags CastlesMask;

        protected SpeculativeMove(CellName from, CellName to) {
            From = from;
            To = to;
            CastlesMask = CastleFlags.All;
            var mergedCells = (1ul << (int)from) | (1ul << (int)to);
            if ((mergedCells & (1ul << (int)CellName.A1)) != 0) {
                CastlesMask &= ~CastleFlags.WhiteCanDoLongCastle;
            }
            if ((mergedCells & (1ul << (int)CellName.H1)) != 0) {
                CastlesMask &= ~CastleFlags.WhiteCanDoShortCastle;
            }
            if ((mergedCells & (1ul << (int)CellName.A8)) != 0) {
                CastlesMask &= ~CastleFlags.BlackCanDoLongCastle;
            }
            if ((mergedCells & (1ul << (int)CellName.H8)) != 0) {
                CastlesMask &= ~CastleFlags.BlackCanDoShortCastle;
            }
            if ((mergedCells & (1ul << (int)CellName.E1)) != 0) {
                CastlesMask &= ~CastleFlags.WhiteCanDoLongCastle;
                CastlesMask &= ~CastleFlags.WhiteCanDoShortCastle;
            }
            if ((mergedCells & (1ul << (int)CellName.E8)) != 0) {
                CastlesMask &= ~CastleFlags.BlackCanDoLongCastle;
                CastlesMask &= ~CastleFlags.BlackCanDoShortCastle;
            }
        }

        /// <summary>
        /// Gets or sets next move in case this move is not valid in current board state.
        /// </summary>
        public SpeculativeMove NextGroupMove;

        /// <summary>
        /// Gets or sets next move in case this move is valid in current board state.
        /// </summary>
        public SpeculativeMove NextMove;

        /// <summary>
        /// Validates minimal set of cells relying on previous checks.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract ValidationResult FastValidate(ChessBoard board);

        /// <summary>
        /// Executes move.
        /// </summary>
        /// <param name="board">Board on which move is executed</param>
        /// <param name="rollbackData">Rollback data for unexecute operation</param>
        public abstract void Execute(ChessBoard board, ref RollbackData rollbackData);

        /// <summary>
        /// Revert board to originial state before move
        /// </summary>
        /// <param name="board">Board on which move was executed</param>
        /// <param name="rollbackData">Rollback data retrieved during execute operation</param>
        public abstract void Unexecute(ChessBoard board, ref RollbackData rollbackData);

        /// <summary>
        /// Returns short notation of the move. Must be invoked before move has been actually done.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public string ToShortNotation(ChessBoard board) {
            var body = GetNotationBody(board);
            board.Move(this, out RollbackData rollback);
            var suffix = GetCheckmateState(board);
            board.Unmove(this, ref rollback);
            return body + suffix;
        }

        /// <summary>
        /// Returns notation body of move (without check and checkmates)
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        protected virtual string GetNotationBody(ChessBoard board) {
            var moves = board.GetValidMovesList()
                      .Where(item => item.Piece == Piece && item.To == To && item.Promotion == Promotion)
                      .ToList();
            string captureSymbol = IsCapture(board) ? CaptureSymbol : "";
            if (moves.Count == 1) return NotationSymbol + captureSymbol + To.GetCellName();

            var fromFile = From.GetFile();
            var fromRank = From.GetRank();

            var byFile = moves.Where(item => item.From.GetFile() == fromFile).ToList();
            if (byFile.Count == 1) return NotationSymbol + CellOperations.GetFileSymbol(fromFile) + captureSymbol + To.GetCellName();

            var byRank = moves.Where(item => item.From.GetRank() == fromRank).ToList();
            if (byRank.Count == 1) return NotationSymbol + fromRank.GetRankSymbol() + captureSymbol + To.GetCellName();

            return NotationSymbol + From.GetCellName() + captureSymbol + To.GetCellName();
        }

        /// <summary>
        /// Returns character +, # or none depending on whether opponent is under check or in checkmate
        /// </summary>
        /// <returns></returns>
        protected string GetCheckmateState(ChessBoard board) {
            bool check = board.UnderCheck;

            var otherMoves = board.GetValidMovesList();
            if (check) {
                if (otherMoves.Count > 0) return "+";
                return "#";
            }
            return "";
        }

        protected abstract string NotationSymbol { get; }

        public virtual string CaptureSymbol => "x";

        /// <summary>
        /// Checks whether current move performs piece capture. Must be invoked before move took place.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public virtual bool IsCapture(ChessBoard board) {
            var color = Piece.GetColor();
            var opponentColor = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            var otherPiece = board[To];
            return otherPiece.GetColor() == opponentColor;
        }

    }
}