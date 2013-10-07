using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnPromotionMove : WhitePawnMove, IPromotionMove {

        public WhitePawnPromotionMove(CellName from, PieceType promotion)
            : base(from, @from.IncreaseRank()) {
            Promotion = promotion;
            if (@from.GetRank() != CellRank.R7) throw new InvalidOperationException();
            if (promotion.GetColor() != PieceColor.White) throw new InvalidOperationException();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            return board[To] == PieceType.None ? ValidationResult.Valid : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.None;
            board[To] = Promotion;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[From] = PieceType.WhitePawn;
            board[To] = PieceType.None;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return To.GetCellName() + "=" + PieceOperations.GetPromotionPieceSymbol(Promotion);
        }

    }
}