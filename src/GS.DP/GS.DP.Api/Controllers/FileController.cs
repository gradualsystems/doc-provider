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

    [HttpGet("{id}")]
    public async Task<ActionResult> Download([FromRoute] string id)
    {
        var model = await _facade.GetFileAsync(id);

        return File(model.Content, model.MimeType);
    }

    [HttpPost("Upload/{id}")]
    public async Task<ActionResult<IEnumerable<FileModel>>> Upload(int id, [FromForm] IEnumerable<IFormFile> files)
    {
        List<FileModel> list = new List<FileModel>();

        foreach (var file in files)
        {
            FileModel model = new FileModel();
            model.Name = file.FileName;
            model.Size = file.Length;

            if (file.Length > 0)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    //dto.Content = memoryStream.ToArray();

                    //dto = await fileService.UploadAsync(id, dto);

                }   
            }

            list.Add(model);
        }

        return Ok(list);
    }

}
