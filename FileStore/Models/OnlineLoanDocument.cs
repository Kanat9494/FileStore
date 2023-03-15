namespace FileStore.Models;

public class OnlineLoanDocument
{
    public int OnlineLoanId { get; set; }
    public string ClientITIN { get; set; }
    public string FileName { get; set; }
    public byte[]? ImageData { get; set; }
}
