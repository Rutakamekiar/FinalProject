using System;
using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces.RepositoryInterfaces;

namespace DAL.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly DbContext _context;
        public FolderRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Context must be not null!");
        }
        public void Create(Folder item)
        {
            _context.Set<Folder>().Add(item
                                     ?? throw new ArgumentNullException("Folder must be not null!"));
        }

        public void Delete(int id)
        {
            var file = Get(id);
            _context.Set<Folder>().Remove(file);
        }
        public void Update(Folder file)
        {
            _context.Entry(file).State = EntityState.Modified;
        }
        public Folder Get(int id)
        {
            return _context.Set<Folder>().Find(id)
                   ?? throw new FolderNotFoundException($"Folder with id = {id} was not found");
        }

        public IQueryable<Folder> GetAll()
        {
            return _context.Set<Folder>();
        }
    }
}
