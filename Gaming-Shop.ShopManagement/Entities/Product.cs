using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int SupplierID { get; set; }
        public int CategoryID { get; set; }

        public Supplier Supplier { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
