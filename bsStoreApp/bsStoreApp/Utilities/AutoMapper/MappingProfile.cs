using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace bsStoreApp.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BookDtoUpdate, Libs>();
            CreateMap<Libs,BookDto>();
            CreateMap<BookDtoInsertion,Libs>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
