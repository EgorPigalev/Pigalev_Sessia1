using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigalev_Sessia1
{
    public partial class User
    {
        public string FIO
        {
            get
            {
                return UserSurname + " " + UserName + " " + UserPatronymic; 
            }
        }
    }
}
