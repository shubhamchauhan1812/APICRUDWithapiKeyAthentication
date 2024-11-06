using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<tblErrorDTO<string>> tblErrorDTO { get; set; }
        public DbSet<tbl_ApiKey> tbl_ApiKey { get; set; }
    }
}
