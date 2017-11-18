using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnPromotionCaptureMove : BlackPawnMove, IPromotionMove {

        public BlackPawnPromotionCaptureMove(CellName from, CellName to, PieceType promotion)
            : base(from, to) {
            Promotion = promotion;
            if (@from.GetRank() != CellRank.R2 || to.GetRank() != CellRank.R1) throw new InvalidOperationException();
            if (promotion.GetColor() != PieceColor.Black) throw new InvalidOperationException();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsWhite() ? ValidationResult.Valid : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearBlackPawn(From);
            board[To] = Promotion;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetBlackPawn(From);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return From.GetFileSymbol().ToString() + To.GetFileSymbol().ToString() + "=" + PieceOperations.GetPromotionPieceSymbol(Promotion);
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

    }
}