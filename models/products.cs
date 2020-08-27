using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class products
    {
        public long BarCode { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public int QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public int ReorderLevel { get; set; }
        public string Discontinued { get; set; }
    }
}
