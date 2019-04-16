using System;
using DAL.Interfaces.RepositoryInterfaces;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository Files { get; }
        void Save();
    }
}