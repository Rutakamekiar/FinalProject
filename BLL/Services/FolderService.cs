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
        public const string RootPath = @"C:\Users\Vlad\Desktop\FinalProject\Content\";

        public FolderService(IUnitOfWork data)
        {
            _data = data;
        }
        //Ok
        public HashSet<FolderDTO> GetAll()
        {
            return Mapper.Map<HashSet<FolderDTO>>(_data.Folders.GetAll());
        }
        //Ok
        public HashSet<FolderDTO> GetAllFolderContentByUserId(string userId)
        {
            return Mapper.Map<HashSet<FolderDTO>>(
                _data.Folders.GetAll().Where(f=>f.UserId.Equals(userId)));
        }
        //Ok
        public FolderDTO Get(int id)
        {
            return Mapper.Map<FolderDTO>(_data.Folders.Get(id));
        }
        //Ok
        public void Create(FolderDTO item)
        {
            _data.Folders.Create(Mapper.Map<Folder>(item));
            CreatePhysicalFolder();
            _data.Save();
            void CreatePhysicalFolder()
            {
                Directory.CreateDirectory(ReturnFullFolderPath(item));
            }
        }

        //Ok
        private string ReturnFolderPath(FolderDTO item)
        {
            return item.Path + @"\" + item.Name;
        }
        //Ok
        private string ReturnFullFolderPath(FolderDTO item)
        {
            return RootPath + item.Path + @"\" + item.Name;
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
            Folder folder = _data.Folders.Get(folderDto.Id);
            if (folder.Files.Count == 0)
            {
                foreach (var file in folder.Files)
                {

                }
            }
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
