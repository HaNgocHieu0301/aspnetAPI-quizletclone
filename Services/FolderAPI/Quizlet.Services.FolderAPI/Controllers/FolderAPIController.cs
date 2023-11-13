using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Repositories.Interfaces;

namespace Quizlet.Services.FolderAPI.Controllers
{
    [Route("api/folder")]
    [ApiController]
    public class FolderAPIController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FolderAPIController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(_folderRepository.GetFolders());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] FolderDTO folderDto)
        {
            try
            {
                int folderId = _folderRepository.AddFolder(folderDto);
                return Created("Updated Successfully", folderId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] FolderDTO folderDto)
        {
            try
            {
                _folderRepository.UpdateFolder(folderDto);
                return Ok("Updated Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete("/{folderId}")]
        public IActionResult Delete(int folderId)
        {
            try
            {
                _folderRepository.DeleteFolder(folderId);
                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}