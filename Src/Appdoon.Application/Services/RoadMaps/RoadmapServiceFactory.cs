using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Roadmaps.Command.CreateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.DeleteRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.UpdateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Query.GetAllRoadmapsService;
using Appdoon.Application.Services.Roadmaps.Query.GetIndividualRoadmapService;
using Appdoon.Application.Services.RoadMaps.Command.BookmarkRoadmapService;
using Appdoon.Application.Services.RoadMaps.Command.RegisterRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.CheckUserRegisterRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.FilterRoadmapsService;
using Appdoon.Application.Services.RoadMaps.Query.GetPreviewRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.GetUserRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.SearchRoadmapsService;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Mapdoon.Application.Services.Roadmaps
{
	public interface IRoadmapServiceFactory : ITransientService
	{
		IBookmarkRoadmapService BookmarkRoadmapService { get; }
		ICreateRoadmapService CreateRoadmapService { get; }
		IDeleteRoadmapService DeleteRoadmapService { get; }
		IRegisterRoadmapService RegisterRoadmapService { get; }
		IUpdateRoadmapService UpdateRoadmapService { get; }
		ICheckUserRegisterRoadmapService CheckUserRegisterRoadmapService { get; }
		IFilterRoadmapsService FilterRoadmapsService { get; }
		IGetAllRoadmapsService GetAllRoadmapsService { get; }
		IGetIndividualRoadmapService GetIndividualRoadmapService { get; }
		IGetPreviewRoadmapService GetPreviewRoadmapService { get; }
		IGetUserRoadmapService GetUserRoadmapService { get; }
		ISearchRoadmapsService SearchRoadmapsService { get; }
	}
	internal class RoadmapServiceFactory : IRoadmapServiceFactory
	{
		private readonly IDatabaseContext _context;
		private readonly IFacadeFileHandler _facadeFileHandler;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserHubConnectionIdManager _userHubConnectionIdManager;
		private readonly IRoadmapPermissionManager _roadmapPermissionManager;

		public RoadmapServiceFactory(IDatabaseContext context, 
			IFacadeFileHandler facadeFileHandler, 
			IHttpContextAccessor httpContextAccessor, 
			IUserHubConnectionIdManager userHubConnectionIdManager,
            IRoadmapPermissionManager roadmapPermissionManager)
		{
			_context = context;
			_facadeFileHandler = facadeFileHandler;
			_httpContextAccessor = httpContextAccessor;
			_userHubConnectionIdManager = userHubConnectionIdManager;
			_roadmapPermissionManager = roadmapPermissionManager;
    }

		private IBookmarkRoadmapService _bookmarkRoadmapService;
		public IBookmarkRoadmapService BookmarkRoadmapService
		{
			get
			{
				return _bookmarkRoadmapService ??= new BookmarkRoadmapService(_context);
			}
		}

		private ICreateRoadmapService _createRoadmapService;
		public ICreateRoadmapService CreateRoadmapService
		{
			get
			{
				return _createRoadmapService ??= new CreateRoadmapService(_context, _facadeFileHandler, _roadmapPermissionManager);
			}
		}

		private IDeleteRoadmapService _deleteRoadmapService;
		public IDeleteRoadmapService DeleteRoadmapService
		{
			get
			{
				return _deleteRoadmapService ??= new DeleteRoadmapService(_context);
			}
		}

		private IRegisterRoadmapService _registerRoadmapService;
		public IRegisterRoadmapService RegisterRoadmapService
		{
			get
			{
				return _registerRoadmapService ??= new RegisterRoadmapService(_context);
			}
		}

		private IUpdateRoadmapService _updateRoadmapService;
		public IUpdateRoadmapService UpdateRoadmapService
		{
			get
			{
				return _updateRoadmapService ??= new UpdateRoadmapService(_context, _facadeFileHandler);
			}
		}

		private ICheckUserRegisterRoadmapService _checkUserRegisterRoadmap;
		public ICheckUserRegisterRoadmapService CheckUserRegisterRoadmapService
		{
			get
			{
				return _checkUserRegisterRoadmap ??= new CheckUserRegisterRoadmapService(_context);
			}
		}

		private IFilterRoadmapsService _filterRoadmapsService;
		public IFilterRoadmapsService FilterRoadmapsService
		{
			get
			{
				return _filterRoadmapsService ??= new FilterRoadmapsService(_context, _facadeFileHandler);
			}
		}

		private IGetAllRoadmapsService _getAllRoadmapsService;
		public IGetAllRoadmapsService GetAllRoadmapsService
		{
			get
			{
				return _getAllRoadmapsService ??= new GetAllRoadmapsService(_context, _facadeFileHandler);
			}
		}

		private IGetIndividualRoadmapService _getIndividualRoadmapService;
		public IGetIndividualRoadmapService GetIndividualRoadmapService
		{
			get
			{
				return _getIndividualRoadmapService ??= new GetIndividualRoadmapService(_context, _facadeFileHandler);
			}
		}

		private IGetPreviewRoadmapService _getPreviewRoadmapService;
		public IGetPreviewRoadmapService GetPreviewRoadmapService
		{
			get
			{
				return _getPreviewRoadmapService ??= new GetPreviewRoadmapService(_context, _facadeFileHandler);
			}
		}

		private IGetUserRoadmapService _getUserRoadmapService;
		public IGetUserRoadmapService GetUserRoadmapService
		{
			get
			{
				return _getUserRoadmapService ??= new GetUserRoadmapService(_context, _facadeFileHandler, _httpContextAccessor, _userHubConnectionIdManager);
			}
		}

		private ISearchRoadmapsService _searchRoadmapsService;
		public ISearchRoadmapsService SearchRoadmapsService
		{
			get
			{
				return _searchRoadmapsService ??= new SearchRoadmapsService(_context, _facadeFileHandler);
			}
		}
	}
}
