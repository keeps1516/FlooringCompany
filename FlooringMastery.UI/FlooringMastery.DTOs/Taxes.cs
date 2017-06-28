using System;
namespace FlooringMastery.DTOs
{
    public class Taxes
    {
        public string StateNameAbreviated
        {
            get;
            set;
        }

        public string StateName
        {
            get;
            set;
        }

        public decimal TaxRate
        {
            get;
            set;
        }
    }
}
