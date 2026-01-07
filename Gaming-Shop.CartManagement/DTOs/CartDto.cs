using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.CartManagement.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; } = Enumerable.Empty<CartItemDto>();
        public decimal TotalPrice { get; set; }
    }
}
