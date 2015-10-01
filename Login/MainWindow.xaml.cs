using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Login
{
    public partial class MainWindow
    {
        // Initialise DB source location(Debug) and version
        private const string DbConnectionString = @"Data Source=database.db;Version=3;";

        public MainWindow(string username)
        {
            InitializeComponent();
            txtUsername.Text = username; // Set username field
        }

        // Update Button (MouseClick) - Update database
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Create and open connection to DB
            var sqliteCon = new SQLiteConnection(DbConnectionString);
            try
            {
                sqliteCon.Open(); // Open SQLite connection
                // Array holding the value for each textbox
                string[,] inputArray = { {txtForename.Text, "forename"},
                                         {txtSurname.Text, "surname"},
                                         {txtAge.Text, "age"}};
                for (var i = 0; i < inputArray.Length/2; i++)
                {
                    if (inputArray[i,0].Length > 0) // if textbox is not empty
                    {
                        // Update database with new personal information from textbox
                        var query = "update userinfo set " + inputArray[i,1] + " = " + inputArray[i,0] + " where username = " + txtUsername.Text + "";
                        // Create a command with the SQL query and pass to the DB connection
                        var createCommand = new SQLiteCommand(query, sqliteCon);
                        // Execute the query
                        createCommand.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Updated!");
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
