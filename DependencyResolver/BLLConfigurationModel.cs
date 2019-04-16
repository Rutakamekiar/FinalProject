using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace DependencyResolverConfig
{
    public sealed class BLLConfigurationModel : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileService>().To<FileService>();
        }
    }
}