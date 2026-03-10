namespace TheStartupBuddyV3.Service
{
    public interface IAWSUserImgService
    {
        Task<string> UploadImageFile(IFormFile formFile, string key);
        Task<string> DeleteImageFile(string key);
        Task<bool> CheckFileExists(string fileName);
        Task<string> UploadImageFile(IFormFile formFile);
    }
}
