using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public  class PriceService
    {
        /// <summary>
        /// Call the PriceRepository
        /// </summary>
        PriceRepository PriceRepository = new PriceRepository();




        /// <summary>
        /// Service Method for UpdatePrice 
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
                MessageBox.Show(ex.ToString());
            }
           
        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetPrice()
        {
            
            return PriceRepository.GetPrice();
        }
    }

}
