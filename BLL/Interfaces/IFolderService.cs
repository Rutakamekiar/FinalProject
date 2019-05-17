using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IFolderService : IService<FolderDTO>
    {
        FolderDTO CreateRootFolder(string userId, string email);
        FolderDTO GetRootFolderContentByUserId(string userId);
        FolderDTO CreateFolderInFolder(FolderDTO parent, string name);
        FolderDTO GetByUserId(int id, string userId);

        bool IsFolderExists(FolderDTO file);
        void EditFolder(int id, FolderDTO item);

    }
}
