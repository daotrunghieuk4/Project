using System;
using System.IO;
using System.Windows;

namespace ChessUI
{
    public partial class GameMenu : Window
    {
        private SoundManager soundManager;
        private int currentUserID;
        private string username;
        public GameMenu(SoundManager soundManager, int userID, string username)
        {
            InitializeComponent();
            this.soundManager = soundManager;
            this.currentUserID = userID;
            this.username = username;
        }
        private void PlayWithHumanButton_Click(object sender, RoutedEventArgs e)
        {
            soundManager.StopBackgroundMusic();
            var mainWindow = new MainWindow(soundManager, currentUserID, username,isPvE: false);
            mainWindow.Show();
            this.Close();
        }
        private void PlayWithAIButton_Click(object sender, RoutedEventArgs e)
        {
            soundManager.StopBackgroundMusic();
            var mainWindow = new MainWindow(soundManager, currentUserID, username,isPvE: true);
            mainWindow.Show();
            this.Close();
        }
        private void ViewHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow(soundManager, currentUserID, username);
            historyWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(soundManager);
            loginWindow.Show();
            this.Close();
        }
    }
}
