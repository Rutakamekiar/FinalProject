using System;
using System.Collections.Generic;
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
    }
}
