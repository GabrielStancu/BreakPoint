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
    public partial class MainWindow : Window
    {
        private SqlConnection conn = new SqlConnection();
        private Image pbUserCrt = new Image();
        private User userCrt;
        int pbWidth = 30, pbHeight = 30, spatiuPeRand = 5, spatiuPeColoana = 5;
        int xOff = 5, yOff = 5, xLun = 5, yLun = 5, xMeet = 5, yMeet = 5, xBrk = 5, yBrk = 5;
        int maxDeIncarcat = 3;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = userCrt.Username;
            conn.ConnectionString = Statice.ConnStr;
            conn.Open();
            loadNotif();
            loadLocatii();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        
        int indexPanou;
        public MainWindow(User user)
        {
            InitializeComponent();
            txtMenuProfile.Text = "Profile";
            txtMenuActivities.Text = "Activities";
            txtMenuFood.Text = "Add Activity";
            userCrt = user;
            indexPanou = userCrt.CurrentLocation;
            MouseDown += Window_MouseDown;
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
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void BreakBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movePbUser(indexPanou, 4);
        }

        private void OfficeBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movePbUser(indexPanou, 1);
        }

        private void LunchBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movePbUser(indexPanou, 2);
        }

        private void MeertingsBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movePbUser(indexPanou, 3);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "MenuProfile":
                    usc = new UserControlProfile(userCrt);
                    GridMain.Children.Add(usc);
                    break;
                case "MenuActivities":
                    usc = new UserControlActivities();
                    GridMain.Children.Add(GridHolder);
                    break;
                case "MenuFood":
                    usc = new UserControlFood(userCrt);
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }


        private void ListViewMenu_SelectionChangedLocation(object sender, SelectionChangedEventArgs e)
        {

        }

        private void loadNotif() 
        {
            int x = 15, y = 17, notifIncarcate = 0;
            SqlCommand getNotif = new SqlCommand();
            getNotif.Connection = conn;
            getNotif.CommandText = "SELECT Notifications.StartTime, Notifications.FinishTime, Notifications.Date, Notifications.Description, Notifications.InitiatorID, Notifications.IdNotification, Employees.UserName, Employees.Avatar FROM Notifications INNER JOIN Employees ON Notifications.InitiatorId = Employees.IdUser ORDER BY Date, StartTime";

            SqlDataReader reader = getNotif.ExecuteReader();
            while (reader.Read() && notifIncarcate < maxDeIncarcat)
            {
                string data = reader[2].ToString().Substring(8, 2) + "." + reader[2].ToString().Substring(5, 2) + "." + reader[2].ToString().Substring(0, 4);
                
                Notification notification = new Notification(reader[0].ToString(), reader[1].ToString(), data, reader[3].ToString(), Int32.Parse(reader[4].ToString()));

                Canvas pnlNotif = new Canvas();
                pnlNotif.Width = 620;
                pnlNotif.Height = 120;
                
                
                Border BorderEvents = new Border();
                BorderEvents.Margin = new Thickness(x, y, 0, 0);
                Grid GridEventsInsideBorder = new Grid();
                CanvasEvents.Children.Add(BorderEvents);
                BorderEvents.Child = GridEventsInsideBorder;
                BorderEvents.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5C99D6"));
                BorderEvents.CornerRadius = new CornerRadius(20, 20, 20, 20);
                GridEventsInsideBorder.Children.Add(pnlNotif);

                Image pb = new Image();
                pb.Width = 50;
                pb.Height = 50;
                pb.Margin = new Thickness(10, 10, 0, 0);

                atasareAvatar(pb, Int32.Parse(reader[7].ToString()));

                pb.Stretch = Stretch.Fill;

                // Set the StretchDirection property.
                pb.StretchDirection = StretchDirection.Both;

                TextBlock txtNume = new TextBlock();
                Panel.SetZIndex(txtNume, 150);
                txtNume.Text = reader[6].ToString();
                txtNume.Visibility = Visibility.Hidden;
                txtNume.IsEnabled = false;

                txtNume.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                txtNume.FontSize = 20;
                txtNume.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                txtNume.Margin = new Thickness(55, -10, 0, 0);
                pnlNotif.Children.Add(txtNume);


                pb.MouseEnter += new MouseEventHandler((sender, e) => displayNameAvatarHover(sender, e, txtNume));
                pb.MouseLeave += new MouseEventHandler((sender, e) => hideNameAvatarHover(sender, e, txtNume));

                pnlNotif.Children.Add(pb);

                Label lblStart = new Label(), lblFinish = new Label(), lblDate = new Label(), lblDescription = new Label();

                lblStart.Margin = new Thickness(10, 85, 0, 0); //de scalat
                lblStart.Content = notification.StartTime;
                lblStart.FontSize = 16;
                lblStart.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
               // lblStart.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00")); 

                lblFinish.Margin = new Thickness(75, 85, 0, 0);
                lblFinish.Content = notification.FinishTime;
                lblFinish.FontSize = 16;
                lblFinish.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                //lblFinish.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));

                lblDate.Margin = new Thickness(520, 85, 0, 0);
                lblDate.Content = notification.Date;
                lblDate.FontSize = 16;
                lblDate.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                //lblDate.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));

                lblDescription.Margin = new Thickness(70, 10, 0, 0);
                //lblDescription.AutoSize = false;
                lblDescription.Width = 530;
                lblDescription.Height = 75;
                lblDescription.Content = notification.Description;
                lblDescription.FontSize = 20;
                lblDescription.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                //lblDescription.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));

                pnlNotif.Children.Add(lblStart);
                pnlNotif.Children.Add(lblFinish);
                pnlNotif.Children.Add(lblDate);
                pnlNotif.Children.Add(lblDescription);

                y += 140;
                notifIncarcate++;
            }
            reader.Close();
        }

        private void displayNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = true;
            txt.Visibility = Visibility.Visible;
        }

        private void hideNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = false;
            txt.Visibility = Visibility.Hidden;
        }

        private void loadLocatii()
        {

            SqlCommand getLocatii = new SqlCommand();
            getLocatii.Connection = conn;
            getLocatii.CommandText = "SELECT IdUser, LocationId, Avatar, Username FROM Employees";

            SqlDataReader reader = getLocatii.ExecuteReader();

            while (reader.Read())
            {
                Location loc = new Location(Int32.Parse(reader[1].ToString()), "nume_generic"); //aici de preluat numele cum trebuie

                Image pbUser = new Image();
                
                TextBlock txtNume = new TextBlock();
                Panel.SetZIndex(txtNume, 150);
                txtNume.Text = reader[3].ToString();
                txtNume.Visibility = Visibility.Hidden;
                txtNume.IsEnabled = false;
                
                txtNume.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                txtNume.FontSize = 20;
                txtNume.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
               
                
                if(loc.IdLocation == 1)
                {
                    txtNume.Margin = new Thickness(xOff, yOff - 30, 0, 0);
                    pnlOffice.Children.Add(txtNume);
                }
                else if(loc.IdLocation == 2)
                {
                    txtNume.Margin = new Thickness(xLun, yLun - 30, 0, 0);
                    pnlLunch.Children.Add(txtNume);
                }
                else if (loc.IdLocation == 3)
                {
                    txtNume.Margin = new Thickness(xMeet, yMeet - 30, 0, 0);
                    pnlMeeting.Children.Add(txtNume);
                }
                else
                {
                    txtNume.Margin = new Thickness(xBrk, yBrk - 30, 0, 0);
                    pnlBreak.Children.Add(txtNume);
                }

                    pbUser.MouseEnter += new MouseEventHandler((sender, e) => displayNameAvatarHover(sender, e, txtNume));
                pbUser.MouseLeave += new MouseEventHandler((sender, e) => hideNameAvatarHover(sender, e, txtNume));
                pbUser.Width = pbWidth;
                pbUser.Height = pbHeight;

                atasareAvatar(pbUser, Int32.Parse(reader[2].ToString()));

                if (loc.IdLocation == 1)
                {
                    pbUser.Margin = new Thickness(xOff, yOff, 0, 0);
                    pnlOffice.Children.Add(pbUser);
                    xOff += pbWidth + spatiuPeRand;

                    if (xOff >= pnlOffice.Width - pbWidth - 2 * spatiuPeRand)
                    {
                        xOff = spatiuPeRand;
                        yOff += pbHeight + spatiuPeColoana;
                    }
                }
                else if (loc.IdLocation == 2)
                {
                    pbUser.Margin = new Thickness(xLun, yLun, 0, 0);
                    pnlLunch.Children.Add(pbUser);
                    xLun += pbWidth + spatiuPeRand;

                    if (xLun >= pnlLunch.Width - pbWidth - 2 * spatiuPeRand)
                    {
                        xLun = spatiuPeRand;
                        yLun += pbHeight + spatiuPeColoana;
                    }
                }
                else if (loc.IdLocation == 3)
                {
                    pbUser.Margin = new Thickness(xMeet, yMeet, 0, 0);
                    pnlMeeting.Children.Add(pbUser);
                    xMeet += pbWidth + spatiuPeRand;

                    if (xMeet >= pnlMeeting.Width - pbWidth - 2 * spatiuPeRand)
                    {
                        xMeet = spatiuPeRand;
                        yMeet += pbHeight + spatiuPeColoana;
                    }
                }
                else if (loc.IdLocation == 4)
                {
                    pbUser.Margin = new Thickness(xBrk, yBrk, 0, 0);
                    pnlBreak.Children.Add(pbUser);
                    xBrk += pbWidth + spatiuPeRand;

                    if (xBrk >= pnlBreak.Width - pbWidth - 2 * spatiuPeRand)
                    {
                        xBrk = spatiuPeRand;
                        yBrk += pbHeight + spatiuPeColoana;
                    }
                }

                if (Int32.Parse(reader[0].ToString()) == userCrt.IdUser)
                {
                    pbUserCrt = pbUser; //identificare picturebox user curent
                    indexPanou = loc.IdLocation;
                }
            }
            reader.Close();
        }

        private void movePbUser(int idPanouPlecare, int idPanouSosire)
        {
            if (indexPanou == idPanouSosire)
                return;         
            searchPb(idPanouPlecare);
            if (idPanouSosire == 1)
            {
                adaugarePb(pnlOffice, pbUserCrt, xOff, yOff);
            }
            else if (idPanouSosire == 2)
            {
                adaugarePb(pnlLunch, pbUserCrt, xLun, yLun);
            }
            else if (idPanouSosire == 3)
            {
                adaugarePb(pnlMeeting, pbUserCrt, xMeet, yMeet);
            }
            else if (idPanouSosire == 4)
            {
                adaugarePb(pnlBreak, pbUserCrt, xBrk, yBrk);
            }
            indexPanou = idPanouSosire;
            
        }

        private void adaugarePb(Canvas pnlSosire, Image pb, int x, int y)
        {
            int idLocatie;
            pb.Width = pbWidth;
            pb.Height = pbHeight;
            pb.Margin = new Thickness(x, y, 0, 0);
            pnlSosire.Children.Add(pb);
            pbUserCrt = pb;
            x += pbWidth + spatiuPeRand;

            if (x >= pnlSosire.Width - pbWidth - 2 * spatiuPeRand)
            {
                x = spatiuPeRand;
                y += pbHeight + spatiuPeColoana;
            }

            if (pnlSosire == pnlOffice)
                idLocatie = 1;
            else if (pnlSosire == pnlLunch)
                idLocatie = 2;
            else if (pnlSosire == pnlMeeting)
                idLocatie = 3;
            else //if (pnlSosire == pnlBreak)
                idLocatie = 4;

            SqlCommand updateLocatii = new SqlCommand();
            updateLocatii.Connection = conn;
            updateLocatii.CommandText = "UPDATE Employees SET LocationID = @locatie WHERE IdUser = @id";
            updateLocatii.Parameters.AddWithValue("@locatie", idLocatie);
            updateLocatii.Parameters.AddWithValue("@id", userCrt.IdUser);
            updateLocatii.ExecuteNonQuery();
        }

        private void searchPb(int idPanou)
        {
            Canvas pnlPlecare;
            if (idPanou == 1)
                pnlPlecare = pnlOffice;
            else if (idPanou == 2)
                pnlPlecare = pnlLunch;
            else if (idPanou == 3)
                pnlPlecare = pnlMeeting;
            else //if (idPanou == 4)
                pnlPlecare = pnlBreak;

            foreach (Image pb in pnlPlecare.Children.OfType<Image>())
            {
                if (pb == pbUserCrt)
                {
                    pnlPlecare.Children.Remove(pb);
                    //pb.Source = null;
                    break;
                }
            }


        }

        private void atasareAvatar(Image pbUser, int idAvatar)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            if (idAvatar == 1)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy-1.png");
            else if (idAvatar == 2)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy.png");
            else if (idAvatar == 3)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl-1.png");
            else if (idAvatar == 4)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl.png");
            else if (idAvatar == 5)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man.png");
            else if (idAvatar == 6)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-1.png");
            else if (idAvatar == 7)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-2.png");
            else if (idAvatar == 8)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-3.png");
            else //if (idUser == 9)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-4.png");
            bi3.EndInit();
            pbUser.Stretch = Stretch.Fill;
            pbUser.Source = bi3;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (Image img in pnlOffice.Children.OfType<Image>())
            {
                img.Source = null;
            }
            pnlOffice.Children.Clear();
            xOff = spatiuPeRand;
            yOff = spatiuPeColoana;

            foreach (Image img in pnlLunch.Children.OfType<Image>())
            {
                img.Source = null;
            }
            pnlLunch.Children.Clear();
            xLun = spatiuPeRand;
            yLun = spatiuPeColoana;

            foreach (Image img in pnlMeeting.Children.OfType<Image>())
            {
                img.Source = null;
            }
            pnlMeeting.Children.Clear();
            xMeet = spatiuPeRand;
            yMeet = spatiuPeColoana;

            foreach (Image img in pnlBreak.Children.OfType<Image>())
            {
                img.Source = null;
            }
            pnlBreak.Children.Clear();
            xBrk = spatiuPeRand;
            yBrk = spatiuPeColoana;

            loadLocatii();
            loadNotif();
        }

        private void findPerson(string search)
        {
            SqlCommand searchPerson = new SqlCommand();
            searchPerson.Connection = conn;
            searchPerson.CommandText = "";
        }
    }
}
