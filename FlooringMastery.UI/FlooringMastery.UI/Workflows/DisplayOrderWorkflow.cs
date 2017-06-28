using System;
using FlooringMastery.Buisness;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display an Order");
            Console.WriteLine("----------------------");

            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Enter an the date of the orders you wish to display:");
                Console.WriteLine("Your date must be in this format 06/01/2013");
                Console.WriteLine();
                Console.WriteLine("If you wish to quit enter Q");

                string stringdate = Console.ReadLine();
                if(stringdate == "Q")
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
                        ConsoleIO.DisplayOrderListDetails(response.Orders);
                        Console.WriteLine();
                        Console.ReadKey();
                        Console.Clear();
                        continue;

                    }
                    else
                    {
                        Console.WriteLine("An error occured");
                        Console.WriteLine(response.Message);
                        Console.WriteLine("Press any key to continue... ");
                        Console.ReadKey();
                        Console.Clear();
                        continue;

                    }
                }
            }
        }
    }
}
