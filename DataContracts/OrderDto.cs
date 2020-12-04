using System;

namespace DataContracts
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
