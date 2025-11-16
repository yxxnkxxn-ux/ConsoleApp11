using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace namespace ConsoleApp11.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Memo { get; set; }
        public DateTime DueDate { get; set; }

        public int DaysLeft()
        {
            return (DueDate.Date - DateTime.Now.Date).Days;
        }

        public bool IsDueTomorrow()
        {
            return DaysLeft() == 1;
        }
    }
}
