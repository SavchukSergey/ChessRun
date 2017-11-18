namespace ChessRun.Engine.Moves.King {
    public class BlackShortCastleMove : BlackKingMove {

        public BlackShortCastleMove()
            : base(CellName.E8, CellName.G8) {
            CastlesMask &= ~CastleFlags.BlackCanDoLongCastle;
            CastlesMask &= ~CastleFlags.BlackCanDoShortCastle;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            if (!board.BlackCanDoShortCastle) return ValidationResult.Invalid;
            if (board[CellName.F8] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.G8] != PieceType.None) return ValidationResult.Invalid;
            if (board.IsAttackedByWhite(CellName.E8)) return ValidationResult.Invalid;
            if (board.IsAttackedByWhite(CellName.F8)) return ValidationResult.Invalid;
            return ValidationResult.ValidAndStop;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearBlackKing(CellName.E8);
            board.SetBlackRook(CellName.F8);
            board.SetBlackKing(CellName.G8);
            board.ClearBlackRook(CellName.H8);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.SetBlackKing(CellName.E8);
            board.ClearBlackRook(CellName.F8);
            board.ClearBlackKing(CellName.G8);
            board.SetBlackRook(CellName.H8);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O";
        }
    }
}