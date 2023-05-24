using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Managment_App
{
    public interface ILogin
    {
        bool LoginUser(string username, string password);
    }
}
