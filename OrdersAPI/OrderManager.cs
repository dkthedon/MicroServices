using DataContracts;
using System.Collections.Generic;

namespace OrdersAPI
{
    public interface OrderManager
    {
        IEnumerable<OrderDto> GetOrders(int userId);
        OrderDto GetOrder(int userId, int orderId);
        bool SaveOrder(int userId, OrderDto order);
        bool DeleteOrder(int userId, int orderId);
        bool DeleteOrders(int userId);
    }
}
