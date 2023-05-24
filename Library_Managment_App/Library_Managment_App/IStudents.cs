using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Managment_App
{
    public interface IStudents
    {
        int Add_Student();

        int Delete_Student();

        int update_Student();

        int View_Student();

        int View_Students();

        int Students_Havinng_Books();
    }
}
