using System;
using System.Collections.Generic;

namespace FlooringMastery.DTOs
{
    public class ListOrderResponse
    {
        public List<Order> Orders { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
