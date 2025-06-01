namespace BlobProcessing.Contracts;

public interface IBlobService
{
    FileStream CreateStreamAsync(string fileName);
    FileStream CreateStreamAsync(string fileName, string folderName);
}