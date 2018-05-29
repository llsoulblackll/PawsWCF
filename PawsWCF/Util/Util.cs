using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PawsWCF.Util
{
    public static class Util
    {
        public static class AWSUtil
        {

            private const string S3_BUCKET = "paws-wcf-bucket";

            private static readonly string S3_BUCKET_URL = $"https://{S3_BUCKET}.s3.amazonaws.com/";

            private const string ACCESS_KEY = "AKIAIIPAYTRDS76J42SQ";
            private const string SECRET_KEY = "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";

            private static readonly RegionEndpoint REGION = RegionEndpoint.USWest2;

            /// <summary>
            /// Uploads file to and AWS S3 bucket
            /// </summary>
            /// <param name="objectName">Name of the file</param>
            /// <param name="file">File as bytes</param>
            /// <returns>URL of the uploaded object</returns>
            public static string UploadToS3(string objectName, byte[] file)
            {
                using (var awsClient = new AmazonS3Client(ACCESS_KEY, SECRET_KEY, REGION))
                {
                    ////WE COULD USE THE ASYNC API BUT OUR CLIENT IS ALREADY ASYNC
                    //var putRequest = awsClient.PutObject(new PutObjectRequest
                    //{
                    //});

                    TransferUtility transferUtility = new TransferUtility(awsClient);

                    var request = new TransferUtilityUploadRequest
                    {
                        InputStream = new MemoryStream(file),
                        BucketName = S3_BUCKET,
                        Key = objectName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    transferUtility.Upload(request);

                    return $"{S3_BUCKET_URL}{objectName}";
                }
            }

            public static string UploadToS3(string objectName, string dataAsBase64)
            {
                using (var awsClient = new AmazonS3Client(ACCESS_KEY, SECRET_KEY, REGION))
                {
                    TransferUtility transferUtility = new TransferUtility(awsClient);

                    var request = new TransferUtilityUploadRequest
                    {
                        InputStream = new MemoryStream(Convert.FromBase64String(dataAsBase64)),
                        BucketName = S3_BUCKET,
                        Key = objectName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    transferUtility.Upload(request);

                    return $"{S3_BUCKET_URL}{objectName}";

                }
            }

            public static bool DeleteFromS3(string objectName)
            {
                using (var awsClient = new AmazonS3Client(ACCESS_KEY, SECRET_KEY, REGION))
                {
                    var res = awsClient.DeleteObject(S3_BUCKET, objectName);
                    return true;
                }
            }

            public static async Task<bool> UploadToS3Async(string bucketName, string objectName, byte[] file)
            {
                //using (var awsClient = new AmazonS3Client(ACCESS_KEY, SECRET_KEY, REGION))
                //{
                //}
                throw new NotImplementedException();
            }
        }

        public static class IOUtil
        {
            public static bool SaveFile(string path, byte[] data)
            {
                File.WriteAllBytes(path, data);
                return File.Exists(path);
            }

            public static bool SaveFile(string path, string dataAsBase64)
            {
                File.WriteAllBytes(path, Convert.FromBase64String(dataAsBase64));
                return File.Exists(path);
            }

            public static bool Exists(string path)
            {
                return File.Exists(path);
            }

            public static bool DeleteFile(string path)
            {
                if(File.Exists(path))
                    File.Delete(path);
                return !File.Exists(path);
            }

        }

    }
}