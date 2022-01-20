using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.View
{
    public interface IResultDisplayFilterView
    {
        string IsDone { get; set; }

        string Title { get; set; }

        string Description { get; set; }

        DateTime DueDateFrom { get; set; }

        DateTime DueDateTo { get; set; }
    }
}
