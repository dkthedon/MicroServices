using DataContracts;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace OrdersAPI
{
    public class InMemoryOrderManager : OrderManager
    {
        private ConcurrentDictionary<int, List<OrderDto>> orders;
        public InMemoryOrderManager()
        {
            orders = new ConcurrentDictionary<int, List<OrderDto>>();
        }

        public bool DeleteOrder(int userId, int orderId)
        {
            bool success = false;
            if (orders.ContainsKey(userId))
            {
                OrderDto order = orders[userId].Find(x => x.OrderId == orderId);
                if (order != null)
                {
                    orders[userId].Remove(order);
                    success = true;
                }
            }

            return success;
        }

        public bool DeleteOrders(int userId)
        {
            bool success = false;
            if (orders.ContainsKey(userId))
            {
                orders.Remove(userId, out var deletedOrders);
                success = true;
            }
            return success;
        }

        public OrderDto GetOrder(int userId, int orderId)
        {
            OrderDto order = null;
            if (orders.ContainsKey(userId))
            {
                order = orders[userId].Find(x => x.OrderId == orderId);
            }

            return order;
        }

        public IEnumerable<OrderDto> GetOrders(int userId)
        {
            return orders.GetValueOrDefault(userId);
        }

        public bool SaveOrder(int userId, OrderDto order)
        {
            bool success = false;
            if (!orders.ContainsKey(userId))
            {
                orders.TryAdd(userId, new List<OrderDto>());
            }

            if (order.OrderId == 0)
                order.OrderId = orders[userId].Count + 1;

            if (!orders[userId].Any(x => x.OrderId == order.OrderId))
            {
                orders[userId].Add(order);
                success = true;
            }

            return success;
        }
    }
}
