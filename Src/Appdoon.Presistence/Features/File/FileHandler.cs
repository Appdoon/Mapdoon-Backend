using Mapdoon.Application.Interfaces;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mapdoon.Presistence.Features.File
{
    internal class FileHandler : IFileHandler
    {
        private readonly IMinioClient _minioClient;

        public IMinioClient MinioClient => _minioClient;

        public FileHandler()
        {
            var endponit = "188.121.116.198:9000";
            var accessKey = "minioadmin";
            var secretKey = "minioadmin";

            try
            {
                _minioClient = new MinioClient()
                    .WithEndpoint(endponit)
                    .WithCredentials(accessKey, secretKey)
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task CreateBucket(string bucketName)
        {
            if (!await IsBucketExists(bucketName))
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                await MinioClient.MakeBucketAsync(makeBucketArgs);
            }
        }

        public async Task RemoveBucket(string bucketName)
        {
            if (await IsBucketExists(bucketName))
            {
                var removeBucketArgs = new RemoveBucketArgs().WithBucket(bucketName);
                await MinioClient.RemoveBucketAsync(removeBucketArgs);
            }
        }

        public async Task<bool> IsBucketExists(string bucketName)
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            bool isBucketExist = await MinioClient.BucketExistsAsync(bucketExistArgs);

            return isBucketExist;
        }

        public async Task SaveStreamObject(string bukcetName, string objectName, Stream fileStream, string contentType)
        {
            PutObjectArgs putObjectArgs = new PutObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName)
                .WithObjectSize(fileStream.Length)
                .WithStreamData(fileStream)
                .WithContentType(contentType);

            await MinioClient.PutObjectAsync(putObjectArgs);
            await GetObjectStat(bukcetName, objectName);
        }

        public async Task SaveFileObject(string bukcetName, string objectName, string fileName, string contentType)
        {
            PutObjectArgs putObjectArgs = new PutObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName)
                .WithFileName(fileName)
                .WithContentType(contentType);

            await MinioClient.PutObjectAsync(putObjectArgs);
            await GetObjectStat(bukcetName, objectName);
        }

        public async Task GetObjectWithFile(string bukcetName, string objectName)
        {
            await GetObjectStat(bukcetName, objectName);
            GetObjectArgs getObjectArgs = new GetObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName)
                .WithFile(objectName);
            await MinioClient.GetObjectAsync(getObjectArgs);
        }

        public async Task RemoveObject(string bukcetName, string objectName)
        {
            await GetObjectStat(bukcetName, objectName);

            RemoveObjectArgs removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName);

            await MinioClient.RemoveObjectAsync(removeObjectArgs);
        }

        public async Task CopyObject(string sourceBukcetName, string sourceObjectName, string destinationBukcetName, string destinationObjectName)
        {
            var copySourceObjectArgs = new CopySourceObjectArgs()
                .WithBucket(sourceBukcetName)
                .WithObject(sourceObjectName);

            var copyObjectArgs = new CopyObjectArgs()
                .WithBucket(destinationBukcetName)
                .WithBucket(destinationObjectName)
                .WithCopyObjectSource(copySourceObjectArgs);

            await MinioClient.CopyObjectAsync(copyObjectArgs);
        }

        public async Task<string> GetObjectStat(string bukcetName, string objectName)
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName);

            var stat = await MinioClient.StatObjectAsync(statObjectArgs);

            return stat.ObjectName + ": " + stat.ETag + ": " + stat.VersionId + ": " + stat.ContentType;
        }

        public async Task<bool> IsObjectExist(string bukcetName, string objectName)
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName);

            var stat = await MinioClient.StatObjectAsync(statObjectArgs);

            return stat.ETag != null;
        }

        public List<string> GetObjectKeys(string bukcetName)
        {
            ListObjectsArgs listObjectsArgs = new ListObjectsArgs()
                .WithBucket(bukcetName);

            List<string> keys = new List<string>();

            var observalbe = MinioClient.ListObjectsAsync(listObjectsArgs);
            var subscription = observalbe.Subscribe(item => keys.Add(item.Key));

            Thread.Sleep(1000);

            return keys;
        }

        public async Task<List<string>> GetBucketNames()
        {
            var list = await MinioClient.ListBucketsAsync();

            List<string> bucketNames = new List<string>();
            foreach (Bucket bucket in list.Buckets)
            {
                bucketNames.Add(bucket.Name);
            }

            return bucketNames;
        }

        public async Task<string> GetObjectUrl(string bukcetName, string objectName)
        {
            PresignedGetObjectArgs presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bukcetName)
                .WithObject(objectName)
                .WithExpiry(60 * 60 * 24);

            string url = await MinioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            return url;
        }
    }
}
