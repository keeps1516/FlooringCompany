using System;
using System.Collections.Generic;

namespace FlooringMastery.DTOs.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadOrders(DateTime date);
        void SaveOrder(Order order);
        void EditOrder(Order order);
        void RemoveOrder(Order order);
    }
}

