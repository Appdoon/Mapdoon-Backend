using Mapdoon.Application.Services.PaymentGatway;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentGatewayService _paymentgatewayservice;
        public PaymentController(IPaymentGatewayService paymentGatewayService)
        {
           _paymentgatewayservice = paymentGatewayService;
        }
        [HttpGet("{RoadMapId}")]
        public IActionResult Get(int RoadMapId  , [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _paymentgatewayservice.ExecutePayment(RoadMapId , userId);
            return Ok(result);
        }

        [HttpPost("{PaymentId}/{Authority}")]
        public IActionResult Post(int PaymentId,string Authority, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _paymentgatewayservice.CheckVerification(PaymentId, Authority ,userId);
            return Ok(result);
        }
    }
}
