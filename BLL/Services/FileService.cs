using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public sealed class FileService : IFileService, IDisposable
    {
        private IUnitOfWork _data;
        public FileService(IUnitOfWork data)
        {
            _data = data;
        }
        
        public void Create(FileDTO item)
        {
            _data.Files.Create(Mapper.Map<File>(item));
            _data.Save();
        }

        public void Delete(int id)
        {
            _data.Files.Delete(id);
            _data.Save();
        }

        public void Dispose()
        {
            _data.Dispose();
        }

        public FileDTO Get(int id)
        {
            return Mapper.Map<FileDTO>(_data.Files.Get(id));
        }

        public HashSet<FileDTO> GetAll()
        {
            return Mapper.Map<HashSet<FileDTO>>(_data.Files.GetAll());
        }

        public List<FileDTO> GetAllByUserId(string userid)
        {
            return GetAll().Where(f => f.UserId.Equals(userid)).ToList();
        }

        public FileDTO GetByName(string name)
        {
            return GetAll().Where(f => f.Name.Contains(name)).OrderBy(f=>f.Name).FirstOrDefault();
        }
        public void EditFile(int id, FileDTO fileDto)
        {
            var newFile = _data.Files.Get(id);
            newFile.Name = fileDto.Name;
            newFile.AccessLevel = fileDto.AccessLevel;
            _data.Files.Update(newFile);
            _data.Save();
        }
    }
}
