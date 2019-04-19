using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
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
            if(User.IsInRole("Manager"))
                return Ok(_fileService.GetAll());
            return Ok(_fileService.GetAllByUserId(User.Identity.GetUserId()));
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                var file = _fileService.GetByName(name);
                if(file.AccessLevel == 0)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                var dataBytes = File.ReadAllBytes(CreateFileFullPath(file));
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
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); ;
            }
            
        }
        private string CreateFileFullPath(FileDTO fileDTO)
        {
            return HttpContext.Current.Server.MapPath($"{fileDTO.Path}/{fileDTO.UserId}/{fileDTO.Name}");
        }

        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                if (request.Files.Count <= 0)
                    return BadRequest("File was not found. Please upload it.");
                var file = request.Files[0];
                if(File.Exists(HttpContext.Current.Server.MapPath($"~/Files/{User.Identity.GetUserId()}/{file.FileName}")))
                    return BadRequest("The file with the specified name exists. Please change the file name");
                if (!(file?.ContentLength > 0))
                    return BadRequest("file?.ContentLength > 0");
                FileDTO fileDto = CreateFileDto(file.FileName);
                _fileService.Create(fileDto);
                file.SaveAs(CreateFileFullPath(fileDto));
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
                if (!User.IsInRole("Admin") && file.UserId != User.Identity.GetUserId())
                        return BadRequest($"File not found; {file.UserId} | {User.Identity.GetUserId()}");
                string fullPath = CreateFileFullPath(file);
                File.Delete(fullPath);
                _fileService.Delete(file.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        //[Route("{name}")]
        public IHttpActionResult EditFile(string name, [FromBody]FileDTO file)
        {
            try
            {
                var fileDto = _fileService.GetByName(name);
                if (fileDto.UserId == User.Identity.GetUserId())
                {
                    _fileService.EditFile(name, file);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
