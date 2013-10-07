using System.Diagnostics;
using System.Linq;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves {
    [DebuggerDisplay("Piece: {Piece}, From: {From}, To: {To}")]
    public abstract class SpeculativeMove {

        public PieceType Piece;

        public readonly CellName From;

        public readonly CellName To;

        public PieceType Promotion = PieceType.None;

        public CastleFlags CastlesMask;

        protected SpeculativeMove(CellName from, CellName to) {
            From = from;
            To = to;
            CastlesMask = CastleFlags.WhiteCanDoLongCastle | CastleFlags.WhiteCanDoShortCastle | CastleFlags.BlackCanDoLongCastle | CastleFlags.BlackCanDoShortCastle;
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

        public SpeculativeMove NextGroupMove;

        public SpeculativeMove NextMove;

        /// <summary>
        /// Validates minimal set of cells relying on previous checks.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract ValidationResult FastValidate(ChessBoard board);

        public abstract void Execute(ChessBoard board, ref RollbackData rollbackData);

        public abstract void Unexecute(ChessBoard board, ref RollbackData rollbackData);

        /// <summary>
        /// Returns short notation of the move. Must be invoked before move has been actually done.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public string ToShortNotation(ChessBoard board) {
            var body = GetNotationBody(board);
            RollbackData rollback;
            board.Move(this, out rollback);
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
            if (moves.Count == 1) return NotationSymbol + captureSymbol + CellOperations.GetCellName(To);

            var fromFile = CellOperations.GetFile(From);
            var fromRank = CellOperations.GetRank(From);

            var byFile = moves.Where(item => CellOperations.GetFile(item.From) == fromFile).ToList();
            if (byFile.Count == 1) return NotationSymbol + CellOperations.GetFileSymbol(fromFile) + captureSymbol + CellOperations.GetCellName(To);

            var byRank = moves.Where(item => CellOperations.GetRank(item.From) == fromRank).ToList();
            if (byRank.Count == 1) return NotationSymbol + CellOperations.GetRankSymbol(fromRank) + captureSymbol + CellOperations.GetCellName(To);

            return NotationSymbol + CellOperations.GetCellName(From) + captureSymbol + CellOperations.GetCellName(To);
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

        public virtual string CaptureSymbol { get { return "x"; } }

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