using Appdoon.Application.Interfaces;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;
using Mapdoon.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mapdoon.Application.Services.Roadmaps
{
    public interface IRoadmapPermissionManager : IPermissionManager
    {
    }
    internal class RoadmapPermissionManager : PermissionManager, IRoadmapPermissionManager
    {
        private readonly IDatabaseContext _context;
        public RoadmapPermissionManager(IDatabaseContext context)
        {
            _context = context;
        }

        public override bool CanCreate(int userId)
        {
            List<Role> roles = _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .FirstOrDefault();

            List<string> roleNames = roles.Select(r => r.Name).ToList();

            return roleNames.Contains("Admin") || roleNames.Contains("Teacher");
        }

        public override bool CanEdit(int userId, int roadmapId)
        {
            RoadMap roadmap = _context.RoadMaps.Where(r => r.Id == roadmapId).FirstOrDefault();
            return roadmap.CreatoreId == userId;
        }

        public override bool CanDelete(int userId, int roadmapId)
        {
            RoadMap roadmap = _context.RoadMaps.Where(r => r.Id == roadmapId).FirstOrDefault();
            return roadmap.CreatoreId == userId;
        }

        public override bool CanView(int userId, int roadmapId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault() != null;
        }

        public bool CanBookmarkRoadmap(int userId, int roadmapId)
        {
            return CanView(userId, roadmapId);
        }

        public override RoadmapPermissionView GetPermissionView(int userId, int roadmapId)
        {
            _context.RoadMaps.Find(roadmapId).Should().NotBeNull(); 
            _context.Users.Find(userId).Should().NotBeNull();

            PermissionView oldPermissionView = base.GetPermissionView(userId, roadmapId);

            RoadmapPermissionView permissionView = new RoadmapPermissionView
            {
                CanBookmarkRoadmap = CanBookmarkRoadmap(userId, roadmapId),
                CanCreate = oldPermissionView.CanCreate,
                CanDelete = oldPermissionView.CanDelete,
                CanView = oldPermissionView.CanView,
                CanEdit = oldPermissionView.CanEdit,
            };

            return permissionView;
        }

        public class RoadmapPermissionView : PermissionView
        {
            public bool CanBookmarkRoadmap { get; set; }
        }
    }
}
