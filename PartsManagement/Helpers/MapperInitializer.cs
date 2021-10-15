using AutoMapper;
using PartsManagement.Dtos;
using PartsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsManagement.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Furnitori, FurnitoriDTO>().ReverseMap();
            CreateMap<Furnitori, CreateFurnitoriDTO>().ReverseMap();
            CreateMap<Furnitori, UpdateFurnitoriDTO>().ReverseMap();
            CreateMap<Komenti, KomentiDTO>().ReverseMap();
            CreateMap<Komenti, CreateKomentiDTO>().ReverseMap();
            CreateMap<Komenti, UpdateKomentiDTO>().ReverseMap();
            CreateMap<Marka, MarkaDTO>().ReverseMap();
            CreateMap<Marka, CreateMarkaDTO>().ReverseMap();
            CreateMap<Marka, UpdateMarkaDTO>().ReverseMap();
            CreateMap<Modeli, ModeliDTO>().ReverseMap();
            CreateMap<Modeli, CreateModeliDTO>().ReverseMap();
            CreateMap<Modeli, UpdateMarkaDTO>().ReverseMap();
            CreateMap<Porosia, PorosiaDTO>().ReverseMap();
            CreateMap<Porosia, CreatePorosiaDTO>().ReverseMap();
            CreateMap<Produkti, CreateProduktiDTO>().ReverseMap();
            CreateMap<Produkti, UpdateProduktiDTO>().ReverseMap();
            CreateMap<Produkti, ProduktiDTO>().ReverseMap();
            CreateMap<Porosia, UpdatePorosiaDTO>().ReverseMap();
            CreateMap<FaturaIN, CreateFaturaINDTO>().ReverseMap();
            CreateMap<FaturaIN, FaturaINDTO>().ReverseMap();
            CreateMap<FaturaIN, UpdateFaturaINDTO>().ReverseMap();
            CreateMap<Sektori, SektoriDTO>().ReverseMap();
            CreateMap<Sektori, CreateSektoriDTO>().ReverseMap();
            CreateMap<Sektori, UpdateSektoriDTO>().ReverseMap();
            CreateMap<FaturaOUT, FaturaOUTDTO>().ReverseMap();
            CreateMap<FaturaOUT, CreateFaturaOUTDTO>().ReverseMap();
            CreateMap<FaturaOUT, UpdateFaturaOUTDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Contact, UpdateContactDTO>().ReverseMap();
            CreateMap<Contact, CreateContactDTO>().ReverseMap();
        }
    }
}