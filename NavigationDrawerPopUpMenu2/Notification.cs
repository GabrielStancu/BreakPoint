using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationDrawerPopUpMenu2
{
    public class Notification
    {
        public string StartTime;
        public string FinishTime;
        public string Date;
        public string Description;
        public int InitiatorID;

        public Notification(string startTime, string finishTime, string date, string description, int initiatorID)
        {
            StartTime = startTime;
            FinishTime = finishTime;
            Date = date;
            Description = description;
            InitiatorID = initiatorID;
        }
    }
}
