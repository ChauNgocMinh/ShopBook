namespace BackEndWebShop.Model
{
    public class BookModel
    {
        public string Id { get; set; } = null!;

        public string? Namebook { get; set; }

        public string? Category { get; set; }

        public string? PublishingCompany { get; set; }

        public int? Price { get; set; }

        public int? Status { get; set; }
    }
}
