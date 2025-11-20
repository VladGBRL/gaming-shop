using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.DTOs
{
    public class SupplierGetDTO
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
