using System;
using FlooringMastery.Buisness;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Responses;
namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            bool repeat = true;
            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Remove an Order");
                Console.WriteLine("(----------------------------);");
                Console.WriteLine();
                Console.WriteLine("Enter the Date of the Order you wish to remove...");
                Console.WriteLine("Your date must be in this format 06/01/2013");
                Console.WriteLine("If you wish to quit enter Q");

                string stringdate = Console.ReadLine();
                if (stringdate == "Q")
                {
                    repeat = false;
                    break;
                }

                DateConvert DC = new DateConvert();
                DateConvertResponse dateResponse = DC.StringToDateConversion(stringdate);
                if (!dateResponse.Success)
                {
                    Console.WriteLine(dateResponse.Message);
                    Console.WriteLine("Press any key to continue... ");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                else
                {
                    OrderDisplayResponse response = manager.LookupOrder(dateResponse.date);
                    if (response.Success)
                    {
                        Console.WriteLine("Enter the Order number of the order you wish to remove");

                        string orderstring = Console.ReadLine();
                        int orderNumber = 0;
                        Order retrievedOrder = new Order();

                        if (!Int32.TryParse(orderstring, out orderNumber))
                        {
                            Console.WriteLine("You must enter a valid number");
                            Console.WriteLine("Press any key to continue... ");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }

                        foreach (var x in response.Orders)
                        {
                            if (orderNumber == x.OrderNumber)
                            {
                                retrievedOrder = x;
                            }
                        }
                        Console.Clear();
                        ConsoleOrderdetailsDisplay.OrderDetails(retrievedOrder);
                        Console.WriteLine("Are You sure you want to delete this order?");
                        Console.WriteLine("Enter Y for Yes or N for No");
                        string decision = Console.ReadLine();

                        if(decision=="Y")
                        {
                            manager.TotallyRemoveOrder(retrievedOrder);
                        }else
                        {
                            continue;
                        }

                    }
                }
            }
        }

    }
}
