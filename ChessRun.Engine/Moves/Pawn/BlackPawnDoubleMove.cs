﻿using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnDoubleMove : BlackPawnMove {

        private readonly CellName _middleCell;

        public BlackPawnDoubleMove(CellName from)
            : base(from, @from.DecreaseRank(2)) {
            _middleCell = @from.DecreaseRank();
        }

        public CellName MiddleCell {
            get { return _middleCell; }
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackPawn(From);
            board.SetBlackPawn(To);
            board.EnPassantMove = _middleCell;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackPawn(To);
            board.SetBlackPawn(From);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return To.GetCellName();
        }

    }
}