using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BtkAkademiAIBlog.WebApi.Context
{
    public class BlogAIContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-73DG7VF\\SQLEXPRESS; initial catalog=BtkAkademiAIBlogDb;integrated security=true;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TradingVideo> TradingVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
