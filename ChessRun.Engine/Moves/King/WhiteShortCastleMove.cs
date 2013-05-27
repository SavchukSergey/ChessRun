﻿namespace ChessRun.Engine.Moves.King {
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
            board[CellName.E1] = PieceType.None;
            board[CellName.F1] = PieceType.WhiteRook;
            board[CellName.G1] = PieceType.WhiteKing;
            board[CellName.H1] = PieceType.None;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[CellName.E1] = PieceType.WhiteKing;
            board[CellName.F1] = PieceType.None;
            board[CellName.G1] = PieceType.None;
            board[CellName.H1] = PieceType.WhiteRook;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O";
        }
    }
}