using BlobProcessing.Contracts;
using BlobProcessing.Settings;
using Microsoft.Extensions.Options;

namespace BlobProcessing.Works;

public class BlobService(IOptions<BlobSettings> options) : IBlobService
{
    public FileStream CreateStreamAsync(string fileName)
    {
        var filePath = Path.Combine(options.Value.ResourceFolder, fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        return fileStream;
    }

    public FileStream CreateStreamAsync(string fileName, string folderName)
    {
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(Path.Combine(options.Value.ResourceFolder, folderName));
        }

        var filePath = Path.Combine(options.Value.ResourceFolder, folderName, fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        return fileStream;
    }
}