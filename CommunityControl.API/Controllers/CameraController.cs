using CommunityControl.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityControl.Api.Controllers
{
    [ApiController]
    [Route("camera")]
    [Authorize]
    public class CameraController : ControllerBase
    {
        private readonly ICameraService _cameraService;

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpGet("stream")]
        public async Task<IActionResult> GetStream()
        {
            var stream = await _cameraService.GetCameraStreamAsync();

            if (stream == null)
                return StatusCode(503, "Camera stream unavailable");

            var contentType = _cameraService.GetStreamContentType();

            return new FileStreamResult(stream, contentType)
            {
                EnableRangeProcessing = false
            };
        }
    }
}
