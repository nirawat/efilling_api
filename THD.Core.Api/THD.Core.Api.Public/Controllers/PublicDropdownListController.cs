using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using THD.Core.Api.Models;
using THD.Core.Api.Public.Models;

namespace THD.Core.Api.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicDropdownListController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDropdownListController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        [HttpGet("GetAllRegisterUserByCharacter/{Character}")]
        public async Task<IActionResult> GetAllRegisterUserByCharacter(string character)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDropdownList"}/{"GetAllRegisterUserByCharacter"}/{character}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }
        }



    }
}