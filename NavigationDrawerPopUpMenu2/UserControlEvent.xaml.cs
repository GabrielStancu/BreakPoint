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
    public partial class UserControlFood : UserControl
    {
        private SqlConnection conn = new SqlConnection();
        User UserCrt;
        public UserControlFood(User user)
        {
            InitializeComponent();
            UserCrt = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int idEvent = 0;
            SqlCommand getMax = new SqlCommand();
            getMax.Connection = conn;
            getMax.CommandText = "SELECT MAX(IdNotification) FROM Notifications";
            SqlDataReader reader = getMax.ExecuteReader();
            if (reader.Read())
            {
                idEvent = Int32.Parse(reader[0].ToString());
                idEvent++;
            }
            reader.Close();

            SqlCommand updateActivitate = new SqlCommand();
            updateActivitate.Connection = conn;
            updateActivitate.CommandText = "INSERT INTO Notifications(IdNotification, StartTime, FinishTime, Date, Description, InitiatorID) VALUES(@idEvent, @start, @finish, @date, @description, @id)";
            updateActivitate.Parameters.AddWithValue("@idEvent", idEvent);
            updateActivitate.Parameters.AddWithValue("@start", OraIncepere.Text);
            updateActivitate.Parameters.AddWithValue("@finish", OraTerminare.Text);
            updateActivitate.Parameters.AddWithValue("@description", Descriere.Text);
            updateActivitate.Parameters.AddWithValue("@date", DataEvent.Text.Substring(6, 4) + "." + DataEvent.Text.Substring(3, 2) + "." + DataEvent.Text.Substring(0, 2));
            updateActivitate.Parameters.AddWithValue("@id", UserCrt.IdUser);
            updateActivitate.ExecuteNonQuery();

            MessageBox.Show("Event successfully created!");
            OraIncepere.Text = string.Empty;
            OraTerminare.Text = string.Empty;
            Descriere.Text = string.Empty;
            DataEvent.Text = string.Empty;
        }

        private void GridMain_Loaded(object sender, RoutedEventArgs e)
        {

            conn.ConnectionString = Statice.ConnStr;
            conn.Open();

            //incarcare imagini
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\coffee.png");
            bi3.EndInit();
            Coffee.Stretch = Stretch.Fill;
            Coffee.Source = bi3;

            BitmapImage bi4 = new BitmapImage();
            bi4.BeginInit();
            bi4.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\gaming.png");
            bi4.EndInit();
            Gaming.Stretch = Stretch.Fill;
            Gaming.Source = bi4;

            BitmapImage bi5 = new BitmapImage();
            bi5.BeginInit();
            bi5.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\movie.png");
            bi5.EndInit();
            Movie.Stretch = Stretch.Fill;
            Movie.Source = bi5;

            BitmapImage bi6 = new BitmapImage();
            bi6.BeginInit();
            bi6.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\party.png");
            bi6.EndInit();
            Party.Stretch = Stretch.Fill;
            Party.Source = bi6;

            BitmapImage bi7 = new BitmapImage();
            bi7.BeginInit();
            bi7.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\ping-pong.png");
            bi7.EndInit();
            PingPong.Stretch = Stretch.Fill;
            PingPong.Source = bi7;

            BitmapImage bi8 = new BitmapImage();
            bi8.BeginInit();
            bi8.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\restaurant.jpg");
            bi8.EndInit();
            Restaurant.Stretch = Stretch.Fill;
            Restaurant.Source = bi8;
        }
    }
}
