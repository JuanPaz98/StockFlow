using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockFlow.Application.Features.Orders.Dtos
{
    public class OrderRequestDto
    {
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}
