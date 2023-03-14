namespace FileStore.Services;

public interface IFileService<TResponse>
{
    Task<IActionResult> SaveFile(TResponse response);
}
