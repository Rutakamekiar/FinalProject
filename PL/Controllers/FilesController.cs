using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using PL.Models;


namespace PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        //Ok
        [HttpGet]
        public IHttpActionResult Get()
        {
            if(User.IsInRole(Roles.Manager.ToString()))
                return Ok(Mapper.Map<List<FileView>>(_fileService.GetAll()));
            return Ok(Mapper.Map<List<FileView>>(_fileService.GetAllByUserId(User.Identity.GetUserId())));
        }
        //Ok
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var file = _fileService.Get(id);
            if (file.AccessLevel == 0 && User.Identity.GetUserId() != file.Folder.UserId)
                return BadRequest("You have no access to this file");

            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(_fileService.GetFileBytes(file));
            httpResponseMessage.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = file.Name;
            httpResponseMessage.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/multi-form");
            var response = ResponseMessage(httpResponseMessage);
            return response;
        }
        
        //Ok
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.Files.Count <= 0)
                return BadRequest("File was not found. Please upload it.");
            var file = request.Files[0];
            if (file?.ContentLength <= 0)
                return BadRequest("File have not content");
            FileDTO fileDto = new FileDTO
            {
                AccessLevel = Convert.ToInt32(request.Form.Get("AccessLevel")),
                Name = file.FileName,
                FolderId = Convert.ToInt32(request.Form.Get("FolderId"))
            };

            if (_fileService.IsFileExists(fileDto))
                return BadRequest("The file with the specified name exists. Please change the file name");

            file.InputStream.Read(fileDto.FileBytes = new byte[file.ContentLength], 0, file.ContentLength);
            _fileService.Create(fileDto);
            return Ok();
        }

        //Ok
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteFile(int id)
        {
            var file = _fileService.Get(id);
            if (!User.IsInRole("Admin") && file.Folder.UserId != User.Identity.GetUserId())
                return BadRequest($"File not found");
            _fileService.Delete(file);
            return Ok();
        }

        //Ok
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult EditFile(int id, [FromBody] FileDTO file)
        {
            var fileDto = _fileService.Get(id);
            if (fileDto.Folder.UserId == User.Identity.GetUserId())
            {
                _fileService.EditFile(id, file);
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }
    }
}
