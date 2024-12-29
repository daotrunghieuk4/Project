using System;
using System.Collections.Generic;
using System.Linq;
using ChessLogic;

public static class AIEngine
{
    private static Random random = new Random();

    private static readonly Dictionary<PieceType, int> PieceValues = new Dictionary<PieceType, int>
    {
        { PieceType.King, 1000 },
        { PieceType.Queen, 9 },  
        { PieceType.Rook, 5 },    
        { PieceType.Bishop, 3 }, 
        { PieceType.Knight, 3 },  
        { PieceType.Pawn, 1 }     
    };

    public static Move GetBestMove(GameState gameState)
    {
        IEnumerable<Move> legalMoves = gameState.AllLegalMovesForCurrentPlayer();

        if (!legalMoves.Any())
        {
            return null;
        }

        List<(Move move, int value)> capturingMoves = new List<(Move, int)>();

        foreach (var move in legalMoves)
        {
            Piece capturedPiece = gameState.Board[move.ToPos.Row, move.ToPos.Column];
            if (capturedPiece != null && capturedPiece.Color != gameState.CurrentPlayer)
            {
                int capturedValue = PieceValues[capturedPiece.Type];
                capturingMoves.Add((move, capturedValue));
            }
        }

        if (capturingMoves.Any())
        {
            var bestMove = capturingMoves.OrderByDescending(m => m.value).First().move;
            return bestMove;
        }

        return legalMoves.ElementAt(random.Next(legalMoves.Count()));
    }
}
