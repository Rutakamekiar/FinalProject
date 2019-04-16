using AutoMapper;
using BLL.MappingProfiles;

namespace PL.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.AddProfile<FileMapConfig>();
            });
        }
    }
}