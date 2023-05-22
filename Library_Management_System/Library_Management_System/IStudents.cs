using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public interface IStudents
    {
        void Add_Student();

        void Delete_Student_By_ID();

        void update_Student_By_ID();

        void Search_Student_Based_On_Student_ID();
    }
}
