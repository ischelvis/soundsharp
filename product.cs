namespace Opdracht
{
    public class Product
    {
        public int Id;
        public string Make;
        public string Model;
        public int MbSize;
        public double Price;
        public int Stock;

        public Product(int id, string make, string model, int mbSize, double price, int stock)
        {
            Id = id;
            Make = make;
            Model = model;
            MbSize = mbSize;
            Price = price;
            Stock = stock;
        }

        public Product()
        {
            
        }
    }
}