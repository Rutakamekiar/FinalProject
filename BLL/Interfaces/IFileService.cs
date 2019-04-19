using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFileService : IService<FileDTO>
    {
        FileDTO GetByName(string name);
        List<FileDTO> GetAllByUserId(string userid);
        void EditFile(string id, FileDTO fileDto);
    }
}
