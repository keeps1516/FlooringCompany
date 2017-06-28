using System;
using FlooringMastery.Buisness;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();

            bool repeat = true;
            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Edit an Order");
                Console.WriteLine("----------------------------");
                Console.WriteLine();
                Console.WriteLine("Enter the Date of the Order you wish to edit...");
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
                        Console.WriteLine("Enter the Order number of the order you wish to edit");

                        string orderstring= Console.ReadLine();
                        int orderNumber=0;
                        Order retrievedOrder = new Order();

                        if(!Int32.TryParse(orderstring, out orderNumber))
                        {
							Console.WriteLine("You must enter a valid number");
                            Console.WriteLine("Press any key to continue... ");
                            Console.ReadKey();
							Console.Clear();
							continue;
						}

                        foreach (var x in response.Orders)
                        {
                            if(orderNumber == x.OrderNumber)
                            {
                                retrievedOrder = x;
                            }
                        }


                
                        bool stuff = false;
                        while (!stuff)
                        {
                            var orderResponse1 = manager.CheckOrderForValidStateAndValidProduct(retrievedOrder);
                            ConsoleOrderdetailsDisplay.OrderDetails(orderResponse1.order);
                            Console.WriteLine("------------------------------------------");
							Console.WriteLine("What details would you like to edit?");
							Console.WriteLine("Customer Name? Enter: Cust");
							Console.WriteLine("State? Enter: State");
							Console.WriteLine("Product Type? Enter: Prod");
							Console.WriteLine("Area? Enter: Area");
                            Console.WriteLine("Enter: Q to Quit and discard changes");
                            Console.WriteLine("------------------------------------------");
                            Console.WriteLine("If you would like to Save your changes, Enter: Save");

							string userinput = Console.ReadLine();
                            decimal areanumber = 0;
                            switch(userinput)
                            {
                                case "Cust":
                                    retrievedOrder.CustomerName = Console.ReadLine();
                                    break;
                                case "State":
                                    retrievedOrder.State = Console.ReadLine();
                                    break;
                                case "Prod":
                                    retrievedOrder.ProductType = Console.ReadLine();
                                    break;
                                case "Area":
                                    if(!decimal.TryParse(Console.ReadLine(), out areanumber))
                                    {
                                        Console.WriteLine("You must enter a valid decimal number");
                                    }
                                    retrievedOrder.Area=areanumber;
                                    break;
                                case "Save":
                                    ConsoleOrderdetailsDisplay.OrderDetails(orderResponse1.order);
                                    Console.WriteLine("------------------------------------------");
                                    Console.WriteLine("Do you REALLY want to save thes changes?");
                                    Console.WriteLine("Enter Y for Yes or N for No");
                                    string decision = Console.ReadLine();
                                    if(decision == "Y")
                                    {
										var orderResponse2 = manager.CheckOrderForValidStateAndValidProduct(retrievedOrder);
                                        if (!orderResponse2.Success)
                                        {
                                            Console.WriteLine("Your orders state and product must match our system's records");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            continue;
                                        }
                                        else
                                        {
                                            manager.SaveEdittedOrder(orderResponse2.order);
                                        }
                                        stuff = true;
                                        break;
                                    }
                                    stuff = true;
                                    break;
                                case "Q":
                                    stuff = true;
                                    return;

                            }
                        }

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