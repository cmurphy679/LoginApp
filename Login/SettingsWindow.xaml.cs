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

        public string GetUsername => txtUsername.Text; // Get username

        // Change password button (Click)
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // If the password boxes are empty, print error
            if (txtPassword.Password.Length < 1 || txtNewPassword.Password.Length < 1)
            {
                MessageBox.Show("Please enter the old and new password!");
                return;
            }

            try
            {
                // Create and open connection to DB
                var sqliteCon = new SQLiteConnection(DbConnectionString);
                sqliteCon.Open();
                // Retrieve current password from the database
                var query = "SELECT password FROM userinfo WHERE username = '" + txtUsername.Text +"'";
                // Create a command with the SQL query and pass to the DB connection
                var createCommand = new SQLiteCommand(query, sqliteCon);
                // Execute the query
                createCommand.ExecuteNonQuery();

                // Create data reader
                var dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    if ((string)(dr["password"]) == txtPassword.Password) // If old password matches the database, continue
                    {
                        if (txtNewPassword.Password.Length < 1) // If new password field is empty, return
                        {
                            MessageBox.Show("New password cannot be blank!");
                            return;
                        }
                        // Update database with new password
                        query = "UPDATE userinfo SET password = '" + txtNewPassword.Password + "' WHERE username = '" + txtUsername.Text + "'";
                        // Create a command with the SQL query and pass to the DB connection
                        createCommand = new SQLiteCommand(query, sqliteCon);
                        // Execute the query
                        createCommand.ExecuteNonQuery();
                        MessageBox.Show("Password successfully changed!");
                    }
                    else // Old password didn't match the database, return
                    {
                        MessageBox.Show("Password is incorrect! Please enter the correct password in order to change!");
                        return;
                    }
                }
                sqliteCon.Close(); // Close SQLite connection
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Delete account button (Click)
        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // If the password box is empty, print error
            if (txtPassword.Password.Length < 1)
            {
                MessageBox.Show("Please enter password to continue!");
                return;
            }

            // Dialog box to confirm account deletion
            var msgResult = MessageBox.Show("Are you sure you want to delete your account?", "Delete Account", MessageBoxButton.YesNo);
            if (msgResult == MessageBoxResult.Yes)
            {
                // Create and open connection to DB
                var sqliteCon = new SQLiteConnection(DbConnectionString);
                try
                {
                    sqliteCon.Open();
                    // Delete user from database
                    var query = "DELETE FROM userinfo WHERE username = '" + txtUsername.Text + "'";
                    // Create a command with the SQL query and pass to the DB connection
                    var createCommand = new SQLiteCommand(query, sqliteCon);
                    // Execute the query
                    createCommand.ExecuteNonQuery();
                    MessageBox.Show("Account successfully deleted!");
                    sqliteCon.Close(); // Close SQLite connection
                    var login = new LoginWindow(); // Create new window
                    login.Show(); // Show the new window
                    Close(); // Close the current window
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Back button (Click)
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var profile = new ProfileWindow(GetUsername); // Create new window
            profile.Show(); // Show the new window
            Close(); // Close the current window
        }

        // Back button (MouseEnter)
        private void btnBack_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Back button (MouseExit)
        private void btnBack_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
        // Change password button (MouseEnter)
        private void btnChangePassword_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Change password button (MouseExit)
        private void btnChangePassword_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
        // Delete account button (MouseEnter)
        private void btnDeleteAccount_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Delete account button (MouseExit)
        private void btnDeleteAccount_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
    }
}
