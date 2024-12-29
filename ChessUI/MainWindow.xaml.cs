using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ChessLogic;
using ChessLogic.Moves;
using ChessUI;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SoundManager soundManager;
        private int currentUserID = 0;
        private string username;

        private bool isPvE;
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPos = null;
        public MainWindow(SoundManager soundManager, int userID, string username, bool isPvE = false)
        {
            InitializeComponent();
            InitializeBoard();

            this.soundManager = soundManager;
            this.currentUserID = userID;
            this.isPvE = isPvE;
            this.username = username;

            gameState = new GameState(Player.White, Board.Initial());
            gameState.OnTimeUpdate += GameState_OnTimeUpdate;
            gameState.OnTimeOut += GameState_OnTimeOut;
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
            gameState.StartTimer();
        }

        private void GameState_OnTimeUpdate(int playerATime, int playerBTime)
        {
            Dispatcher.Invoke(() =>
            {
                PlayerATimeLabel.Text = FormatTime(playerATime);
                PlayerBTimeLabel.Text = FormatTime(playerBTime);
            });
        }
        private void GameState_OnTimeOut(string message)
        {
            gameState.StopTimer();

            string resultMessage = message.Contains("Player A") ? "Player B thắng" : "Player A thắng";
            string whitePlayer = username;
            string blackPlayer = isPvE ? "AI" : "Player 2";

            SaveGameHistory(whitePlayer, blackPlayer, resultMessage);

            Dispatcher.Invoke(() =>
            {
                MessageBox.Show(message, "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                ShowGameOver();
            });
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }
        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
            }
        }
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {

            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);

                    soundManager.PlayFootstepSound();
                }

                if (isPvE && gameState.CurrentPlayer == Player.Black)
                {
                    MakeAIMove();
                }
            }
        }
        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);
            };
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }
        private void MakeAIMove()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (s, e) =>
            {
                timer.Stop();

                Move aiMove = AIEngine.GetBestMove(gameState);

                if (aiMove != null)
                {
                    HandleMove(aiMove);
                    soundManager.PlayFootstepSound();
                }
                if (gameState.IsGameOver())
                {
                    ShowGameOver();
                }
            };

            timer.Start();
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
        private void SetCursor(Player player)
        {
            if (player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }
        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }
        private void ShowGameOver()
        {
            string result = gameState.GetGameResult();
            string whitePlayer = username;
            string blackPlayer = isPvE ? "AI" : "Player 2";

            SaveGameHistory(whitePlayer, blackPlayer, result);

            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void SaveGameHistory(string whitePlayer, string blackPlayer, string result)
        {
            whitePlayer = username;

            if (isPvE) 
            {
                blackPlayer = "AI"; 
            }
            else 
            {
                blackPlayer = "Player 2"; 
            }

            string connectionString = "Data Source=DESKTOP-SBIF5O8;Initial Catalog=UserAuthentication;User ID=sa;Password=30102004;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO GameHistory (UserID, WhitePlayer, BlackPlayer, Result, DatePlayed) 
                             VALUES (@UserID, @WhitePlayer, @BlackPlayer, @Result, GETDATE())";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserID);
                        command.Parameters.AddWithValue("@WhitePlayer", whitePlayer);
                        command.Parameters.AddWithValue("@BlackPlayer", blackPlayer);
                        command.Parameters.AddWithValue("@Result", result);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving game history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RestartGame()
        {
            if (gameState != null)
            {
                gameState.StopTimer();
                gameState.OnTimeUpdate -= GameState_OnTimeUpdate;
                gameState.OnTimeOut -= GameState_OnTimeOut;
            }

            selectedPos = null;
            HideHighlights();
            moveCache.Clear();

            gameState = new GameState(Player.White, Board.Initial());
            gameState.OnTimeUpdate += GameState_OnTimeUpdate;
            gameState.OnTimeOut += GameState_OnTimeOut;

            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
            gameState.StartTimer();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMenuOnScreen() && e.Key == Key.Enter)
            {
                ShowPauseMenu();
            }
        }
        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;

            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;

                if (option == Option.Continue)
                {
                    gameState.StartTimer();
                }
                else if (option == Option.Restart)
                {
                    RestartGame();
                }
            };
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMenuOnScreen())
            {
                gameState.StopTimer();
                ShowPauseMenu();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            soundManager.PlayBackgroundMusic();
            GameMenu gameMenu = new GameMenu(soundManager, currentUserID, username);
            gameMenu.Show();
            this.Close();
        }
    }
}