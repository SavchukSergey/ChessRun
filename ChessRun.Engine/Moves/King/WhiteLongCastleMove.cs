namespace ChessRun.Engine.Moves.King {
    public class WhiteLongCastleMove : WhiteKingMove {

        public WhiteLongCastleMove()
            : base(CellName.E1, CellName.C1) {
            CastlesMask &= ~CastleFlags.WhiteCanDoLongCastle;
            CastlesMask &= ~CastleFlags.WhiteCanDoShortCastle;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            if (!board.WhiteCanDoLongCastle) return ValidationResult.Invalid;
            if (board[CellName.D1] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.C1] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.B1] != PieceType.None) return ValidationResult.Invalid;
            if (board.IsAttackedByBlack(CellName.E1)) return ValidationResult.Invalid;
            if (board.IsAttackedByBlack(CellName.D1)) return ValidationResult.Invalid;
            return ValidationResult.ValidAndStop;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearWhiteRook(CellName.A1);
            board.SetWhiteKing(CellName.C1);
            board.SetWhiteRook(CellName.D1);
            board.ClearWhiteKing(CellName.E1);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.SetWhiteRook(CellName.A1);
            board.ClearWhiteKing(CellName.C1);
            board.ClearWhiteRook(CellName.D1);
            board.SetWhiteKing(CellName.E1);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O-O";
        }

    }
}