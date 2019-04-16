using DAL.Interfaces;
using DAL.Interfaces.RepositoryInterfaces;
using System;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IFileRepository Files { get; }

        public UnitOfWork(DbContext context, IFileRepository files)
        {
            _context = context;
            Files = files;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
