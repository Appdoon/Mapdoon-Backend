﻿using Mapdoon.Application.Services.ChatSystem.Command.CreateChatMessageService;
using Mapdoon.Application.Services.ChatSystem.Query.GetAllMessagesService;
using Mapdoon.Application.Services.ChatSystem.Query.GetRegisterdUsersService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Mapdoon.Common.Interfaces;


namespace Mapdoon.WebApi.Controllers
{
    //[Authorize(policy: "User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatSystemController : ControllerBase
    {
        private readonly ICreateChatMessageService _createChatMessageService;
        private readonly IGetAllMessagesService _getallmessageservice;
        private readonly IGetRegisterdUsersService _getregisterdusersservice;
        public ChatSystemController(ICreateChatMessageService createChatMessageService
                                    , IGetAllMessagesService getallmessageservice
                                    ,IGetRegisterdUsersService getregisterdusersservice)
        {
            _createChatMessageService = createChatMessageService;
            _getallmessageservice = getallmessageservice;
            _getregisterdusersservice = getregisterdusersservice;
        }
        [HttpPost("{RoadMapId}")]
        public IActionResult Post(int RoadMapId, CreateMessageDto message,[FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _createChatMessageService.Execute(RoadMapId , userId , message);
            return Ok(result);
        }
        [HttpGet("{RoadMapId}")]
        public IActionResult Get(int RoadMapId , int PageNumber = 1, int PageSize = 15)
        {
            var result = _getallmessageservice.Execute(RoadMapId , PageNumber , PageSize);
            return Ok(result);
        }
        [HttpGet("{RoadMapId}")]
        public IActionResult GetRegisteredUsers(int RoadMapId)
        {
            var result = _getregisterdusersservice.Execute(RoadMapId);
            return Ok(result);
        }
    }
}
