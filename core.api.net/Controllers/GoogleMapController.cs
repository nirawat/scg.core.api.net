using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Core.Api.Net.Business.Services.Business;
using Core.Api.Net.Business.Models;
using Core.Api.Net.Business.Models.Response;

namespace Core.Api.Net.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GoogleMapController : ControllerBase
    {
        private readonly IGoogleMapApiService _IGoogleMapApiService;

        public GoogleMapController(IGoogleMapApiService IGoogleMapApiService)
        {
            _IGoogleMapApiService = IGoogleMapApiService;
        }

        [AllowAnonymous]
        [HttpGet("GetRestaurants/{Key1}/{Key2}/{Key3}")]
        public IActionResult GetRestaurants(string Key1, string Key2, string Key3)
        {
            ResponseMessage resp = new ResponseMessage();

            var list_data = _IGoogleMapApiService.GetRestaurants(Key1, Key2, Key3);

            if (list_data != null)
            {
                resp.status = StatusResponse.Success;
                resp.message = "Success";
                resp.data = list_data.results;

                return Ok(new { resp });
            }
            else return BadRequest();
        }

    }
}