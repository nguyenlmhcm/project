using System.Data.Entity;
using System.Data.SqlClient;

namespace DoAn1.Models
{
    public class CSDL : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<BestSeller> BestSeller { get; set; }
        public DbSet<Comming> Comming { get; set; }
        public DbSet<Winner> Winner { get; set; }
        public DbSet<WhatsNew> WhatsNew { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<NewRelease> NewRelease { get; set; }
        public DbSet<History> History { get; set; }
        
        public CSDL()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-GNVB183",
                InitialCatalog = "doan1",
                IntegratedSecurity = true
            };

            this.Database.Connection.ConnectionString = builder.ConnectionString;
        }
    } 
}  