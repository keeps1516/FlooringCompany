using System;
namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(string number);
        void SaveOrder(Order order);
    }
}
