using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NavigationDrawerPopUpMenu2
{
    static public class Statice
    {
        static public int[] Rep = {20, 10, 30, 55, 100,
                            50, 23, 56, 44, 99,
                            152, 21, 53, 71, 17,
                            1, 26, 88, 79, 32};
        static public string[] JobToString = { "", "Administrator", "Backend Developer", "Frontend Developer", "Call Support", "Manager", "Secretary", "Intern" };

        public static string connStrLocal = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + @"\dbOffice.mdf;Integrated Security=True";

        private static string ipServer = "192.168.43.2";
        private static string user = "abstract";
        private static string password = "acon";
        public static string connStrOnline = "Data Source=" + ipServer + ",1433;Network Library=DBMSSOCN; Initial Catalog = dbOffice; User ID = "+ user +"; Password = " + password + "; ";

        public static string ConnStr = connStrLocal;
    }
}
