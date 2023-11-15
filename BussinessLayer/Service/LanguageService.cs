using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public  class LanguageService
    {
       

        public void UpdatePrice(int Price)
        {
            try
            {
                LanguageRepository languageRepository = new LanguageRepository();
                languageRepository.UpdatePrice(Price);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        public int GetPrice()
        {
            LanguageRepository languageRepository = new LanguageRepository();
            return languageRepository.GetPrice();
        }
    }

}
