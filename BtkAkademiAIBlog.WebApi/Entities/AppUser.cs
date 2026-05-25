using Microsoft.AspNetCore.Identity;

namespace BtkAkademiAIBlog.WebApi.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Article> Articles { get; set; }
    }
}
