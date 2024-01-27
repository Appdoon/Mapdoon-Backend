using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;

namespace Mapdoon.Application.Interfaces
{
    public interface IPermissionManager : ITransientService
    {
        public abstract bool CanView(int userId, int entityId);
        public abstract bool CanCreate(int userId);
        public abstract bool CanEdit(int userId, int entityId);
        public abstract bool CanDelete(int userId, int entityId);
        public abstract PermissionView GetPermissionView(int userId, int entityId);
        public abstract ResultDto<PermissionView> GetPremissionViewDto(int userId, int entityId);
    }
    public abstract class PermissionManager : IPermissionManager
    {
        public static void CheckPermission(bool can, string message = null)
        {
            if (!can)
            {
                throw new UnauthorizedAccessException(message ?? "Permission Denied!");
            }
        }

        public virtual bool CanView(int userId, int entityId)
        {
            return true;
        }
        public virtual bool CanCreate(int userId)
        {
            return true;
        }
        public virtual bool CanEdit(int userId, int entityId)
        {
            return true;
        }
        public virtual bool CanDelete(int userId, int entityId)
        {
            return true;
        }
        public virtual PermissionView GetPermissionView(int userId, int entityId)
        {
            return new PermissionView
            {
                CanView = CanView(userId, entityId),
                CanCreate = CanCreate(userId),
                CanEdit = CanEdit(userId, entityId),
                CanDelete = CanDelete(userId, entityId),
            };
        }

        public virtual ResultDto<PermissionView> GetPremissionViewDto(int userId, int entityId)
        {

            return new ResultDto<PermissionView>()
            {
                IsSuccess = true,
                Message = "دسترسی ها ارسال شدند.",
                Data = GetPermissionView(userId, entityId),
            };
        }
    }

    public class PermissionView
    {
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
