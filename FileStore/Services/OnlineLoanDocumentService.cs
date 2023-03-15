namespace FileStore.Services;

public class OnlineLoanDocumentService : IFileService<OnlineLoanDocument>
{
    public Task<IActionResult> SaveFile(OnlineLoanDocument response)
    {
        try
        {
            var filePath = Path.Combine("C:\\Users\\kkudaibergenov\\Desktop\\docs", response.ImageDate.ToString());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
