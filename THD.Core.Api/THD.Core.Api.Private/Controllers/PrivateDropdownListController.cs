using THD.Core.Api.Business.Interface;
using THD.Core.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Text;
using THD.Core.Api.Helpers;
using System.Threading;
using System.Collections.Generic;

namespace THD.Core.Api.Private.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateDropdownListController : ControllerBase
    {
        private readonly IDropdownListService _IDropdownListService;

        public PrivateDropdownListController(IDropdownListService IDropdownListService)
        {
            _IDropdownListService = IDropdownListService;
        }


        [HttpGet("GetAllRegisterUserByCharacter/{Character}")]
        public async Task<IActionResult> GetAllRegisterUserByCharacter(string character)
        {
            IList<ModelSelectOption> e = await _IDropdownListService.GetAllRegisterUserByCharacterAsync(character);

            if (e != null) return Ok(e);
            return BadRequest();
        }


    }
}