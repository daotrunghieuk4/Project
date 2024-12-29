using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ChessUI
{
    public partial class LoginWindow : Window
    {
        private int currentUserID;
        private SoundManager soundManager;
        public LoginWindow(SoundManager soundManager)
        {
            InitializeComponent();
            this.soundManager = soundManager;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            string connectionString = "Data Source=DESKTOP-SBIF5O8;Initial Catalog=UserAuthentication;User ID=sa;Password=30102004;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT ID FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        currentUserID = Convert.ToInt32(result);

                        GameMenu gameMenu = new GameMenu(soundManager, currentUserID, username);  // Truyền currentUserID vào GameMenu
                        gameMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Close();
        }

        private void UsernameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
