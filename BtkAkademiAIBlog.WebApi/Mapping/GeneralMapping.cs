using AutoMapper;
using BtkAkademiAIBlog.WebApi.Dtos.ArticleDtos;
using BtkAkademiAIBlog.WebApi.Entities;

namespace BtkAkademiAIBlog.WebApi.Mapping
{ 
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Article,ResultArticleWithCategoryDto>()
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category.CategoryName));

            CreateMap<Article,CreateArticleDto>().ReverseMap();
            CreateMap<Article,UpdateArticleDto>().ReverseMap();
            CreateMap<Article,GetArticleByIdDto>().ReverseMap();
        }
    }
}
