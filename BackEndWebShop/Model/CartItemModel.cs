namespace BackEndWebShop.Model
{
    public class CartItemModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? IdBook { get; set; }
        public int? Number { get; set; } = null!;

        public double? TotalItem { get; set; }

        public bool? Status { get; set; }

        public DateTime Date { get; set; }
    }
}
