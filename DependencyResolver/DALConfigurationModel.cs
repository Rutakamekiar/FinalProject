using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Web.Common;
using DAL.Entity_Framework;
using System.Data.Entity;
using Ninject.Modules;
using DAL.Interfaces.RepositoryInterfaces;

namespace DependencyResolver
{
    public sealed class DALConfigurationModel : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<DbContext>().To<Context>().InSingletonScope();
            Bind<IFileRepository>().To<FileRepository>().InSingletonScope();
            Bind<IFolderRepository>().To<FolderRepository>().InSingletonScope();
        }
    }
}
