using System;
using System.Collections.Generic;

namespace FlooringMastery.DTOs.Responses
{
    public class OrderDisplayResponse : Response
    {
        public List<Order> Orders { get; set; }
    }
}
