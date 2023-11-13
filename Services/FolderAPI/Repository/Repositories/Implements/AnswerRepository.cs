using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.DTO;
using DataAccess;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implements
{
    public class FolderRepository : IFolderRepository
    {
        private readonly IMapper mapper;

        public FolderRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<FolderDTO> GetFolders()
        {
            var folders = FolderDAO.Get();
            return mapper.Map<List<FolderDTO>>(folders);
        }

        public FolderDTO GetFolderById(int id)
        {
            var folder = FolderDAO.Get(id);
            return mapper.Map<FolderDTO>(folder);
        }

        public int AddFolder(FolderDTO folderDto)
        {
            Folder folder = mapper.Map<Folder>(folderDto);
            return FolderDAO.Add(folder);
        }

        public void DeleteFolder(int id)
        {
            Folder folder = FolderDAO.Get(id);
            if (folder != null)
                FolderDAO.Remove(id);
        }

        public void UpdateFolder(FolderDTO editFolderDto)
        {
            Folder folder = FolderDAO.Get(editFolderDto.FolderId);
            folder = mapper.Map(editFolderDto, folder);
            FolderDAO.Update(folder);
        }
    }
}