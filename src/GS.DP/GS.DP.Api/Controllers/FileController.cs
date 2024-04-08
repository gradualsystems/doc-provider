using GS.DP.Common;
using GS.DP.Facade;
using Microsoft.AspNetCore.Mvc;

namespace GS.DP.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IDocProviderFacade _facade;

    public FileController(IDocProviderFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("download/{id}")]
    public async Task<ActionResult> Download([FromRoute] string id)
    {
        var model = await _facade.GetFileAsync(id);

        return File(model.Content, model.MimeType);
    }

    [HttpPost("upload")]
    public async Task<ActionResult<string>> Upload(IFormFile file)
    {
        //if (files.Count() == 0) return BadRequest();

        //var file = files.FirstOrDefault();

        if (file == null) return BadRequest();

        if (file.Length == 0) return BadRequest();

        FileModel model = new FileModel();
        model.Name = file.FileName;
        model.Extension = Path.GetExtension(file.FileName);
        model.Size = file.Length;

        using (var memoryStream = new System.IO.MemoryStream())
        {
            await file.CopyToAsync(memoryStream);

            model.Content = memoryStream.ToArray();
        }

        string id = await _facade.SaveFileAsync(model);

        model.Id = id;

        return Ok(model.Id);
    }

}
