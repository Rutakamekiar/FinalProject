using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.MappingProfiles
{
    public sealed class FileMapConfig : Profile
    {
        public FileMapConfig()
        {
            CreateMap<FileDTO, File>();
            CreateMap<File, FileDTO>();
        }
    }
}
