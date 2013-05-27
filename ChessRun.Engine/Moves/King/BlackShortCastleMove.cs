﻿namespace ChessRun.Engine.Moves.King {
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
            board[CellName.E8] = PieceType.None;
            board[CellName.F8] = PieceType.BlackRook;
            board[CellName.G8] = PieceType.BlackKing;
            board[CellName.H8] = PieceType.None;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[CellName.E8] = PieceType.BlackKing;
            board[CellName.F8] = PieceType.None;
            board[CellName.G8] = PieceType.None;
            board[CellName.H8] = PieceType.BlackRook;
        }

        protected override string GetNotationBody(ChessBoard board) {
            return "O-O";
        }
    }
}