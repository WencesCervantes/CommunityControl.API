namespace CommunityControl.Api.Services
{
    public interface ICameraService
    {
        Task<Stream?> GetCameraStreamAsync();
        string GetStreamContentType();
    }
}
