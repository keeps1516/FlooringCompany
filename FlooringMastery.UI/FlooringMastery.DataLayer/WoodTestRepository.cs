﻿using System;
using FlooringMastery.DTOs.Interfaces;
using FlooringMastery.DTOs;
using System.Collections.Generic;

namespace FlooringMastery.DataLayer
{
    public class WoodTestRepository : IOrderRepository
    {
        private static Order _order = new Order()
        {
            Date = DateTime.Now,
            OrderNumber = 1,
            CustomerName = "Wise",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            MaterialCost = 515,
            LaborCost = 475,
            Tax = 61.88M,
            Total = 1051.88M

        };

        public List<Order> LoadOrders(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
