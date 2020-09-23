using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationDrawerPopUpMenu2
{
    public class Location
    {
        public int IdLocation;
        public string Name;

        public Location(int idLocation, string name)
        {
            IdLocation = idLocation;
            Name = name;
        }

        public int GetLocationId()
        {
            return IdLocation;
        }

        public string GetLocationName(int idLocation)
        {
            return Name;
        }
    }
}
