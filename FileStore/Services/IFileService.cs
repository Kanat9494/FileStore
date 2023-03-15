namespace FileStore.Services;

public interface IFileService<TResponse>
{
    Task<ResponseMessage> SaveFile(TResponse response);
}
