using System;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.Pawn {
    public class BlackPawnPromotionCaptureMove : BlackPawnMove, IPromotionMove {

        public BlackPawnPromotionCaptureMove(CellName from, CellName to, PieceType promotion)
            : base(from, to) {
            Promotion = promotion;
            if (CellOperations.GetRank(from) != CellRank.R2 || CellOperations.GetRank(to) != CellRank.R1) throw new InvalidOperationException();
            if (PieceOperations.GetColor(promotion) != PieceColor.Black) throw new InvalidOperationException();
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return PieceOperations.IsWhite(piece) ? ValidationResult.Valid : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = Promotion;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.BlackPawn;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return CellOperations.GetFileSymbol(From).ToString() + CellOperations.GetFileSymbol(To).ToString() + "=" + PieceOperations.GetPromotionPieceSymbol(Promotion);
        }

        public override bool IsCapture(ChessBoard board) {
            return true;
        }

    }
}