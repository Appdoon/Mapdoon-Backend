using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.HashFunctions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mapdoon.Presistence.Features.File
{
    public class FacadeFileHandler : IFacadeFileHandler
    {
        private readonly IFileHandler _fileHandler;
        public FacadeFileHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public async Task<string> CreateFile(
            string title,
            string fileName,
            string bucketName,
            string contentType,
            IFormFile file)
        {
            string objectName = "";
            if (file != null)
            {
                objectName = GetFileSrc(title, fileName);
                await UploadFile(objectName, bucketName, contentType, file);
            }
            return objectName;
        }

        public async Task<string> UpdateFile(
            string title,
            string fileName,
            string bucketName,
            string contentType,
            string oldObjectName,
            IFormFile file)
        {
            if (file != null)
            {
                var objectName = GetFileSrc(title, fileName);
                await UploadFile(objectName, bucketName, contentType, file);

                if (oldObjectName != "" && await _fileHandler.IsObjectExist(bucketName, oldObjectName))
                {
                    await _fileHandler.RemoveObject(bucketName, oldObjectName);
                }

                return objectName;
            }

            return oldObjectName;
        }

        public async Task<string> GetFileUrl(string bucketName, string objectName)
        {
            if (objectName != "" && await _fileHandler.IsObjectExist(bucketName, objectName))
            {
                return await _fileHandler.GetObjectUrl(bucketName, objectName); 
            }

            return objectName;
        }

        private async Task UploadFile(string objectName, string bucketName, string contentType, IFormFile formFile)
        {
            await _fileHandler.CreateBucket(bucketName);
            Stream stream = formFile.OpenReadStream();
            await _fileHandler.SaveStreamObject(bucketName, objectName, stream, contentType);
        }

        private string GetFileSrc(string title, string filenName)
        {
            var name = title + "_" + DateTime.Now.Ticks.ToString();
            var imgaeSrc = $"({name})" + filenName.ToString();

            return MD5Hash.ComputeMD5(imgaeSrc);
        }
    }
}
