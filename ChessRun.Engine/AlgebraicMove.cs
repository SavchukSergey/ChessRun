using System;
using System.Text;
using ChessRun.Engine.Utils;

namespace ChessRun.Engine {
    public struct AlgebraicMove {

        public PieceType Promotion;

        public PieceType Piece;

        public CellFile HintFileFrom;

        public CellRank HintRankFrom;

        public CellFile HintFileTo;

        public CellRank HintRankTo;

        public bool Capture;

        public bool Check;

        public bool Checkmate;

        public bool IsShortCastle;

        public bool IsLongCastle;

        public CellName To {
            get {
                if (HintFileTo > 0 && HintRankTo > 0) {
                    return CellOperations.GetCell(HintFileTo, HintRankTo);
                }
                throw new InvalidOperationException("'To' property can be used only when both destination file and rank are specified");
            }
            set {
                HintFileTo = value.GetFile();
                HintRankTo = value.GetRank();
            }
        }

        public CellName From {
            get {
                if (HintFileFrom > 0 && HintRankFrom > 0) {
                    return CellOperations.GetCell(HintFileFrom, HintRankFrom);
                }
                throw new InvalidOperationException("'From' property can be used only when both destination file and rank are specified");
            }
            set {
                HintFileFrom = value.GetFile();
                HintRankFrom = value.GetRank();
            }
        }

