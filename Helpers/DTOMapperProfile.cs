using AnuncioWeb.Models;
using AnuncioWeb.Models.DTO;
using AutoMapper;

namespace AnuncioWeb.Helpers
{
    public class DTOMapperProfile : Profile
    {
       public DTOMapperProfile()
        {
            CreateMap<Anuncio, AnuncioDTO>();
        }
    }
}
