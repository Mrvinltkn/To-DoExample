using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoExample.Models
{
    public class DateFilterModel
    {
        public int Deadline { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}