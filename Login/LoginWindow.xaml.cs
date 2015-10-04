using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class LoginWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";
        // Holds attempted login attempts for each user (max of 3)
        readonly Dictionary<string, int> _userTable = new Dictionary<string, int>();

        public LoginWindow()
        {
            InitializeComponent();
        }

        public string GetUsername => txtUsername.Text; // Get username

        // Login Button (MouseClick)
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // If the username or password box are empty, print error
            if (txtUsername.Text.Length < 1 || txtPassword.Password.Length < 1)
            {
                MessageBox.Show("Please enter a username and password before continuing!");
                return;
            }

            // Check login attempt count for this username
            int userCount;
            if (_userTable.TryGetValue(txtUsername.Text, out userCount))
            {
                if (userCount >= 3) // If over 3, print alert and return
                {
                    MessageBox.Show("You have attempted to login as " + txtUsername.Text + " too many times(3)! Account locked!");
                    return;
                }
                _userTable[txtUsername.Text] = userCount + 1; // Else, increase count for this user
            }
            else // If this is first login attempt, add user to database
            {
                _userTable.Add(txtUsername.Text, 1);
            }

            try
            {
                // Create and open connection to DB
                var sqliteCon = new SQLiteConnection(DbConnectionString);
                sqliteCon.Open();
                // Lookup username + password from textboxes in the DB
                var query = "SELECT * FROM userinfo WHERE username = '" + txtUsername.Text +
                               "' AND password = '" + txtPassword.Password + "'";
                // Create a command with the SQL query and pass to the DB connection
                var createCommand = new SQLiteCommand(query, sqliteCon);
                // Execute the query
                createCommand.ExecuteNonQuery();

                // Create data reader
                var dr = createCommand.ExecuteReader();
                // Keep track of attempted login count
                var count = 0;
                while (dr.Read())
                {
                    count++;
                }
                // Login logic processes username and password
                switch (count)
                {
                    case 1:
                        sqliteCon.Close(); // Close SQLite connection
                        var profile = new ProfileWindow(GetUsername); // Create new window
                        profile.Show(); // Show the new window
                        Close(); // Close the current window
                        break;
                    default:
                        MessageBox.Show("Username and password is incorrect! Try again!");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Register Button (Click)
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var register = new RegisterWindow(); // Create new window
            register.Show(); // Show the new window
            Close(); // Close the current window
        }

        // Login Button (MouseEnter)
        private void btnLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Login Button (MouseExit)
        private void btnLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
        // Register Button (MouseExit)
        private void btnRegister_MouseEnter(object sender, MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Register Button (MouseExit)
        private void btnRegister_MouseLeave(object sender, MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
    }
}
