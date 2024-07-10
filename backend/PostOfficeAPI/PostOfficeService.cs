namespace PostOfficeAPI
{
    public class PostOfficeService
    {
        public async Task<string> GetPostOfficesJsonAsync()
        {
            var filePath = "C:\\Users\\leroy\\source\\repos\\shop\\backend\\PostOfficeAPI\\postOffices.json";
            var json = await File.ReadAllTextAsync(filePath);
            return json;
        }
    }
}
