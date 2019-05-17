using System;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using AutoMapper;
using DAL.Entities;
using  System.IO;
using System.Linq;

namespace BLL.Services
{
    public class FolderService : IFolderService
    {
        private readonly IUnitOfWork _data;
        private IFileService _fileService;
        public const string RootPath = @"C:\Users\Vlad\Desktop\FinalProject\Content\";

        public FolderService(IUnitOfWork data, IFileService fileService)
        {
            _data = data;
            _fileService = fileService;
        }
        //Ok
        public HashSet<FolderDTO> GetAll()
        {
            return Mapper.Map<HashSet<FolderDTO>>(_data.Folders.GetAll());
        }
        //Ok
        public FolderDTO GetRootFolderContentByUserId(string userId)
        {
            return Mapper.Map<FolderDTO>(
                _data.Folders.GetAll()
                    .Where(f => f.UserId.Equals(userId))
                    .Where(f => f.ParentFolderId.Equals(null)).FirstOrDefault()
                ?? throw new FolderNotFoundException($"Cannot find root folder with userId = {userId}"));
        }
        //Ok
        public FolderDTO Get(int id)
        {
            return Mapper.Map<FolderDTO>(_data.Folders.Get(id));
        }
        //Ok
        public FolderDTO GetByUserId(int id, string userId)
        {
            var folder = Mapper.Map<FolderDTO>(_data.Folders.Get(id));
            return folder.UserId.Equals(userId)
                ? folder
                : throw new FolderNotFoundException($"Cannot find folder with id = {id} and userId = {userId}");
        }

        //Ok
        public void Create(FolderDTO item)
        {
            _data.Folders.Create(Mapper.Map<Folder>(item));
            Directory.CreateDirectory(ReturnFullFolderPath(item));
            _data.Save();
        }

        //Ok
        private string ReturnFolderPath(FolderDTO item)
        {
            return item.Path + @"\" + item.Name;
        }
        //Ok
        private string ReturnFullFolderPath(FolderDTO item)
        {
            return RootPath + ReturnFolderPath(item);
        }
        //Ok
        public void EditFolder(int id, FolderDTO item)
        {
            var folder = _data.Folders.Get(id);
            string oldPath = ReturnFolderPath(Mapper.Map<FolderDTO>(folder));
            folder.Name = item.Name;
            string newPath = ReturnFolderPath(Mapper.Map<FolderDTO>(folder));
            Directory.Move(RootPath + oldPath, RootPath + newPath);
            _data.Folders.Update(folder);

            _data.Save();
        }
        public void Delete(FolderDTO folderDto)
        {
            if (!folderDto.Files.Count.Equals(0))
                foreach (var file in folderDto.Files)
                    _fileService.Delete(file);

            if (!folderDto.Folders.Count.Equals(0))
                foreach (var folder in folderDto.Folders)
                    Delete(folder);

            _data.Folders.Delete(folderDto.Id);
            Directory.Delete(ReturnFullFolderPath(folderDto));
            _data.Save();
        }

        //Ok
        public FolderDTO CreateRootFolder(string userId, string email)
        {
            FolderDTO folder = new FolderDTO()
            {
                Name = email,
                Path = "",
                UserId = userId,
            };
            Create(folder);
            return folder;
        }

        //Ok
        public FolderDTO CreateFolderInFolder(FolderDTO parent, string name)
        {
            FolderDTO folder = new FolderDTO()
            {
                Name = name,
                Path = ReturnFolderPath(parent),
                ParentFolderId = parent.Id,
                UserId = parent.UserId,
            };
            if (IsFolderExists(folder))
            {
                throw new Exception("The folder with the specified name exists. Please change the folder name");
            }
            Create(folder);
            return folder;
        }
        //Ok
        public bool IsFolderExists(FolderDTO file)
        {
            return Directory.Exists(ReturnFullFolderPath(file));
        }
        //Ok
        public void Dispose()
        {
            _data.Dispose();
        }
    }
}