        public static void ParseMove(string move, PieceColor turn, out AlgebraicMove algebraicMove) {
            algebraicMove.HintFileFrom = CellFile.None;
            algebraicMove.HintRankFrom = CellRank.None;
            algebraicMove.HintFileTo = CellFile.None;
            algebraicMove.HintRankTo = CellRank.None;
            algebraicMove.IsLongCastle = false;
            algebraicMove.IsShortCastle = false;
            algebraicMove.Promotion = PieceType.None;
            algebraicMove.Check = false;
            algebraicMove.Checkmate = false;
            algebraicMove.Capture = false;
            var sb = new StringBuilder(move);
            if (sb.Length >= 4 && CellOperations.IsValidFile(sb[0]) && sb[1] == 'x' && CellOperations.IsValidFile(sb[2]) && CellOperations.IsValidRank(sb[3])) {
                algebraicMove.Piece = PieceOperations.GetPawn(turn);
                algebraicMove.Capture = true;
                algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                algebraicMove.HintFileTo = CellOperations.GetFile(sb[2]);
                algebraicMove.HintRankTo = CellOperations.GetRank(sb[3]);
                sb = sb.Remove(0, 4);
            } else if (sb.Length >= 3 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidFile(sb[1]) && CellOperations.IsValidRank(sb[2])) {
                algebraicMove.Piece = PieceOperations.GetPawn(turn);
                algebraicMove.Capture = true;
                algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                algebraicMove.HintFileTo = CellOperations.GetFile(sb[1]);
                algebraicMove.HintRankTo = CellOperations.GetRank(sb[2]);
                sb = sb.Remove(0, 3);
            } else if (sb.Length >= 3 && CellOperations.IsValidFile(sb[0]) && sb[1] == 'x' && CellOperations.IsValidFile(sb[2])) {
                algebraicMove.Piece = PieceOperations.GetPawn(turn);
                algebraicMove.Capture = true;
                algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                algebraicMove.HintFileTo = CellOperations.GetFile(sb[2]);
                sb = sb.Remove(0, 3);
            } else if (sb.Length >= 2 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidFile(sb[1])) {
                algebraicMove.Piece = PieceOperations.GetPawn(turn);
                algebraicMove.Capture = true;
                algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                algebraicMove.HintFileTo = CellOperations.GetFile(sb[1]);
                sb = sb.Remove(0, 2);
            } else if (sb.Length >= 2 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidRank(sb[1])) {
                algebraicMove.Piece = PieceOperations.GetPawn(turn);
                algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                algebraicMove.HintRankTo = CellOperations.GetRank(sb[1]);
                algebraicMove.HintFileTo = CellOperations.GetFile(sb[0]);
                sb = sb.Remove(0, 2);
            } else if (sb.Length >= 1 && PieceOperations.IsValidPiece(sb[0])) {
                algebraicMove.Piece = PieceOperations.GetPiece(sb[0], turn);
                sb.Remove(0, 1);
                if (sb.Length >= 5 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidRank(sb[1]) && sb[2] == 'x' && CellOperations.IsValidFile(sb[3]) && CellOperations.IsValidRank(sb[4])) {
                    algebraicMove.Capture = true;
                    algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                    algebraicMove.HintRankFrom = CellOperations.GetRank(sb[1]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[3]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[4]);
                    sb.Remove(0, 5);
                } else if (sb.Length >= 4 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidRank(sb[1]) && CellOperations.IsValidFile(sb[2]) && CellOperations.IsValidRank(sb[3])) {
                    algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                    algebraicMove.HintRankFrom = CellOperations.GetRank(sb[1]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[2]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[3]);
                    sb.Remove(0, 4);
                } else if (sb.Length >= 4 && CellOperations.IsValidFile(sb[0]) && sb[1] == 'x' && CellOperations.IsValidFile(sb[2]) && CellOperations.IsValidRank(sb[3])) {
                    algebraicMove.Capture = true;
                    algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[2]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[3]);
                    sb.Remove(0, 4);
                } else if (sb.Length >= 4 && CellOperations.IsValidRank(sb[0]) && sb[1] == 'x' && CellOperations.IsValidFile(sb[2]) && CellOperations.IsValidRank(sb[3])) {
                    algebraicMove.Capture = true;
                    algebraicMove.HintRankFrom = CellOperations.GetRank(sb[0]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[2]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[3]);
                    sb.Remove(0, 4);
                } else if (sb.Length >= 3 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidFile(sb[1]) && CellOperations.IsValidRank(sb[2])) {
                    algebraicMove.HintFileFrom = CellOperations.GetFile(sb[0]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[1]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[2]);
                    sb.Remove(0, 3);
                } else if (sb.Length >= 3 && CellOperations.IsValidRank(sb[0]) && CellOperations.IsValidFile(sb[1]) && CellOperations.IsValidRank(sb[2])) {
                    algebraicMove.HintRankFrom = CellOperations.GetRank(sb[0]);
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[1]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[2]);
                    sb.Remove(0, 3);
                } else if (sb.Length >= 3 && sb[0] == 'x' && CellOperations.IsValidFile(sb[1]) && CellOperations.IsValidRank(sb[2])) {
                    algebraicMove.Capture = true;
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[1]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[2]);
                    sb.Remove(0, 3);
                } else if (sb.Length >= 2 && CellOperations.IsValidFile(sb[0]) && CellOperations.IsValidRank(sb[1])) {
                    algebraicMove.HintFileTo = CellOperations.GetFile(sb[0]);
                    algebraicMove.HintRankTo = CellOperations.GetRank(sb[1]);
                    sb.Remove(0, 2);
                } else {
                    throw new FormatException("Invalid move format");
                }
            } else if (sb.ToString().StartsWith("O-O-O")) {
                algebraicMove.IsLongCastle = true;
                algebraicMove.Piece = PieceOperations.GetKing(turn);
                algebraicMove.From = CellOperations.GetCastleFromCell(turn);
                algebraicMove.To = CellOperations.GetLongCastleToCell(turn);
                sb = sb.Remove(0, 5);
            } else if (sb.ToString().StartsWith("O-O")) {
                algebraicMove.IsShortCastle = true;
                algebraicMove.Piece = PieceOperations.GetKing(turn);
                algebraicMove.From = CellOperations.GetCastleFromCell(turn);
                algebraicMove.To = CellOperations.GetShortCastleToCell(turn);
                sb = sb.Remove(0, 3);
            } else {
                throw new FormatException("Invalid move format");
            }
            if (sb.Length >= 1 && sb[0] == '=') {
                if (algebraicMove.Piece != PieceType.WhitePawn && algebraicMove.Piece != PieceType.BlackPawn)
                    throw new FormatException("Only pawns can be promoted");
                switch (turn) {
                    case PieceColor.White:
                        algebraicMove.HintRankFrom = CellRank.R7;
                        algebraicMove.HintRankTo = CellRank.R8;
                        break;
                    case PieceColor.Black:
                        algebraicMove.HintRankFrom = CellRank.R2;
                        algebraicMove.HintRankTo = CellRank.R1;
                        break;
                }
                if (sb.Length < 2) throw new FormatException("Promotion piece symbol is missing");
                var promotionChar = sb[1];
                if (!PieceOperations.IsValidPromotionPiece(promotionChar)) throw new FormatException("Invalid promotion piece");
                algebraicMove.Promotion = PieceOperations.GetPromotionPiece(promotionChar, turn);
                sb = sb.Remove(0, 2);
            }
            if (sb.Length >= 1) {
                if (sb[0] == '+') {
                    algebraicMove.Check = true;
                } else if (sb[0] == '#') {
                    algebraicMove.Checkmate = true;
                }
                sb = sb.Remove(0, 1);
            }
        }

    }
}