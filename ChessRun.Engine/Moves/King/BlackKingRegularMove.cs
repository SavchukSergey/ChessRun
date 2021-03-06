﻿using ChessRun.Engine.Utils;

namespace ChessRun.Engine.Moves.King {
    public class BlackKingRegularMove : BlackKingMove {

        public BlackKingRegularMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override ValidationResult FastValidate(ChessBoard board) {
            var piece = board[To];
            return piece.IsWhiteOrEmpty() ? ValidationResult.ValidAndStop : ValidationResult.Invalid;
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board.ClearBlackKing(From);
            board.ClearCell(To);
            board.SetBlackKing(To);
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board.SetBlackKing(From);
        }

    }
}