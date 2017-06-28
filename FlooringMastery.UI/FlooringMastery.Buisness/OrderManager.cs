﻿using System;
using System.Collections.Generic;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Interfaces;
using FlooringMastery.DTOs.Responses;

namespace FlooringMastery.Buisness
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepo _taxrepository;
        private IProductRepository _productrepository;

        public OrderManager(IOrderRepository orderRespository, ITaxRepo taxrepository, IProductRepository productrepository)
        {
            _orderRepository = orderRespository;
            _taxrepository = taxrepository;
            _productrepository = productrepository;
        }

        public OrderDisplayResponse LookupOrder(DateTime date)
        {
            OrderDisplayResponse response = new OrderDisplayResponse();

            response.Orders = _orderRepository.LoadOrders(date);

			if (response.Orders == null)
			{
				response.Success = false;
				response.Message = $"{date} is not a valid order date.";
			}
			else
			{
				response.Success = true;
			}

			return response;
        }

        public OrderResponse CheckOrderForValidStateAndValidProduct(Order order)
        {
            OrderResponse response = new OrderResponse();
            var taxes = _taxrepository.List();
            var products = _productrepository.List();
            response.Success = false;



            foreach (var product in products)
            {
               if (order.ProductType == product.ProductType)
                {
                    response.Success = true;
                    order.CostPerSquareFoot = product.CostPerSquareFoot;
                    order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                    order.MaterialCost = order.Area * product.CostPerSquareFoot;
                    order.LaborCost = order.Area * product.LaborCostPerSquareFoot;
                    break;
                }
                else
                {

                    continue;
                }
            }
			foreach (var tax in taxes)
			{
			   if (order.State == tax.StateName || order.State == tax.StateNameAbreviated)
				{
					response.Success = true;
                    order.State = order.State;
                    order.TaxRate = tax.TaxRate;
                    order.Tax = (order.MaterialCost + order.LaborCost) * (tax.TaxRate / 100);
                    order.Total = (order.MaterialCost + order.LaborCost + order.Tax);
					break;
				}
				else
				{
                    response.Success = false;
					continue;
				}

			}
            response.order = order;
            return response;
        }

        public void SaveCheckedOrder(Order order)
        {
            List<Order> orders = new List<Order>();
            orders =_orderRepository.LoadOrders(order.Date);
            if(orders != null)
            {
				order.OrderNumber = orders.Count + 1;
				_orderRepository.SaveOrder(order);
            }else
            {
               order.OrderNumber += 1;
                _orderRepository.SaveOrder(order);
            }

        }

        public void SaveEdittedOrder(Order order)
        {
            _orderRepository.EditOrder(order);

        }

        public void TotallyRemoveOrder(Order order)
        {
            _orderRepository.RemoveOrder(order);
        }
    }
}
