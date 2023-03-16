using Microsoft.AspNetCore.Mvc;

namespace FileStore.Services;

public class OnlineLoanDocumentService : IFileService<OnlineLoanDocument>
{
    public async Task<ResponseMessage> SaveFileAsync(OnlineLoanDocument response)
    {
        try
        {
            string directoryPath = $"\\\\192.168.0.6\\abs_files\\{response.ClientITIN}";
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

            var fileName = response.FileName;
            var filePath = Path.Combine($"\\\\192.168.0.6\\abs_files\\{response.ClientITIN}\\OnlineLoans\\{response.OnlineLoanId}", (fileName ?? "").Trim());
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

    public async Task<List<byte[]>> GetFilesAsync(ImageUrlRequest imageUrl)
    {
        string filePath = $"\\\\192.168.0.6\\abs_files\\{imageUrl.ClientITIN}\\{imageUrl.MiddlePath}\\{imageUrl.OnlineLoanId}";
        if (!Directory.Exists(filePath))
            return null;

        List<byte[]> listOfFiles = new List<byte[]>();

        await Task.Run(() =>
        {
            string[] fileArray = Directory.GetFiles(filePath, "*.jpg");
            for (int i = 0; i < fileArray.Length; i++) 
            {
                listOfFiles.Add(ImageToByteArrayFromFilePath(fileArray[i]));
            }

            fileArray = Directory.GetFiles(filePath, "*.png");
            for (int i = 0; i < fileArray.Length; i++)
            {
                listOfFiles.Add(ImageToByteArrayFromFilePath(fileArray[i]));
            }
        });
        

        return listOfFiles;
    }

    static byte[] ImageToByteArrayFromFilePath(string imagefilePath)
    {
        byte[] imageArray = File.ReadAllBytes(imagefilePath);
        return imageArray;
    }
}
