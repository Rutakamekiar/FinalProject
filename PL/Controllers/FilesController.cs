using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;


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
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_fileService.GetAllByUserId(User.Identity.GetUserId()));
        }
        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_fileService.GetAll());
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var file = _fileService.Get(id);
                if(file.AccessLevel == 0)
                    return new HttpResponseMessage(HttpStatusCode.NotFound); ;
                var dataBytes = File.ReadAllBytes($"C:/Users/Vlad/Desktop/FinalProject/PL/Files/{file.Name}");
                var dataStream = new MemoryStream(dataBytes);

                HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                httpResponseMessage.Content = new StreamContent(dataStream);
                httpResponseMessage.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = file.Name;
                httpResponseMessage.Content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/multi-form");

                return httpResponseMessage;
            }
            catch (FileNotFoundException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound); ;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); ;
            }
            
        }

        private string CreateFileFullPath(FileDTO fileDTO)
        {
            return $"{fileDTO.Path}/{fileDTO.Name}";
        }

        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                if (request.Files.Count <= 0)
                    return BadRequest("File was not found.Please upload it.");
                var file = request.Files[0];
                if (!(file?.ContentLength > 0))
                    return BadRequest("file?.ContentLength > 0");
                FileDTO fileDto = CreateFileDto(file.FileName);
                _fileService.Create(fileDto);
                file.SaveAs(HttpContext.Current.Server.MapPath
                    (CreateFileFullPath(fileDto)));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private FileDTO CreateFileDto(string fileName)
        {
            return new FileDTO()
            {
                Path = $"~/Files",
                Name = fileName,
                UserId = User.Identity.GetUserId(),
                AccessLevel = 1
            };
        }
        [HttpDelete]
        public IHttpActionResult DeleteFile(string name)
        {
            try
            {
                var file = _fileService.GetByName(name);
                if (file == null || file.UserId != User.Identity.GetUserId())
                    return BadRequest("File not found");
                string fullPath = HttpContext.Current.Server.MapPath(CreateFileFullPath(file));
                File.Delete(fullPath);
                _fileService.Delete(file.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteUserFile")]
        public IHttpActionResult DeleteUserFile(string name)
        {
            try
            {
                var file = _fileService.GetByName(name);
                if (file == null)
                    return BadRequest("File not found");
                string fullPath = HttpContext.Current.Server.MapPath(CreateFileFullPath(file));
                File.Delete(fullPath);
                _fileService.Delete(file.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
