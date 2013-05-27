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
            board[CellName.A1] = PieceType.None;
            board[CellName.C1] = PieceType.WhiteKing;
            board[CellName.D1] = PieceType.WhiteRook;
            board[CellName.E1] = PieceType.None;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[CellName.A1] = PieceType.WhiteRook;
            board[CellName.C1] = PieceType.None;
            board[CellName.D1] = PieceType.None;
            board[CellName.E1] = PieceType.WhiteKing;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O-O";
        }

    }
}