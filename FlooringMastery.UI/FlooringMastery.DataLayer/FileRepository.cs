using System;
using System.Collections.Generic;
using System.IO;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace FlooringMastery.DataLayer
{
    public class FileRepository : IOrderRepository
    {
        private string _filePath;

        public FileRepository(string filepath)
        {
            _filePath = filepath;
        }

        public ListOrderResponse List(DateTime date)
        {
            ListOrderResponse response = new ListOrderResponse();
            response.Success = true;
            response.Orders = new List<Order>();

            string datestring = date.ToString("MMddyyyy");
            string datefilepath = $"Orders_{datestring}.txt";

            try
            {
               //using (StreamReader sr = new StreamReader(_filePath + datefilepath))
                using (TextFieldParser parser = new TextFieldParser(new StreamReader(_filePath+datefilepath)))
                {
                    parser.SetDelimiters(",");
                    parser.HasFieldsEnclosedInQuotes = true;
                    string header = parser.ReadLine();
                    string line;
                    while ((line = parser.ReadLine()) != null)
                    {

                        Order order = new Order();
                        var columns = line.Split(',');

                        order.Date = date;
                        order.OrderNumber = Int32.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.MaterialCost = decimal.Parse(columns[8]);
                        order.LaborCost = decimal.Parse(columns[9]);
                        order.Tax = decimal.Parse(columns[10]);
                        order.Total = decimal.Parse(columns[11]);

                        response.Orders.Add(order);
                    }

                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occured loading the order list:" + ex.Message;
            }
            return response;
        }

        public List<Order> LoadOrders(DateTime date)
        {
            var response = List(date);

            if (!response.Success)
            {
                return null;
            }
            else
            {
                return response.Orders;
            }
        }
        public void SaveOrder(Order order)
        {
            string datestring = order.Date.ToString("MMddyyyy");
            string datefilepath = $"Orders_{datestring}.txt";

            if (!File.Exists(_filePath + datefilepath))
            {
                using (StreamWriter sw = File.CreateText(_filePath + datefilepath))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                    sw.WriteLine($"{order.OrderNumber},\"{order.CustomerName}\",{order.State},{order.TaxRate.ToString()},{order.ProductType},{order.Area.ToString()},{order.CostPerSquareFoot.ToString()},{order.LaborCostPerSquareFoot.ToString()},{order.MaterialCost.ToString()},{order.LaborCost.ToString()},{order.Tax.ToString()},{order.Total.ToString()}");

                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_filePath + datefilepath))
                {
                    sw.WriteLine($"{order.OrderNumber},\"{order.CustomerName}\",{order.State},{order.TaxRate.ToString()},{order.ProductType},{order.Area.ToString()},{order.CostPerSquareFoot.ToString()},{order.LaborCostPerSquareFoot.ToString()},{order.MaterialCost.ToString()},{order.LaborCost.ToString()},{order.Tax.ToString()},{order.Total.ToString()}");

                }
            }
        }

		public void EditOrder(Order order)
		{
			string datestring = order.Date.ToString("MMddyyyy");
			string datefilepath = $"Orders_{datestring}.txt";

			var index = 0;
			Order savedOrder = new Order();
			var orders = LoadOrders(order.Date);

			foreach (var item in orders)
			{
				if (item.OrderNumber == order.OrderNumber)
				{
					index = orders.IndexOf(item);
					break;
				}
			}

			orders[index] = order;

			if (File.Exists(_filePath + datefilepath))
			{
				using (StreamWriter sw = File.CreateText(_filePath + datefilepath))
				{
					sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

					foreach (var x in orders)
					{
						sw.WriteLine($"{x.OrderNumber},{x.CustomerName},{x.State},{x.TaxRate.ToString()},{x.ProductType},{x.Area.ToString()},{x.CostPerSquareFoot.ToString()},{x.LaborCostPerSquareFoot.ToString()},{x.MaterialCost.ToString()},{x.LaborCost.ToString()},{x.Tax.ToString()},{x.Total.ToString()}");

					}

				}
			}
            //else
            //{
                
            //}
		}
        public void RemoveOrder(Order order)
        {
			string datestring = order.Date.ToString("MMddyyyy");
			string datefilepath = $"Orders_{datestring}.txt";

			var index = 0;
			var orders = LoadOrders(order.Date);

			foreach (var item in orders)
			{
				if (item.OrderNumber == order.OrderNumber)
				{
					index = orders.IndexOf(item);
					break;
				}
			}

			orders.RemoveAt(index);
			if (File.Exists(_filePath + datefilepath))
			{
				using (StreamWriter sw = File.CreateText(_filePath + datefilepath))
				{
					sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

					foreach (var x in orders)
					{

                        sw.WriteLine($"{x.OrderNumber},\"{x.CustomerName}\",{x.State},{x.TaxRate.ToString()},{x.ProductType},{x.Area.ToString()},{x.CostPerSquareFoot.ToString()},{x.LaborCostPerSquareFoot.ToString()},{x.MaterialCost.ToString()},{x.LaborCost.ToString()},{x.Tax.ToString()},{x.Total.ToString()}");

					}

				}
			}
        }

      }
    }