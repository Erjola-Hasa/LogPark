using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public  class PriceService
    {
        PriceRepository languageRepository = new PriceRepository();

        public void UpdatePrice(int Price)
        {
            try
            {
             
                languageRepository.UpdatePrice(Price);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        public int GetPrice()
        {
            
            return languageRepository.GetPrice();
        }
    }

}
