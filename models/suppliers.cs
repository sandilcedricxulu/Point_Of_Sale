using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class suppliers
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public long Phone{ get; set; }
        public string Email { get; set; }
    }
}
