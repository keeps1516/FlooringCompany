using System;
using System.Collections.Generic;
using System.IO;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Interfaces;

namespace FlooringMastery.DataLayer
{
    public class ProductRepository : IProductRepository
    {
        private string _filepath;

        public ProductRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Products> List()
        {
            List<Products> products = new List<Products>(); 

            using (StreamReader sr = new StreamReader(_filepath))
            {
                string header = sr.ReadLine();
                string line;
                while((line = sr.ReadLine()) !=null)
                {
                    Products product = new Products();
                    var columns = line.Split(',');

                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    products.Add(product);
                }
            }

            return products;
        }
    }
}
