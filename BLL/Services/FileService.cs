using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using File = DAL.Entities.File;
using PhysicalFile = System.IO.File;

namespace BLL.Services
{
    public sealed class FileService : IFileService, IDisposable
    {
        private readonly IUnitOfWork _data;
        private const string RootFilePath = @"C:\Users\Vlad\Desktop\Angular\Content\";

        public FileService(IUnitOfWork data)
        {
           _data = data;
        }
        //Ok
        public void Create(FileDTO item)
        {

            _data.Files.Create(Mapper.Map<File>(item));
            CreatePhysicalFile();
            _data.Save();

            void CreatePhysicalFile()
            {
                PhysicalFile.WriteAllBytes(ReturnFullPath(item), item.FileBytes);
            }
        }
        //Ok
        private string ReturnFullPath(FileDTO file)
        {
            Folder folder = _data.Folders.Get(file.FolderId);
            return $@"{RootFilePath}\{folder.Path}\{folder.Name}\{file.Name}";
        }
        //Ok
        public FileDTO Get(int id)
        {
            return Mapper.Map<FileDTO>(_data.Files.Get(id));
        }
        //Ok
        public MemoryStream GetFileBytes(FileDTO fileDto)
        {
            return new MemoryStream(PhysicalFile.ReadAllBytes(ReturnFullPath(fileDto)));
        }
        //Ok
        public void Delete(FileDTO file)
        {
            _data.Files.Delete(file.Id);
            PhysicalFile.Delete(ReturnFullPath(file));
            _data.Save();
        }
        //Ok
        public void Dispose()
        {
            _data.Dispose();
        }
        //Ok
        public HashSet<FileDTO> GetAll()
        {
            return Mapper.Map<HashSet<FileDTO>>(_data.Files.GetAll());
        }
        //Ok
        public List<FileDTO> GetAllByUserId(string userid)
        {
            return GetAll().Where(f => f.Folder.UserId.Equals(userid)).ToList();
        }
        //Ok
        public void EditFile(int id, FileDTO fileDto)
        {
            var newFile = _data.Files.Get(id);
            string oldPath = ReturnFullPath(Mapper.Map<FileDTO>(newFile));
            newFile.Name = fileDto.Name;
            string newPath = ReturnFullPath(Mapper.Map<FileDTO>(newFile));
            PhysicalFile.Move(oldPath, newPath);
            newFile.AccessLevel = fileDto.AccessLevel;
            _data.Files.Update(newFile);

            _data.Save();
        }
        //Ok
        public bool IsFileExists(FileDTO file)
        {
            return PhysicalFile.Exists(ReturnFullPath(file));
        }
    }
}
