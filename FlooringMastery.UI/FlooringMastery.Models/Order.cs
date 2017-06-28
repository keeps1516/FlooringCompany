using System;
namespace FlooringMastery.Models
{
    public class Order
    {
        public DateTime date
        {
            get;
            set;
        }

        public int OrderNumber
        {
            get;
            set;
        }


        public string CustomerName
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public decimal TaxRate
        {
            get;
            set;
        }

        public string ProductType
        {
            get;
            set;
        }

        public decimal Area
        {
            get;
            set;
        }

        public decimal CostPerSquareFoot
        {
            get;
            set;
        }

        public decimal LaborCostPerSquareFoot
        {
            get;
            set;
        }

        public decimal MaterialCost
        {
            get;
            set;
        }

        public decimal LaborCost
        {
            get;
            set;
        }

        public decimal Tax
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }
    }
}

