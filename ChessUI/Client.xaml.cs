using System;
using System.Windows;

namespace ChessUI
{
    public partial class Client : Window
    {
        private static SoundManager soundManager;

        public Client()
        {
            InitializeComponent();

            if (soundManager == null)
            {
                soundManager = new SoundManager();
                soundManager.PlayBackgroundMusic();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginMenu = new LoginWindow(soundManager);
            loginMenu.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterMenu registerMenu = new RegisterMenu();
            registerMenu.Show();
            this.Close();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordMenu changePasswordMenu = new ChangePasswordMenu();
            changePasswordMenu.Show();
            this.Close();
        }
    }
}
