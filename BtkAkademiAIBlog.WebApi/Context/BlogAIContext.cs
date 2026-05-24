using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BtkAkademiAIBlog.WebApi.Context
{
    public class BlogAIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-73DG7VF\\SQLEXPRESS; initial catalog=BtkAkademiAIBlogDb;integrated security=true;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
