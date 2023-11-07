using LogPark.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogPark.BLL
{
    public  class LanguageService
    {
        public LanguageService languageRepository = new LanguageService();

        public void UpdatePrice(int Price)
        {
            languageRepository.UpdatePrice(Price);
           
        }
        public int GetPrice()
        {
           
           return languageRepository.GetPrice();
        }
    }

}
