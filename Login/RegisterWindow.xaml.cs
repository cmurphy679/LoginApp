using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class RegisterWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";

        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Register button (Click)
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Only update details if all fields are populated
                if (txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0 && txtForename.Text.Length > 0 && txtSurname.Text.Length > 0 && txtAge.Text.Length > 0)
                {
                    try
                    {
                        // Create and open connection to DB
                        var sqliteCon = new SQLiteConnection(DbConnectionString);
                        sqliteCon.Open();
                        // Lookup username in the database
                        var query = "SELECT * FROM userinfo WHERE username = '" + txtUsername.Text + "'";
                        // Create a command with the SQL query and pass to the DB connection
                        var createCommand = new SQLiteCommand(query, sqliteCon);
                        // Execute the query
                        createCommand.ExecuteNonQuery();

                        // Create data reader
                        var dr = createCommand.ExecuteReader();
                        // Keep track of username count
                        var count = 0;
                        while (dr.Read())
                        {
                            count++;
                        }
                        // Login logic processes username and password
                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists! Please try again!");
                        }
                        else
                        {
                            // Insert new user into database
                            query = "INSERT INTO userinfo (forename, surname, age, username, password) VALUES('" + txtForename.Text + "','" + txtSurname.Text + "','"
                                + txtAge.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "')";
                            // Create a command with the SQL query and pass to the DB connection
                            createCommand = new SQLiteCommand(query, sqliteCon);
                            // Execute the query
                            createCommand.ExecuteNonQuery();
                            MessageBox.Show("User has been created!");
                            var login = new LoginWindow(); // Create new window
                            login.Show(); // Show the new window
                            Close(); // Close the current window
                        }
                        sqliteCon.Close(); // Close SQLite connection
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else // If a field is left incomplete
                {
                    MessageBox.Show("Please complete all form fields!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Back button (Click)
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow(); // Create new window
            login.Show(); // Show the new window
            Close(); // Close the current window
        }

        // Back button (MouseEnter)
        private void btnBack_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Back button (MouseLeave)
        private void btnBack_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
        // Register button (MouseEnter)
        private void btnRegister_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute));
        }
        // Register button (MouseLeave)
        private void btnRegister_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute));
        }
    }
}
