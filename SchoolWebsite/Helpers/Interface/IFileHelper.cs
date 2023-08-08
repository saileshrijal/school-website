namespace SchoolWebsite.Helpers.Interface
{
    public interface IFileHelper
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}