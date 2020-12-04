using System.Collections.Generic;

namespace DataContracts
{
    public class OrderDetailsDto
    {
        public UserDto User { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
