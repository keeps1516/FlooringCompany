﻿using System;
using System.Collections.Generic;
using System.IO;
using FlooringMastery.Buisness;
using FlooringMastery.DataLayer;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Responses;
using NUnit.Framework;

namespace FlooringMastery.Testy
{
    [TestFixture]
    public class WoodTests
    {
        private const string _folderPath = @"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/";
        private const string _filePath = @"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/Orders_06012013.txt";
        private const string _originalData = @"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/SeedTestFolder/Orders_06012013.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }

            File.Copy(_originalData, _filePath);
        }

        [Test]

        public void CanReadDataFromFile()
        {
            FileRepository repo = new FileRepository(_folderPath);

            DateTime date = new DateTime(2013, 06, 01);
            ListOrderResponse response = repo.List(date);

            Assert.AreEqual(true, repo.List(date).Success);

        }

        [Test]

        public void CanGetOrderListFromFile()
        {
            FileRepository repo = new FileRepository(_folderPath);

            DateTime date = new DateTime(2013, 06, 01);

            var x = repo.LoadOrders(date);

            Assert.AreEqual(1, repo.LoadOrders(date).Count);

        }


        [Test]
        public void CanCreateNewFileandLoad()
        {
            FileRepository repo = new FileRepository(_folderPath);
            DateTime date = new DateTime(2014, 06, 01);
            Order order = new Order();

            order.Date = date;
            order.OrderNumber = 29;
            order.CustomerName = "Rick James";
            order.State = "Texas";
            order.TaxRate = .01M;
            order.ProductType = "wood";
            order.Area = 100;
            order.CostPerSquareFoot = 1;
            order.LaborCostPerSquareFoot = 2;
            order.MaterialCost = 3;
            order.LaborCost = 4;
            order.Tax = 5;
            order.Total = 6;

            repo.SaveOrder(order);
            var x = repo.LoadOrders(date);

            Assert.AreEqual(1, x.Count);
        }


        [Test]
        public void CanReadTaxFile()
        {
            TaxFileRepository repo = new TaxFileRepository("/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/Taxes.txt");
            List<Taxes> tax = new List<Taxes>();
            tax = repo.List();

            Assert.AreEqual(4, tax.Count);
        }


        [Test]
        public void CanDeleteOrderFromFile()
        {
            FileRepository repo = new FileRepository("/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/");

            Order order = new Order();
            DateTime date = new DateTime(2014, 06, 01);
            order.Date = date;
            order.OrderNumber = 4;
            repo.RemoveOrder(order);

            Assert.Pass();
        }
    }
}
