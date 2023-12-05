using DataAccesLayer;


namespace BusinessLayer
{
    public  class PriceService
    {

        /// <summary>
        /// Define a private  _priceRepository variable
        /// </summary>
        private readonly PriceRepository _priceRepository;

        /// <summary>
        ///  Initializes a new instance of the PriceRepository  
        /// </summary>
        

        public PriceService()
        {
                _priceRepository = new PriceRepository();
        }




        /// <summary>
        /// Updates the Price record in the database with the provided Price
        /// </summary>
        /// <param name="Price"></param>
        public void UpdatePrice(int Price)
        {
           

                _priceRepository.UpdatePrice(Price);
            
           
           
        }





        /// <summary>
        /// Retrive the Price  from datbase 
        /// </summary>
        /// <returns></returns>
        public int GetPrice()
        {
            
            return _priceRepository.GetPrice();
        }
    }

}
