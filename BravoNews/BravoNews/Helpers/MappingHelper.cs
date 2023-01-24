using AutoMapper;
using BravoNews.Models;
using BravoNews.Models.ViewModels;

namespace BravoNews.Helpers
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<AddArticleVM, Article>();
        }
    }
}
