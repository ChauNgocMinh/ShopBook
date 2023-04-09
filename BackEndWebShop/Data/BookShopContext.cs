
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEndWebShop.Data
{
    public partial class BookShopContext : IdentityDbContext<ApplicationUser>
    {
        public BookShopContext()
        {
        }

        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=LAPTOP-V6B0S12C\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True;User ID=sa;Password=123;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("BOOK");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID");
                entity.Property(e => e.Category)
                    .HasMaxLength(15)
                    .HasColumnName("CATEGORY");
                entity.Property(e => e.Namebook)
                    .HasMaxLength(50)
                    .HasColumnName("NAMEBOOK");
                entity.Property(e => e.Price).HasColumnName("PRICE");
                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
