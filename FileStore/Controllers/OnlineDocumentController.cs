using Microsoft.AspNetCore.Identity;

namespace FileStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OnlineDocumentController : ControllerBase
{
    public OnlineDocumentController(IFileService<OnlineLoanDocument> fileService)
    {
        _fileService = fileService;
    }

    private readonly IFileService<OnlineLoanDocument> _fileService;

    [HttpPost("SaveOnlineLoanDocument")]
    public async Task<IActionResult> SaveOnlineLoanDocument([FromBody]OnlineLoanDocument document)
    {
        if (document == null)
            return BadRequest("Документ не загружен!");

        try
        {
            var response = await _fileService.SaveFileAsync(document);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = ex.Message
            });
        }
    }

    [HttpPost("DisplayImages")]
    public async Task<IActionResult> DisplayImages([FromBody]ImageUrlRequest imageUrl)
    {
        var listOfFiles =  await _fileService.GetFilesAsync(imageUrl);

        if (listOfFiles == null)
            return NotFound("По данному пути не найдено файлов для отображения");

        return Ok(listOfFiles);
    }
}
