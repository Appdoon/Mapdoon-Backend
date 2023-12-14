using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.LandingPage.Query.GetStatisticsService;
using Appdoon.Application.Services.LandingPage.Query.GetTopEnrolledRoadmapsService;
using Appdoon.Application.Services.LandingPage.Query.GetTopNewRoadmapsService;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;

namespace Mapdoon.Application.Services.LandingPage
{
    public interface ILandingPageServices : ITransientService
    {
        IGetStatisticsService GetStatisticsService { get; }
        IGetTopEnrolledRoadmapsService GetTopEnrolledRoadmapsService { get; }
        IGetTopNewRoadmapsService GetTopNewRoadmapsService { get; }
    }
    internal class LandingPageServices : ILandingPageServices
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public LandingPageServices(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }

        private IGetStatisticsService _getStatisticsService;
        public IGetStatisticsService GetStatisticsService
        {
            get
            {
                return _getStatisticsService ??= new GetStatisticsService(_context);
            }
        }

        private IGetTopEnrolledRoadmapsService _getTopEnrolledRoadmapsService;
        public IGetTopEnrolledRoadmapsService GetTopEnrolledRoadmapsService
        {
            get
            {
                return _getTopEnrolledRoadmapsService ??= new GetTopEnrolledRoadmapsService(_context, _facadeFileHandler);
            }
        }

        private IGetTopNewRoadmapsService _getTopNewRoadmapsService;
        public IGetTopNewRoadmapsService GetTopNewRoadmapsService
        {
            get
            {
                return _getTopNewRoadmapsService ??= new GetTopNewRoadmapsService(_context, _facadeFileHandler);
            }
        }
    }
}
