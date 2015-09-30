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

        public MainWindow()
        {
            InitializeComponent();
        }

        // Save Button (MouseClick) - Update database
        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Create and open connection to DB
            var sqliteCon = new SQLiteConnection(DbConnectionString);
            try
            {
                sqliteCon.Open();
                // Update database with new personal information from textboxes
                var query = "insert or replace into userinfo (id, forename, surname, age) values('" + txtID.Text + "','" + txtForename.Text + "','" + txtSurname.Text + "','" + txtAge.Text + "')";
                // Create a command with the SQL query and pass to the DB connection
                var createCommand = new SQLiteCommand(query, sqliteCon);
                // Execute the query
                createCommand.ExecuteNonQuery();
                MessageBox.Show("Saved!");
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Login Button (MouseEnter)
        private void btnLogin_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login2.png", UriKind.RelativeOrAbsolute)); ;
        }
        // Login Button (MouseExit)
        private void btnLogin_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgLogin.Source = new BitmapImage(new Uri(@"Images\Raccoon_Login.png", UriKind.RelativeOrAbsolute)); ;
        }
    }
}
