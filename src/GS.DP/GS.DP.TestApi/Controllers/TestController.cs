using GS.DP.TestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GS.DP.TestApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync(string id)
    {
        var result = await _testService.RunDownloadAsync(id);

        return Ok(1);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync()
    {
        var result = await _testService.RunUploadAsync();

        return Ok(result);
    }
}
