using Microsoft.Extensions.Options;
using System.Text.Json;

namespace PostOfficeAPI
{
    public class PostOfficeService
    {
        private readonly IOptions<PostOfficeSettings> _settings;

        public PostOfficeService(IOptions<PostOfficeSettings> settings)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<PostOffice>> GetPostOfficesAsync()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.Value.PostOfficesFilePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The post offices file was not found.", filePath);
            }

            var postOfficesJson = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<IEnumerable<PostOffice>>(postOfficesJson);
        }
    }
}
