using Microsoft.AspNetCore.Mvc;

namespace FileStore.Services;

public class OnlineLoanDocumentService : IFileService<OnlineLoanDocument>
{
    public async Task<ResponseMessage> SaveFile(OnlineLoanDocument response)
    {
        try
        {
            string directoryPath = $"C:\\Users\\kkudaibergenov\\Desktop\\docs\\{response.ClientITIN}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                directoryPath = directoryPath + "\\" + "OnlineLoans";
                if (!Directory.Exists(directoryPath)) 
                    Directory.CreateDirectory(directoryPath);

                directoryPath = directoryPath + "\\" + response.OnlineLoanId.ToString();
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }
            else
            {
                directoryPath = directoryPath + "\\" + "OnlineLoans";
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                directoryPath = directoryPath + "\\" + response.OnlineLoanId.ToString();
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }

            var fileName = response.FileName + ".jpg";
            var filePath = Path.Combine($"C:\\Users\\kkudaibergenov\\Desktop\\docs\\{response.ClientITIN}\\OnlineLoans\\{response.OnlineLoanId}", fileName);
            await File.WriteAllBytesAsync(filePath, response.ImageData);

            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Документ успешно загружен",
            };
        }
        catch (Exception ex)
        {
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = ex.Message,
            };
        }
    }
}
