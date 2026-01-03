using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        private readonly List<WishlistItems> _items = new();
        public IReadOnlyCollection<WishlistItems> Items => _items;

        public void AddItem(int productId)
        {
            if (_items.Any(i => i.ProductId == productId))
                return;

            _items.Add(new WishlistItems
            {
                ProductId = productId,
                Name = Name,
                AddedAt = DateTime.UtcNow
            });
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                _items.Remove(item);
        }
    }
}
