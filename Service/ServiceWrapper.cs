using Amazon.S3;

namespace TheStartupBuddyV3.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IEmailService? _emailService;
        private readonly EmailConfig _emailConfig;
        private IAWSUserImgService? _awsUserImgService;
        private readonly IAmazonS3 _s3Client;

        public ServiceWrapper(EmailConfig emailConfig, IAmazonS3 amazonS3)
        {
            _emailConfig = emailConfig;
            _s3Client = amazonS3;
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
                    _awsUserImgService = new AWSUserImgService(_s3Client);
                }
                return _awsUserImgService;
            }
        }

    }
}
