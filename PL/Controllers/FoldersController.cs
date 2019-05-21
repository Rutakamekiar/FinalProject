using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PL.Models;
using Roles = PL.Models.Roles;

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
        public IHttpActionResult CreateFolderInFolder()
        {
            HttpRequest request = HttpContext.Current.Request;
            int parentId = Convert.ToInt32(request.Form.Get("parentId"));
            string name = request.Form.Get("name");
            var parent = _folderService.Get(parentId);
            if (parent.UserId != User.Identity.GetUserId())
                return BadRequest("cannot create folder in folders of others");
            return Ok(_folderService.CreateFolderInFolder(parent, name));
        }

        //Ok
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(Mapper.Map<FolderView>(_folderService.GetRootFolderContentByUserId(User.Identity.GetUserId())));  
        }
        //Ok
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetByUserId(int id)
        {
            return Ok(Mapper.Map<FolderView>(_folderService.GetByUserId(id, User.Identity.GetUserId())));
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

        //Ok
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteFolder(int id)
        {

            var folderDto = _folderService.Get(id);
            if (!User.IsInRole("Admin") && folderDto.UserId != User.Identity.GetUserId())
                return BadRequest($"File not found");

            _folderService.Delete(folderDto);
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetAll()
        {
            return Ok(Mapper.Map<List<FolderView>>(_folderService.GetAllRootFolders()));
        }
        [Authorize(Roles = "Admin")]
        [Route("users/{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(Mapper.Map<FolderView>(_folderService.Get(id)));
        }


     
    }
}
