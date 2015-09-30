using System.Windows;
using System.Data.SQLite;
using System;

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Create and open connection to DB
            var sqliteCon = new SQLiteConnection(DbConnectionString);
            try
            {
                sqliteCon.Open();
                // Lookup username + password from textboxes in the DB
                var query = "select * from userinfo where username='" + txtUsername.Text +
                               "' and password='" + txtPassword.Password + "'";
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
                    case 0:
                        MessageBox.Show("Username and password is incorrect! Try again!");
                        break;
                    case 1:
                        var main = new MainWindow();
                        main.ShowDialog();
                        break;
                    default:
                        if (count > 1)
                        {
                            MessageBox.Show("Duplicate username and password! Try again!");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
