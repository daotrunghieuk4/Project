using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChessUI
{
    public partial class HistoryWindow : Window
    {
        private SoundManager soundManager;
        private int currentUserID;
        private string username;

        public HistoryWindow(SoundManager soundManager, int userID, string username)
        {
            InitializeComponent();
            this.soundManager = soundManager;
            this.currentUserID = userID; 
            this.username = username;
            LoadGameHistory(); 
        }
        private void LoadGameHistory()
        {
            string connectionString = "Data Source=DESKTOP-SBIF5O8;Initial Catalog=UserAuthentication;User ID=sa;Password=30102004;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Result, WhitePlayer, BlackPlayer, DatePlayed " +
                                   "FROM GameHistory WHERE UserID = @UserID ORDER BY DatePlayed DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", currentUserID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["WhitePlayer"].ToString() == username)
                        {
                            row["WhitePlayer"] = username;
                        }
                    }

                    HistoryDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading game history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameMenu gameMenu = new GameMenu(soundManager, currentUserID, username);
            gameMenu.Show();
            this.Close();
        }
    }
}
