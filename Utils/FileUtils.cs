namespace GoldenCudgel.Utils;

public class FileUtils
{
    public static List<FileInfo> ReadFileList(string directoryPath)
    {
        var currentDir = Directory.GetCurrentDirectory();
        var directoryInfo = new DirectoryInfo(currentDir);
        if (!directoryInfo.Exists) return [];

        var fileInfoList = directoryInfo.GetFiles();
        return fileInfoList.Length == 0
            ? []
            : fileInfoList.Where(f => f.Extension.Equals(".ncm")).ToList();
    }
}