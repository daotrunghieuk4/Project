using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace ChessUI
{
    public partial class ChangePasswordMenu : Window
    {
        public ChangePasswordMenu()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Close();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string oldPassword = OldPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmNewPasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-SBIF5O8;Initial Catalog=UserAuthentication;User ID=sa;Password=30102004;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkOldPasswordQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @OldPassword";
                    using (SqlCommand checkCmd = new SqlCommand(checkOldPasswordQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        checkCmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Old password is incorrect for the given username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    string updatePasswordQuery = "UPDATE Users SET Password = @NewPassword WHERE Username = @Username";
                    using (SqlCommand updateCmd = new SqlCommand(updatePasswordQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@Username", username);
                        updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
