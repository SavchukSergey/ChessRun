using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnEnPassantCaptureMove : BlackPawnMove {

        private readonly CellName _opponentCell;

        public BlackPawnEnPassantCaptureMove(CellName from, CellName to)
            : base(from, to) {
                if (CellOperations.GetRank(from) != CellRank.R4 || CellOperations.GetRank(to) != CellRank.R3) {
                throw new InvalidOperationException();
            }

            _opponentCell = CellOperations.IncreaseRank(to);
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board.EnPassantMove == To ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[_opponentCell] = PieceType.None;
            board[From] = PieceType.None;
            board[To] = PieceType.BlackPawn;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[_opponentCell] = PieceType.WhitePawn;
            board[To] = PieceType.None;
            board[From] = PieceType.BlackPawn;
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }
    }
}