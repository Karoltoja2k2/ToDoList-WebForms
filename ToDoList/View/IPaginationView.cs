using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.View
{
    public interface IPaginationView
    {
        int CurrentPage { get; set; }

        int Total { get; set; }

        int Amount { get; set; }
    }
}
