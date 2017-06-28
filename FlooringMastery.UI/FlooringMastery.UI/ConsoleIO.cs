using System;
using System.Collections.Generic;
using FlooringMastery.DTOs;

namespace FlooringMastery.UI
{
    public static class ConsoleIO
    {
        public static void DisplayOrderListDetails(List<Order> orders)
        {
            foreach (var item in orders)
            {
                string datestring = item.Date.ToString("MM/dd/yyyy");
                Console.WriteLine($"Order Number: {item.OrderNumber}  Order Date: {datestring}");
                Console.WriteLine($"Ordered by {item.CustomerName}");
                Console.WriteLine($"Order State {item.State}");
                Console.WriteLine($"Product orderd {item.ProductType}");
                Console.WriteLine($"Cost of Materials {item.MaterialCost}");
                Console.WriteLine($"Cost of Labor {item.LaborCost}");
                Console.WriteLine($"Tax {item.Tax}");
                Console.WriteLine($"Toal {item.Total}");
            }
        }
    }
}
