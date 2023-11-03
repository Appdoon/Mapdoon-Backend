using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Mapdoon.Common.Interfaces;

namespace Apdoon.WebApi.Controllers
{
    // [Authorize(policy: "User")]
	[Route("api/[controller]/[action]")]
	[ApiController]
    public class CreateRateController : ControllerBase
    {
        // create
        private readonly ICreateRateService _createRateService;
        public CreateRateController(ICreateRateService createRateService)
        {
            _createRateService = createRateService;
        }
        [HttpPost("{RoadMapId}")]
        public JsonResult Post(int RoadMapId , CreateRateDto rateDto ,[FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _createRateService.Execute(RoadMapId , userId , rateDto);
            return new JsonResult(result);
        }
    }
}