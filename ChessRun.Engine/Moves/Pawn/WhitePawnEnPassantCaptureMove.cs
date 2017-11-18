using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnEnPassantCaptureMove : WhitePawnMove {

        private readonly CellName _opponentCell;

        public WhitePawnEnPassantCaptureMove(CellName from, CellName to)
            : base(from, to) {
            if (@from.GetRank() != CellRank.R5 || to.GetRank() != CellRank.R6) {
                throw new InvalidOperationException();
            }
            _opponentCell = to.DecreaseRank();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board.EnPassantMove == To ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackPawn(_opponentCell);
            board.ClearWhitePawn(From);
            board.SetWhitePawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[_opponentCell] = PieceType.BlackPawn;
            board.ClearWhitePawn(To);
            board.SetWhitePawn(From);
        }

        public override bool IsCapture(ChessBoard board) => true;

        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }

    }
}