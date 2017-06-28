using System;
namespace FlooringMastery.DTOs.Responses
{
    public class OrderResponse : Response
    {
        public Order order
        {
            get;
            set;
        }
    }
}
