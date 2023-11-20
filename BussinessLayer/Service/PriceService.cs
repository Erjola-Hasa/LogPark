﻿using DataAccesLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace BusinessLayer
{
    public  class PriceService
    {
        /// <summary>
        ///  Initializes a new instance of the PriceRepository class 
        /// </summary>
        PriceRepository PriceRepository = new PriceRepository();




        /// <summary>
        /// Updates the Price record in the database with the provided Price
        /// </summary>
        /// <param name="Price"></param>
        public void UpdatePrice(int Price)
        {
            try
            {

                PriceRepository.UpdatePrice(Price);
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.log", ex.ToString());
                MessageBox.Show("An error has occurred.");

            }
           
        }





        /// <summary>
        /// Retrive the Price  from datbase 
        /// </summary>
        /// <returns></returns>
        public int GetPrice()
        {
            
            return PriceRepository.GetPrice();
        }
    }

}
