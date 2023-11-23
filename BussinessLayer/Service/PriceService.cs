using DataAccesLayer;


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
           

                PriceRepository.UpdatePrice(Price);
            
           
           
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
