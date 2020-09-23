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
    public partial class SignIn : Window
    {
        private SqlConnection conn = new SqlConnection();
        public SignIn()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

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

            try
            {
                conn.Open();
            }
            catch
            {
                //MessageBox.Show("No internet connection. Please check your internet connection.");
                //Application.Current.Shutdown();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtLogUserName.Text == string.Empty && txtLogPassword.Password == string.Empty)
            {
                MessageBox.Show("No username and no password provided...");
                return;
            }
            else if (txtLogUserName.Text == string.Empty)
            {
                MessageBox.Show("No username provided...");
                return;
            }
            else if (txtLogPassword.Password == string.Empty)
            {
                MessageBox.Show("No password provided...");
                return;
            }


            SqlCommand verifUser = new SqlCommand();
            verifUser.CommandText = "SELECT * FROM Employees WHERE Username Like @user";
            verifUser.Connection = conn;
            verifUser.Parameters.AddWithValue("@user", txtLogUserName.Text);
            verifUser.Parameters.AddWithValue("@pass", txtLogPassword.Password);

            SqlDataReader reader = verifUser.ExecuteReader();
            if (reader.Read())
            {
                if (reader[2].ToString() != txtLogPassword.Password)
                {
                    MessageBox.Show("Username or password inserted incorectly, please try again...");
                    txtLogUserName.Text = string.Empty;
                    txtLogPassword.Password = string.Empty;
                    reader.Close();
                    return;
                }

                User usrCrt = new User(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), Int32.Parse(reader[5].ToString()), reader[6].ToString());
                //eventual daca avem alte statistici mai tarziu 

                reader.Close();
                MainWindow window = new MainWindow(usrCrt);
                this.Hide();
                window.Show();
                this.Close();


            }
            else
            {
                MessageBox.Show("The user does not exist...");
                txtLogUserName.Text = string.Empty;
                txtLogPassword.Password = string.Empty;
                reader.Close();
            }

        }

        private void LblSignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SignUp window = new SignUp();
            this.Hide();
            window.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            conn.Close();
        }
    }
}
