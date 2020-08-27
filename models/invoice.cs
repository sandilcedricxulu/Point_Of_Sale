using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    class invoice
    {
        public int InvoiceNumber { get; set; }
        public int EmployeeNumber { get; set; }
        public int BarCode { get; set; }
        public int Category { get; set; }
        public double UnitPrice { get; set; }
    }
}
