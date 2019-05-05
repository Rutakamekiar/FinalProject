using AutoMapper;
using BLL.MappingProfiles;
using PL.MappingProfiles;

namespace PL.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.AddProfile<FileMapConfig>();
                config.AddProfile<FolderMapConfig>();
                config.AddProfile<FolderMappingProfile>();
                config.AddProfile<FileMappingProfile>();
            });
        }
    }
}