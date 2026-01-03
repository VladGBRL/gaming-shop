using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.DTOs
{
    public class WishlistDto
    {
        public int UserId { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new();
    }
}
