using API.Domain.Services;

namespace API.Infra.Services
{
    public class FileService : IFileService
    {
        public string FileToBase64(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
