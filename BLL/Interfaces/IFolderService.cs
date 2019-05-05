using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IFolderService : IService<FolderDTO>
    {
        FolderDTO CreateRootFolder(string userId, string email);
        HashSet<FolderDTO> GetAllFolderContentByUserId(string userId);
        FolderDTO CreateFolderInFolder(FolderDTO parent, string name);
        bool IsFolderExists(FolderDTO file);
        void EditFolder(int id, FolderDTO item);

    }
}
