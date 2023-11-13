using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.DTO;

namespace BusinessObject.Mapper;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<Folder, FolderDTO>().ReverseMap();
    }
}