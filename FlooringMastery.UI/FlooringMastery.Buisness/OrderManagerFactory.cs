﻿using System;
using System.Configuration;
using FlooringMastery.Buisness;
using FlooringMastery.DataLayer;

namespace FlooringMastery.Buisness
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestFile":
                    return new OrderManager(new FileRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/"),
                                            new TaxFileRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/Taxes.txt"),
                                            new ProductRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/TestFile/Products.txt"));
                case "Production":
					return new OrderManager(new FileRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/ProdFile/"),
											new TaxFileRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/ProdFile/Taxes.txt"),
											new ProductRepository(@"/Users/kriskeepers/Documents/Bitbucket/kris-keepers-indivdual-work/FlooringMastery.UI/SampleData/ProdFile/Products.txt"));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
