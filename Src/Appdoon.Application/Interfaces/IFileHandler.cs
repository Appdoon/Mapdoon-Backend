using Mapdoon.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Mapdoon.Application.Interfaces
{
    public interface IFileHandler : ITransientService
    {
        Task CreateBucket(string bucketName);
        Task RemoveBucket(string bucketName);
        Task<bool> IsBucketExists(string bucketName);
        Task SaveStreamObject(string bukcetName, string objetName, Stream fileStream, string contentType);
        Task SaveFileObject(string bukcetName, string objetName, string fileName, string contentType);
        Task GetObjectWithFile(string bukcetName, string objetName);
        Task RemoveObject(string bukcetName, string objectName);
        Task CopyObject(string sourceBukcetName, string sourceObjetName, string destinationBukcetName, string destinationObjetName);
        Task<string> GetObjectStat(string bukcetName, string objetName);
        Task<bool> IsObjectExist(string bukcetName, string objectName);
        List<string> GetObjectKeys(string bukcetName);
        Task<List<string>> GetBucketNames();
        Task<string> GetObjectUrl(string bukcetName, string objectName);
    }
}
