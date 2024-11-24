using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class LibsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public LibsController(IServiceManager manager)
        {
            _manager = manager; //BU injection ifadesi katmanlar arası bağlantı sağlamada kullanılır 
        }
        [HttpGet]
        [ServiceFilter(typeof (ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _manager
                .libService
                .GetAllBooksAsync(bookParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.libs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "Id")] int id)
        {
            
                var book = await _manager.libService.GetOneBookAsync(id, false);
               
                return Ok(book);
            
           
        }
        [ServiceFilter(typeof(ValidationFilterAttiribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoInsertion bookDto)
        {
           
            var book =await _manager.libService.CreateOneBookAsync(bookDto);
            return StatusCode(201, book);
            
        }
        [ServiceFilter(typeof(ValidationFilterAttiribute))]
        [HttpPut]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "Id")] int id, [FromBody] BookDtoUpdate bookDto)
        {

                await _manager.libService.UpdateOneBookAsync(id, bookDto,true);
                return NoContent();
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "Id")] int id, [FromBody] Libs lib)
        {
            try
            {


                await _manager.libService.DeleteOneBookAsync(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
        [HttpPatch]
        public async Task <IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "Id")] int id, [FromBody] JsonPatchDocument<BookDto> bookPatch)
        {
            
                var entity = await _manager.libService.GetOneBookAsync(id, true);
               
                bookPatch.ApplyTo(entity);
                await _manager.libService.UpdateOneBookAsync(id, new BookDtoUpdate{
                    Id=entity.Id,
                    Title=entity.Title,
                    Price=entity.Price,
                }, true);
                
                return NoContent();
           
        }
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetAllBooksWithDetailsAsync()
        {
            return Ok(await _manager
                .libService
                .GetAllBooksWithDetailsAsync(false));
        }
    }
}

