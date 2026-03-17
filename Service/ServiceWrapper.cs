using Amazon.S3;
using Microsoft.Extensions.Configuration;

namespace TheStartupBuddyV3.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IEmailService? _emailService;
        private readonly EmailConfig _emailConfig;
        private IAWSUserImgService? _awsUserImgService;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public ServiceWrapper(EmailConfig emailConfig, IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _emailConfig = emailConfig;
            _s3Client = amazonS3;
            _configuration = configuration;
        }

        public IEmailService EmailService
        {
            get
            {
                if (_emailService == null)
                {
                    _emailService = new EmailService(_emailConfig);
                }
                return _emailService;
            }
        }

        public IAWSUserImgService AWSUserImgService
        {
            get
            {
                if (_awsUserImgService == null)
                {
                    _awsUserImgService = new AWSUserImgService(_s3Client, _configuration);
                }
                return _awsUserImgService;
            }
        }

    }
}
