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
using System.IO;

namespace NavigationDrawerPopUpMenu2
{
    public partial class UserControlProfile : UserControl
    {
        User CrtUser;
        public UserControlProfile(User user)
        {
            InitializeComponent();
            CrtUser = user;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            JobName.Text = Statice.JobToString[CrtUser.JobId];
            RankName.Text = Statice.Rep[CrtUser.JobId].ToString(); 
            UserName.Text = CrtUser.Username;
            Description.Text = CrtUser.Description;
            atasareAvatar(ProfileImage, CrtUser.AvatarUser);
            atasareEventChart(EventsChart, CrtUser.IdUser);
        }

        private void atasareAvatar(Image pbUser, int idUser)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            if (idUser == 1)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy-1.png");
            else if (idUser == 2)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy.png");
            else if (idUser == 3)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl-1.png");
            else if (idUser == 4)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl.png");
            else if (idUser == 5)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man.png");
            else if (idUser == 6)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-1.png");
            else if (idUser == 7)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-2.png");
            else if (idUser == 8)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-3.png");
            else //if (idUser == 9)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-4.png");
            bi3.EndInit();
            pbUser.Stretch = Stretch.Fill;
            pbUser.Source = bi3;
            pbUser.Height = 125;
            pbUser.Width = 125;
            pbUser.Margin = new Thickness(0, 0, 0, 30);

        }

        private void atasareEventChart(Image pbUser, int idUser)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            int chart = idUser % 4;
            if(chart == 0)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\1.png");
            else if(chart == 1)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\2.png");
            else if (chart == 2)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\3.png");
            else
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\4.png");
            bi3.EndInit();
            pbUser.Stretch = Stretch.Fill;
            pbUser.Source = bi3;
            //pbUser.Margin = new Thickness(0, 0, 0, 30);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.gif"; // add more image formats if desired, semi-colon separated, format: *.<extension>
            openFileDialog.CheckPathExists = true;
            bool? result = openFileDialog.ShowDialog();
            string path;
            if (result.HasValue && result.Value)
            {
                path = openFileDialog.FileName;
                BitmapImage source = new BitmapImage(new Uri(path));
                ProfileImage.Source = source;
                ProfileImage.Stretch = Stretch.Fill;
                ProfileImage.Height = 125;
                ProfileImage.Width = 125;
                ProfileImage.Margin = new Thickness(0, 0, 0, 30);

            }
            else
            {
                // user did not select image
                return;
            }
        }
    }
}
