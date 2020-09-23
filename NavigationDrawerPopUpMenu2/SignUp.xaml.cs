using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace NavigationDrawerPopUpMenu2
{
    public partial class SignUp : Window
    {
        private SqlConnection conn = new SqlConnection();
        public SignUp()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn.ConnectionString = Statice.ConnStr;
            conn.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogUserName.Text == string.Empty)
            {
                MessageBox.Show("No username provided...");
                return;
            }
            else if (txtLogPassword.Password == string.Empty)
            {
                MessageBox.Show("No password provided...");
                return;
            }
            else if (txtConfirmLogPassword.Password == string.Empty)
            {
                MessageBox.Show("The password was not confirmed...");
                return;
            }
            else if (txtLogPassword.Password != txtConfirmLogPassword.Password)
            {
                MessageBox.Show("The passwords inserted do NOT match...");
                txtLogPassword.Password = string.Empty;
                txtConfirmLogPassword.Password = string.Empty;
                return;
            }

            if (userExists())
            {
                MessageBox.Show("The username already exists...");
                txtLogUserName.Text = string.Empty;
                txtLogPassword.Password = string.Empty;
                txtConfirmLogPassword.Password = string.Empty;
            }

            int id = getNextId();
            int avatar = 1; //deocamdata, de inlocuit cu random, la fel si la job, dar de la 2 incolo
            int jobId = 0; //tot random

            User newUser = new User(id, txtLogUserName.Text, txtLogPassword.Password, jobId, avatar, 1, string.Empty);

            SqlCommand cmdNewUser = new SqlCommand();
            cmdNewUser.Connection = conn;
            cmdNewUser.CommandText = "INSERT INTO Employees VALUES(@id, @nume, @pass, @job, @avatar, @locatie, @descriere)";
            cmdNewUser.Parameters.AddWithValue("@id", newUser.IdUser);
            cmdNewUser.Parameters.AddWithValue("@nume", newUser.Username);
            cmdNewUser.Parameters.AddWithValue("@pass", newUser.Password);
            cmdNewUser.Parameters.AddWithValue("@job", newUser.JobId);
            cmdNewUser.Parameters.AddWithValue("@avatar", newUser.AvatarUser);
            cmdNewUser.Parameters.AddWithValue("@locatie", newUser.CurrentLocation);
            cmdNewUser.Parameters.AddWithValue("@descriere", newUser.Description);


            cmdNewUser.ExecuteNonQuery();

            SignIn window = new SignIn();
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        private int getNextId()
        {
            int id = 0;

            SqlCommand getMaxId = new SqlCommand();
            getMaxId.Connection = conn;
            getMaxId.CommandText = "SELECT MAX(IdUser) FROM Employees";
            SqlDataReader reader = getMaxId.ExecuteReader();
            if (reader.Read())
            {
                id = Int32.Parse(reader[0].ToString()) + 1;
            }
            reader.Close();

            return id;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            conn.Close();
        }

        private Boolean userExists()
        {
            bool exists = false;
            SqlCommand searchUser = new SqlCommand();
            searchUser.Connection = conn;
            searchUser.CommandText = "SELECT * FROM Employees WHERE UserName = @user";
            searchUser.Parameters.AddWithValue("@user", txtLogUserName.Text);

            SqlDataReader reader = searchUser.ExecuteReader();
            if (reader.Read())
            {
                exists = true;
            }
            reader.Close();
            return exists;
        }
    }
}
