namespace BackEndWebShop.Model
{
    public class CartBookModel
    {
        public string Id { get; set; } = null!;
        public string? Namebook { get; set; }
        public string? Category { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Title => Quantity * Price;

    }
 
}
