using System;
using System.Collections.Generic;
using System.IO;
using FlooringMastery.DTOs;
using FlooringMastery.DTOs.Interfaces;

namespace FlooringMastery.DataLayer
{
    public class TaxFileRepository : ITaxRepo
    {
        private string _filepath;

        public TaxFileRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Taxes> List()
        {
            List<Taxes> taxes = new List<Taxes>();

            using (StreamReader sr = new StreamReader(_filepath))
            {
                string header = sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) !=null)
                {
                    Taxes tax = new Taxes();
                    var columns = line.Split(',');

                    tax.StateNameAbreviated = columns[0];
                    tax.StateName = columns[1];
                    tax.TaxRate = decimal.Parse(columns[2]);

                    taxes.Add(tax);
                }

            }

            return taxes;
        }
    }
}
