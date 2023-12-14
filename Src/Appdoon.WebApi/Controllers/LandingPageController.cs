using Mapdoon.Application.Services.LandingPage;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LandingPageController : ControllerBase
    {
        private readonly ILandingPageServices _landingPageServices;
        public ILandingPageServices LandingPageServices => _landingPageServices;

        public LandingPageController(ILandingPageServices landingPageServices)
        {
            _landingPageServices = landingPageServices;
        }

        [HttpGet]
        public IActionResult GetStatistics()
        {
            var result = LandingPageServices.GetStatisticsService.Execute();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopEnrolledRoadmaps(int count)
        {
            var result = await LandingPageServices.GetTopEnrolledRoadmapsService.Execute(count);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopNewRoadmaps(int count)
        {
            var result = await LandingPageServices.GetTopNewRoadmapsService.Execute(count);
            return Ok(result);
        }
    }
}
