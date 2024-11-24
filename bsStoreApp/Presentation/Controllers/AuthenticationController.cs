using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public AuthenticationController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttiribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto user)
        {
            var result = await serviceManager.AuthenticationService.RegisterUser(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }
    }
}
