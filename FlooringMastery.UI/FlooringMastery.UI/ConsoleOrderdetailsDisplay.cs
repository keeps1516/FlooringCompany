using System;
using FlooringMastery.DTOs;

namespace FlooringMastery.UI
{
    public class ConsoleOrderdetailsDisplay
    {
        public static void OrderDetails(Order retrievedOrder)
        {

			Console.WriteLine($"Order Number: {retrievedOrder.OrderNumber}");
			string datestring = retrievedOrder.Date.ToString("MM/dd/yyyy");
			Console.WriteLine($"Order Date: {datestring}");
			Console.WriteLine($"Ordered by {retrievedOrder.CustomerName}");
			Console.WriteLine($"Order State {retrievedOrder.State}");
			Console.WriteLine($"Product orderd {retrievedOrder.ProductType}");
            Console.WriteLine($"Area {retrievedOrder.Area}");
			Console.WriteLine($"Cost of Materials {retrievedOrder.MaterialCost}");
			Console.WriteLine($"Cost of Labor {retrievedOrder.LaborCost}");
			Console.WriteLine($"Tax {retrievedOrder.Tax}");
			Console.WriteLine($"Toal {retrievedOrder.Total}");
        }
    }
}
