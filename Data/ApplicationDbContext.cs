 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Adornian.Models;

namespace Adornian.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Adornian.Models.Jewellery> Jewellery { get; set; }
        public DbSet<Adornian.Models.Category> Category { get; set; }
    }
}