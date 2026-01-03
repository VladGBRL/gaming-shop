using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.DTOs
{
    public class WishlistItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}
