using BLL.DTO;
using PL.Models;
using  AutoMapper;
namespace PL.MappingProfiles
{
    public class FolderMappingProfile : Profile
    {
        public FolderMappingProfile()
        {
            CreateMap<FolderDTO, FolderView>();
        }
    }
}