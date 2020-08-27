using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class employees
    {
        public int EmployeeNumber { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReportTo { get; set; }
        public int PositionID { get; set; }
    }
}
