using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationDrawerPopUpMenu2
{
    public class JobType
    {
        //Administrator,      //1
        //BackEndDeveloper,   //2
        //FrontEndDeveloper,  //3
        //CallSupport,        //4
        //ProjectManager,     //5
        //Secretary,          //6
        //Intern              //7
        public int IdJob;
        public string DenumireJob;
        public int PrioritateJob;

        public JobType(int idJob, string denumireJob, int prioritateJob)
        {
            IdJob = idJob;
            DenumireJob = denumireJob;
            PrioritateJob = prioritateJob;
        }
    }
}
