namespace GoldenCudgel.Utils;

public class FileUtils
{
    public static List<FileInfo> ReadFileList(string directoryPath)
    {
        string currentDir = Directory.GetCurrentDirectory();
        DirectoryInfo directoryInfo = new DirectoryInfo(currentDir);
        if (!directoryInfo.Exists)
        {
            return [];
        }

        FileInfo[] fileInfoList = directoryInfo.GetFiles();
        return fileInfoList.Length == 0
            ? []
            : fileInfoList.Where(f => f.Extension.Equals(".ncm")).ToList();
    }
}