namespace ChessRun.Engine.Moves.King {
    public class WhiteShortCastleMove : WhiteKingMove {

        public WhiteShortCastleMove()
            : base(CellName.E1, CellName.G1) {
            CastlesMask &= ~CastleFlags.WhiteCanDoLongCastle;
            CastlesMask &= ~CastleFlags.WhiteCanDoShortCastle;
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            if (!board.WhiteCanDoShortCastle) return ValidationResult.Invalid;
            if (board[CellName.F1] != PieceType.None) return ValidationResult.Invalid;
            if (board[CellName.G1] != PieceType.None) return ValidationResult.Invalid;
            if (board.IsAttackedByBlack(CellName.E1)) return ValidationResult.Invalid;
            if (board.IsAttackedByBlack(CellName.F1)) return ValidationResult.Invalid;
            return ValidationResult.ValidAndStop;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            board.ClearWhiteKing(CellName.E1);
            board.SetWhiteRook(CellName.F1);
            board.SetWhiteKing(CellName.G1);
            board.ClearWhiteRook(CellName.H1);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board.SetWhiteKing(CellName.E1);
            board.ClearWhiteRook(CellName.F1);
            board.ClearWhiteKing(CellName.G1);
            board.SetWhiteRook(CellName.H1);
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O";
        }
    }
}