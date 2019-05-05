using AutoMapper;
using BLL.DTO;
using PL.Models;

namespace PL.MappingProfiles
{
    public class FileMappingProfile:Profile
    {
        public FileMappingProfile()
        {
            CreateMap<FileDTO, FileView>()
                .ForMember(file => file.Path,
                    opt => opt.MapFrom(f => f.Folder.Path + @"\" + f.Folder.Name));
        }
    }
}