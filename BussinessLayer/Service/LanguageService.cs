using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLayer
{
    public  class LanguageService
    {
       

        public void UpdatePrice(int Price)
        {
            LanguageRepository languageRepository = new LanguageRepository();
            languageRepository.UpdatePrice(Price);
           
        }
        public int GetPrice()
        {
            LanguageRepository languageRepository = new LanguageRepository();
            return languageRepository.GetPrice();
        }
    }

}
