using Microsoft.AspNetCore.Identity;

namespace FileStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OnlineLoanDocumentController : ControllerBase
{
    public OnlineLoanDocumentController(IFileService<OnlineLoanDocument> fileService)
    {
        _fileService = fileService;
    }

    private readonly IFileService<OnlineLoanDocument> _fileService;

    [HttpPost("SaveDocument")]
    public async Task<IActionResult> SaveDocument([FromBody]OnlineLoanDocument document)
    {
        if (document == null)
            return BadRequest("Документ не загружен!");

        try
        {
            var response = await _fileService.SaveFile(document);

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
}
