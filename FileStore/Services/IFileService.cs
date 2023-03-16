namespace FileStore.Services;

public interface IFileService<TResponse>
{
    Task<ResponseMessage> SaveFileAsync(TResponse response);

    Task<List<byte[]>> GetFilesAsync(ImageUrlRequest imageUrl);
}
