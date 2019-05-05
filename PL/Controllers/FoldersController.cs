using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/folders")]
    public class FoldersController : ApiController
    {
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;

        public FoldersController(IFolderService folderService, IFileService fileService)
        {
            _folderService = folderService;
            _fileService = fileService;
        }

        //Ok
        [HttpPost]
        public IHttpActionResult CreateFolderInFolder(int folderId, string name)
        {
            var parent = _folderService.Get(folderId);
            if (parent.UserId != User.Identity.GetUserId())
                return BadRequest("cannot create folder in enemy folder");
            return Ok(_folderService.CreateFolderInFolder(parent, name));
        }

        //Ok
        [HttpGet]
        public IHttpActionResult Get()
        {
            if (User.IsInRole(Roles.Manager.ToString()))
                return Ok(Mapper.Map<List<FolderView>>(_folderService.GetAll()));
            return Ok(Mapper.Map<List<FolderView>>(
                _folderService.GetAllFolderContentByUserId(User.Identity.GetUserId())));
        }
        //Ok
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<FolderView>(_folderService.Get(id)));
        }
        //Ok
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult EditFile(int id, [FromBody] FolderDTO folder)
        {
            var folderDto = _folderService.Get(id);
            if (folderDto.UserId == User.Identity.GetUserId())
            {
                _folderService.EditFolder(id, folder);
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }

        //
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteFolder(int id)
        {

            var folderDto = _folderService.Get(id);
            if (!User.IsInRole("Admin") && folderDto.UserId != User.Identity.GetUserId())
                return BadRequest($"File not found");
            if (!folderDto.Files.Count.Equals(0))
                foreach (var file in folderDto.Files)
                    _fileService.Delete(file);

            if (!folderDto.Folders.Count.Equals(0))
                foreach (var folder in folderDto.Folders)
                    DeleteFolder(folder.Id);
            
            _folderService.Delete(folderDto);
            return Ok();
        }
    }
}
