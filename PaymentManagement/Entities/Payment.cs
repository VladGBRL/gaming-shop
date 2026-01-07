using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Entities
{
    namespace PaymentManagement.Data
    {
        public class Payment
        {
            public int Id { get; set; }
            public int UserId { get; set; }

            public decimal TotalAmount { get; set; }
            public string Currency { get; set; } = "ron";

            public string Status { get; set; } = string.Empty;// Pending / Paid / Failed
            public string StripeSessionId { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; }
            public DateTime? PaidAt { get; set; }

            public ICollection<PaymentItem> Items { get; set; } = new List<PaymentItem>();
        }
    }

}
