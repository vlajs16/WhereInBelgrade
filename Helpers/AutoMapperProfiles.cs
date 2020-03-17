using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataTransferObjects;
using Model;

namespace Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Dogadjaj, DogadjajZaListuDTO>()
                //.ForMember(dest => dest.DogadjajID, opt => opt.MapFrom(p => p.DogadjajID));
                .ForMember(dest => dest.ZaKolikoDana,
                opt => opt.MapFrom(p => p.DatumPocetka.CalculateDays()))
                .ForMember(dest => dest.MainKategorija,
                opt => opt.MapFrom(p => p.KategorijeDogadjaji.FirstOrDefault().Kategorija));
            CreateMap<Dogadjaj, DetaljniDogadjajDTO>().
                ForMember(dest => dest.Kategorije,
                opt => opt.MapFrom(p => p.KategorijeDogadjaji.ConvertToKategorije()))
                .ForMember(dest => dest.MainKategorija,
                opt => opt.MapFrom(p => p.KategorijeDogadjaji.FirstOrDefault().Kategorija));
            CreateMap<Kategorija, KategorijaZaDogadjajDTO>();
        }
    }
}
