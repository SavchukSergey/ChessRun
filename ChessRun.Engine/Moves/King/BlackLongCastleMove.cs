namespace ChessRun.Engine.Moves.King {
    public class BlackLongCastleMove : BlackKingMove {

        public BlackLongCastleMove()
            : base(CellName.E8, CellName.C8) {
            CastlesMask &= ~CastleFlags.BlackCanDoLongCastle;
            CastlesMask &= ~CastleFlags.BlackCanDoShortCastle;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            if (!board.BlackCanDoLongCastle) return ValidationResult.Invalid;
            if (board[CellName.D8] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.C8] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.B8] != PieceType.None) return ValidationResult.Invalid;
            if (board.IsAttackedByWhite(CellName.E8)) return ValidationResult.Invalid;
            if (board.IsAttackedByWhite(CellName.D8)) return ValidationResult.Invalid;
            return ValidationResult.ValidAndStop;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackRook(CellName.A8);
            board.SetBlackKing(CellName.C8);
            board.SetBlackRook(CellName.D8);
            board.ClearBlackKing(CellName.E8);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.SetBlackRook(CellName.A8);
            board.ClearBlackKing(CellName.C8);
            board.ClearBlackRook(CellName.D8);
            board.SetBlackKing(CellName.E8);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O-O";
        }
    }
}