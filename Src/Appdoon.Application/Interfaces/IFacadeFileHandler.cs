using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Mapdoon.Application.Interfaces
{
    public interface IFacadeFileHandler : ITransientService
    {
        Task<string> CreateFile(
            string title,
            string fileName,
            string bucketName,
            string contentType,
            IFormFile file);
        Task<string> UpdateFile(
            string title,
            string fileName,
            string bucketName,
            string contentType,
            string oldObjectName,
            IFormFile file);
        Task<string> GetFileUrl(
            string bucketName, 
            string objectName);
    }
}
