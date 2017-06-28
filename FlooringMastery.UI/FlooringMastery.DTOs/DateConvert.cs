using System;
using System.Globalization;
using FlooringMastery.DTOs.Responses;

namespace FlooringMastery.DTOs
{
    public class DateConvert
    {
        public DateConvertResponse StringToDateConversion(string date)
        {
            DateConvertResponse response = new DateConvertResponse();
            response.Success = true;
            try
            {
				
				string dateformat = "MM/dd/yyyy";
				response.date = DateTime.ParseExact(date, dateformat, null);
				return response;
            }
            catch(Exception)
            {
                response.Success = false;
                response.Message = "An error occured. Please enter the dat in the correct format:";
            }

            return response;
        }

	}
}
