using System;
using FlooringMastery.UI.Workflows;

namespace FlooringMastery.UI
{
    public class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");

                Console.WriteLine("\nQ to quit");
                Console.Write("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        DisplayOrderWorkflow workflow = new DisplayOrderWorkflow();
                        workflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addworkflow = new AddOrderWorkflow();
                        addworkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editworkflow = new EditOrderWorkflow();
                        editworkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeworkflow = new RemoveOrderWorkflow();
                        removeworkflow.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}