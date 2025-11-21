namespace CommunityControl.Api.Services
{
    public class CameraService : ICameraService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CameraService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Stream?> GetCameraStreamAsync()
        {
            var client = _httpClientFactory.CreateClient("CameraStream");

            var response = await client.GetAsync("/stream.mjpg",
                HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }

        public string GetStreamContentType()
        {
            // Tipo usado por python
            return "multipart/x-mixed-replace; boundary=frame";
        }
    }
}
