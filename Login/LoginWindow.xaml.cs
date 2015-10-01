using System.Windows;
using System.Data.SQLite;
using System;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class LoginWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";

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

            // Create and open connection to DB
            var sqliteCon = new SQLiteConnection(DbConnectionString);
            try
            {
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
                        sqliteCon.Close(); // close SQLite connection
                        var main = new MainWindow(GetUsername); // Create new window
                        main.Show(); // Show the new window
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

        // Login Button (MouseEnter)
        private void btnLogin_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Login Button (MouseExit)
        private void btnLogin_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }

    }
}
