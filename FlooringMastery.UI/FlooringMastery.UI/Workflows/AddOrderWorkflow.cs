using System;
using FlooringMastery.Buisness;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();
            bool repeat = false;

            while (!repeat)
            {
				Console.Clear();
				Console.WriteLine("Add an Order");
                Console.WriteLine("--------------------------------------------");
				Console.WriteLine("Enter a future date for your order");
                Console.WriteLine("Your date must match this format MM/DD/YYYY");
                Console.WriteLine("Press Q to go back to the main menu");
				Console.WriteLine("--------------------------------------------");
				string date = Console.ReadLine();
                if(date == "Q")
                {
                    repeat = true;
                }
                DateConvert conversion = new DateConvert();
                var convertedDate = conversion.StringToDateConversion(date);
                if (!convertedDate.Success)
                {
                    Console.WriteLine(convertedDate.Message);
                    continue;
                }

                if (convertedDate.date < DateTime.Now)
                {
                    Console.WriteLine("Your date entry must be in the future");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }


				Console.WriteLine("Enter your Customers Name");
				string custName = Console.ReadLine();
                if(custName== null|| custName == "")
                {
                    Console.WriteLine("You have to enter a name for your customer.");
                    Console.WriteLine("They are people too :D");
					Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

				Console.WriteLine("Enter the Product's installed area");
				string area = Console.ReadLine();
                decimal result = 0;
                if (!decimal.TryParse(area, out result))
                {
                    Console.WriteLine("You must input a number in the valid format");
                    Console.WriteLine("Example: 101.00");
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					continue;

                }
                if (result< 100)
                {
                    Console.WriteLine("You must enter a numbner greater than 100");
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					continue;
                }
                decimal decimalarea = decimal.Parse(area);

				Console.WriteLine("Enter your Order's state");
				string state = Console.ReadLine();

				Console.WriteLine("Enter the Product Type");
				string productType = Console.ReadLine();

				order.Date = convertedDate.date;
				order.CustomerName = custName;
				order.Area = decimalarea;
                order.State = state;
                order.ProductType = productType;
                var response = manager.CheckOrderForValidStateAndValidProduct(order);

                if (!response.Success)
                {
                    Console.WriteLine("Your orders state and product must match our system's records");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }else
                {
                    Console.WriteLine($"Confirm your Order:");
                    Console.WriteLine($"Order Date: {response.order.Date}");
                    Console.WriteLine($"Customer Name: {response.order.CustomerName}");
                    Console.WriteLine($"State: {response.order.State}");
                    Console.WriteLine($"Product Type: {response.order.ProductType}");
                    Console.WriteLine($"Area: {response.order.Area}");
                    Console.WriteLine($"Cost of Materials: {response.order.MaterialCost}");
                    Console.WriteLine($"Cost of Labor: {response.order.LaborCost}");
                    Console.WriteLine($"Tax: {response.order.Tax} ");
                    Console.WriteLine($"Total: {response.order.Total}");
                    Console.WriteLine("Would you like to save your order?");
                    Console.WriteLine("Press Y for yes and N for no");
                    string input = Console.ReadLine();

                    if(input == "Y")
                    {
                        manager.SaveCheckedOrder(response.order);
                        repeat = false;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
