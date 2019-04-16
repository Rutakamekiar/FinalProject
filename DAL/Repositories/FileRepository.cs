using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using DAL.Interfaces.RepositoryInterfaces;
using File = DAL.Entities.File;

namespace DAL.Repositories
{
    public sealed class FileRepository : IFileRepository
    {
        private readonly DbSet<File> _context;

        public FileRepository(DbContext context)
        {
            _context = context?.Set<File>()
                       ?? throw new ArgumentNullException($"Context must be not null!");
        }

        public void Create(File item)
        {
            _context.Add(item
                         ?? throw new ArgumentNullException("File must be not null!"));
        }

        public void Delete(int id)
        {
            var file = Get(id);
            _context.Remove(file);
        }

        public File Get(int id)
        {
            return _context.Find(id)
                   ?? throw new FileNotFoundException($"File with id = {id} was not found");
        }

        public IQueryable<File> GetAll()
        {
            return _context;
        }
    }
}
