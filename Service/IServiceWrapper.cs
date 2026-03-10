namespace TheStartupBuddyV3.Service
{
    public interface IServiceWrapper
    {
        IEmailService EmailService { get; }
        IAWSUserImgService AWSUserImgService { get; }

    }
}
