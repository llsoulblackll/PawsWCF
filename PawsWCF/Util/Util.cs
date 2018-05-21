using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PawsWCF.Util
{
    public static class Util
    {

        public static class AWSUtil
        {
            public static Task<bool> UploadToS3Async(string bucketName, string objectName, byte[] file)
            {
                return null;
            }
        }

        public static class IOUtil
        {

            public static bool SaveFile(string path, byte[] data)
            {
                return true;
            }

            public static bool SaveFile(string path, string dataAsBase64)
            {
                return true;
            }

        }

    }
}