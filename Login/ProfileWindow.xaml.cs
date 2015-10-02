using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class ProfileWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";

        public ProfileWindow(string username)
        {
            InitializeComponent();
            txtUsername.Text = username; // Set username field
        }
        public string GetUsername => txtUsername.Text; // Get username

        // Update Button (MouseClick) - Update database
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Only update details if a textbox has been updated
                if (txtForename.Text.Length > 0 || txtSurname.Text.Length > 0 || txtAge.Text.Length > 0)
                {
                    // Create and open connection to DB
                    var sqliteCon = new SQLiteConnection(DbConnectionString);
                    sqliteCon.Open(); // Open SQLite connection
                    // Array holding the value for each textbox
                    string[,] inputArray =
                    {
                        {txtForename.Text, "forename"},
                        {txtSurname.Text, "surname"},
                        {txtAge.Text, "age"}
                    };
                    for (var i = 0; i < inputArray.Length/2; i++)
                    {
                        if (inputArray[i, 0].Length > 0) // If textbox is not empty
                        {
                            // Update database with new personal information from textbox
                            var query = "UPDATE userinfo SET '" + inputArray[i, 1] + "' = '" + inputArray[i, 0] +
                                        "' WHERE username = '" + txtUsername.Text + "'";
                            // Create a command with the SQL query and pass to the DB connection
                            var createCommand = new SQLiteCommand(query, sqliteCon);
                            // Execute the query
                            createCommand.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Updated details!");
                    sqliteCon.Close(); // Close SQLite connection
                }
                else
                {
                    MessageBox.Show("No details entered! Try again!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Settings button (Click)
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow(GetUsername); // Create new window
            settings.Show(); // Show the new window
            Close(); // Close the current window
        }

        // Update Button (MouseEnter)
        private void btnUpdate_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Update Button (MouseExit)
        private void btnUpdate_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
        // Advanced Settings Button (MouseEnter)
        private void btnSettings_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Advanced Settings Button (MouseExit)
        private void btnSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
    }
}
