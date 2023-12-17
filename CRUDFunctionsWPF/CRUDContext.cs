using Microsoft.EntityFrameworkCore;

namespace CRUDFunctionsWPF
{
    public class CRUDContext : DbContext
    {
        private readonly string _connectionString;

        // Constructor that accepts a connection string.
        public CRUDContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // DbSet representing your Person entity
        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Check if DBcontext hasn't already been configured to avoid multiple configs.
            if (!optionsBuilder.IsConfigured)
            {
                //Using connection string for SQL server
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

    }
}
