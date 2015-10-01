using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class SettingsWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";

        public SettingsWindow(string username)
        {
            InitializeComponent();
            txtUsername.Text = username; // Set username field
        }
    }
}
