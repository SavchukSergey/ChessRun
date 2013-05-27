﻿namespace ChessRun.Engine.Moves.Rook {
    public class BlackRookRegularMove : BlackRookMove {

        public BlackRookRegularMove(CellName from, CellName to)
            : base(from, to) {
        }

        public override void Execute(ChessBoard board, ref RollbackData rollbackData) {
            rollbackData.CapturedPiece = board[To];
            board[From] = PieceType.None;
            board[To] = PieceType.BlackRook;
        }

        public override void Unexecute(ChessBoard board, ref RollbackData rollbackData) {
            board[To] = rollbackData.CapturedPiece;
            board[From] = PieceType.BlackRook;
        }
    }
}