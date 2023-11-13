using BusinessObject.Models.DTO;

namespace Repository.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        List<FolderDTO> GetFolders();
        FolderDTO GetFolderById(int id);
        int AddFolder(FolderDTO folderDTO);
        void UpdateFolder(FolderDTO editFolderDTO);
        void DeleteFolder(int id);
    }
}
