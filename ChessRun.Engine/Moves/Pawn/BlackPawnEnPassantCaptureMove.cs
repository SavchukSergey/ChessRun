using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnEnPassantCaptureMove : BlackPawnMove {

        private readonly CellName _opponentCell;

        public BlackPawnEnPassantCaptureMove(CellName from, CellName to)
            : base(from, to) {
                if (@from.GetRank() != CellRank.R4 || to.GetRank() != CellRank.R3) {
                throw new InvalidOperationException();
            }

            _opponentCell = to.IncreaseRank();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board.EnPassantMove == To ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearWhitePawn(_opponentCell);
            board.ClearBlackPawn(From);
            board.SetBlackPawn(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.SetWhitePawn(_opponentCell);
            board.ClearBlackPawn(To);
            board.SetBlackPawn(From);
        }

        public override bool IsCapture(ChessBoard board) => true;
        
        protected override string GetNotationBody(ChessBoard board) {
            return GetCaptureNotationBody(board);
        }
    }
}