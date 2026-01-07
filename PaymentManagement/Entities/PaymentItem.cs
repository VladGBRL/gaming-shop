using PaymentManagement.Entities.PaymentManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentManagement.Entities
{
    public class PaymentItem
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public Payment Payment { get; set; } = null!;
    }
}
