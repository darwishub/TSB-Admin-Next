using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace TheStartupBuddyV3.Service
{
    public class AWSUserImgService : IAWSUserImgService
    {
        private IAmazonS3 _s3Client;
        private readonly string AWS_accessKey;
        private readonly string AWS_secretKey;

        public AWSUserImgService(IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _s3Client = amazonS3;
            AWS_accessKey = configuration["AWSCredentials:AccessKey"] ??"";
            AWS_secretKey = configuration["AWSCredentials:SecretKey"] ?? "";
        }

        public async Task<string> UploadImageFile(IFormFile formFile, string key)
        {
            var fileNameFiltered = Regex.Replace(formFile.FileName.ToLower(), "[^a-zA-Z0-9]", String.Empty);
            var location = $"Next-TSB/User-img/{key}{fileNameFiltered}";
            var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.APSoutheast1);

            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var putRequest = new PutObjectRequest
                    {
                        Key = location,
                        BucketName = "www.thestartupbuddy.co",
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead,
                        AutoCloseStream = true,
                        ContentType = formFile.ContentType
                    };
                    await s3Client.PutObjectAsync(putRequest);
                    return location; 
                }
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }

        public async Task<string> UploadImageFile(IFormFile formFile)
        {
            var fileNameFiltered = Regex.Replace(formFile.FileName.ToLower(), "[^a-zA-Z0-9]", String.Empty);
            var location = $"Next-TSB/User-img/{fileNameFiltered}";
            var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.APSoutheast1);

            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var putRequest = new PutObjectRequest
                    {
                        Key = location,
                        BucketName = "www.thestartupbuddy.co",
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead,
                        AutoCloseStream = true,
                        ContentType = formFile.ContentType
                    };
                    await s3Client.PutObjectAsync(putRequest);
                    return location; 
                }
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }

        public async Task<string> DeleteImageFile(string key)
        {
            var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.APSoutheast1);
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = "www.thestartupbuddy.co",
                Key = $"Next-TSB/User-img/{key}"
            };

            await s3Client.DeleteObjectAsync(deleteRequest);
            return key;
        }

        public async Task<bool> CheckFileExists(string fileName)
        {
            var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.APSoutheast1);
            var request = new Amazon.S3.Model.GetObjectMetadataRequest
            {
                BucketName = "www.thestartupbuddy.co",
                Key = $"Next-TSB/User-img/{fileName}"
            };

            try
            {
                await s3Client.GetObjectMetadataAsync(request);
                return true;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                throw;
            }
        }

    }
}
