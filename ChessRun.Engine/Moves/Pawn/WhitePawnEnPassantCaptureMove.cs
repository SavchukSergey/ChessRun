using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnEnPassantCaptureMove : WhitePawnMove {

        private readonly CellName _opponentCell;

        public WhitePawnEnPassantCaptureMove(CellName from, CellName to)
            : base(from, to) {
            if (CellOperations.GetRank(from) != CellRank.R5 || CellOperations.GetRank(to) != CellRank.R6) {
                throw new InvalidOperationException();
            }
            _opponentCell = CellOperations.DecreaseRank(to);
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board.EnPassantMove == To ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[_opponentCell] = PieceType.None;
            board[From] = PieceType.None;
            board[To] = PieceType.WhitePawn;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[_opponentCell] = PieceType.BlackPawn;
            board[To] = PieceType.None;
            board[From] = PieceType.WhitePawn;
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }

    }
}