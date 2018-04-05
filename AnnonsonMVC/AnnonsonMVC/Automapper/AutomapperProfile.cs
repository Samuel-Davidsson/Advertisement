using AnnonsonMVC.ViewModels;
using AutoMapper;
using Domain.Entites;

namespace AnnonsonMVC.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Article, ArticelViewModel>().ReverseMap();
        }
    }
}
