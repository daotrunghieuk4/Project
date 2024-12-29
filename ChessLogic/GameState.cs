using System;
using System.Timers;

namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }

        public Player CurrentPlayer { get; private set; }

        public Result Result { get; private set; } = null;

        public int PlayerATimeInSeconds { get; private set; } = 600; 
        public int PlayerBTimeInSeconds { get; private set; } = 600; 
        private bool isPlayerATurn = true;
        private System.Timers.Timer timer;

        private int noCaptureOrPawnMoves = 0;
        private string stateString;

        private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

        public event Action<int, int> OnTimeUpdate; 
        public event Action<string> OnTimeOut; 
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;

            stateString = new StateString(CurrentPlayer, board).ToString();
            stateHistory[stateString] = 1;

            timer = new System.Timers.Timer(1000); 
            timer.Elapsed += Timer_Elapsed;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isPlayerATurn)
            {
                PlayerATimeInSeconds--;
                if (PlayerATimeInSeconds <= 0)
                {
                    timer.Stop();
                    OnTimeOut?.Invoke("Player A hết giờ! Player B thắng!");
                }
            }
            else
            {
                PlayerBTimeInSeconds--;
                if (PlayerBTimeInSeconds <= 0)
                {
                    timer.Stop();
                    OnTimeOut?.Invoke("Player B hết giờ! Player A thắng!");
                }
            }

            OnTimeUpdate?.Invoke(PlayerATimeInSeconds, PlayerBTimeInSeconds);
        }
        public void StartTimer()
        {
            if (!timer.Enabled)
            {
                timer.Start();
            }
        }

        public void StopTimer()
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
        }

        public void Dispose()
        {
            StopTimer();
            if (timer != null)
            {
                timer.Dispose();
                timer = null; 
            }
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            StopTimer();

            Board.SetPawnSkipPosition(CurrentPlayer, null);

            bool captureOrPawn = move.Execute(Board);

            if (captureOrPawn)
            {
                noCaptureOrPawnMoves = 0;
                stateHistory.Clear();
            }
            else
            {
                noCaptureOrPawnMoves++;
            }

            CurrentPlayer = CurrentPlayer.Opponent(); 

            if (CurrentPlayer == Player.White)
            {
                isPlayerATurn = true;
            }
            else 
            {
                isPlayerATurn = false;
            }

            UpdateStateString();

            StartTimer(); 
            CheckForGameOver(); 
        }


        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.IsLegal(Board)).ToList();
        }

        public IEnumerable<Move> AllLegalMovesForCurrentPlayer()
        {
            IEnumerable<Position> positions = Board.PiecePositionsFor(CurrentPlayer);

            List<Move> legalMoves = new List<Move>();

            foreach (var pos in positions)
            {
                IEnumerable<Move> pieceMoves = LegalMovesForPiece(pos);
                legalMoves.AddRange(pieceMoves);
            }

            return legalMoves;
        }
        private void CheckForGameOver()
        {
            if (!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent()); 
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial); 
            }
            else if (FiftyMoveRule())
            {
                Result = Result.Draw(EndReason.FiftyMoveRule); 
            }
            else if (ThreefoldRepetition())
            {
                Result = Result.Draw(EndReason.ThreefoldRepetition); 
            }

            if (IsGameOver())
            {
                StopTimer();
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        private bool FiftyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 50;
        }

        private void UpdateStateString()
        {
            stateString = new StateString(CurrentPlayer, Board).ToString();

            if (!stateHistory.ContainsKey(stateString))
            {
                stateHistory[stateString] = 1;
            }
            else
            {
                stateHistory[stateString]++;
            }
        }
        private bool ThreefoldRepetition()
        {
            return stateHistory[stateString] == 3;
        }

        public string GetGameResult()
        {
            if (Result == null)
            {
                return "Game is still ongoing.";
            }

            return Result.ToString();
        }

    }
}