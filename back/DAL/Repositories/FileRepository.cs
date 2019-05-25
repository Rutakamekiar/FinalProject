using System;
using System.Data.Entity;
using System.Linq;
using DAL.Entity_Framework;
using DAL.Interfaces.RepositoryInterfaces;
using File = DAL.Entities.File;

namespace DAL.Repositories
{
    public sealed class FileRepository : IFileRepository
    {
        private readonly DbContext _context;

        public FileRepository(DbContext context)
        {
            _context = context?? throw new ArgumentNullException($"Context must be not null!");
        }

        public void Create(File item)
        {
            _context.Set<File>().Add(item
                         ?? throw new ArgumentNullException("File must be not null!"));
        }

        public void Delete(int id)
        {
            var file = Get(id);
            _context.Set<File>().Remove(file);
        }
        public void Update(File file)
        {
            _context.Entry(file).State = EntityState.Modified;
        }
        public File Get(int id)
        {
            return _context.Set<File>().Find(id)
                   ?? throw new FileNotFoundException($"File with id = {id} was not found");
        }

        public IQueryable<File> GetAll()
        {
            return _context.Set<File>();
        }
    }
}
