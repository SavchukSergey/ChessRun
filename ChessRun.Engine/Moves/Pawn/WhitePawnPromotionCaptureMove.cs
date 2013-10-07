using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class WhitePawnPromotionCaptureMove : WhitePawnMove, IPromotionMove {

        public WhitePawnPromotionCaptureMove(CellName from, CellName to, PieceType promotion)
            : base(from, to) {
            Promotion = promotion;
            if (CellOperations.GetRank(from) != CellRank.R7 || CellOperations.GetRank(to) != CellRank.R8) throw new InvalidOperationException();
            if (promotion.GetColor() != PieceColor.White) throw new InvalidOperationException();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsBlack() ? ValidationResult.Valid : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = Promotion;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.WhitePawn;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return CellOperations.GetFileSymbol(From).ToString() + CellOperations.GetFileSymbol(To).ToString() + "=" + PieceOperations.GetPromotionPieceSymbol(Promotion);
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

    }
}