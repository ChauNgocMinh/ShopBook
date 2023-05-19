namespace BackEndWebShop.Model
{
    public class BillModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string IdCartItem { get; set; } = null!;

        public DateTime BuyingDate { get; set; }
    }
}
