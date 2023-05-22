using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public interface IBooks
    {
        void Add_Book();

        void update_Book_By_ID();

        void Delete_Book_By_ID();

        void Issue_Book();

        void Return_Book();
    }
}
