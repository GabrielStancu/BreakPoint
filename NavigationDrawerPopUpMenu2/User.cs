using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NavigationDrawerPopUpMenu2
{

    public class User
    {
        public string Username;
        public string Password; //poza de profil
        public string Description;
        public int IdUser;
        public int AvatarUser;
        public int JobId; //de schimbat in program din id in clasa
        public int CurrentLocation;

        public User(int idUser, string inputUsername, string inputPassword, int jobId, 
            int avatarUser, int location, string description = null)
        {
            IdUser = idUser;
            Username = inputUsername;
            Password = inputPassword;
            JobId = jobId;
            AvatarUser = avatarUser;
            CurrentLocation = location;
            Description = description;
        }


        //de lucrat cu Gabi legat de CheckLogin
        public bool CheckLogin(string inputUsername, string inputPassword)
        {
            if (inputUsername == Username && inputPassword == Password)
            {
                return true;
            }
            return false;
        }

        public string GetDescription()
        {
            return Description;
        }

        public int GetIdUser()
        {
            return IdUser;
        }

        //public JobType GetJob()
        //{
        //    return Job;
        //}
    }

    
}
