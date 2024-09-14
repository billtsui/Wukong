using System.Reflection;

namespace GoldenCudgel.Utils;

public class FileUtils
{
    public static List<FileInfo> ReadFileList()
    {
        var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var directoryInfo = new DirectoryInfo(currentDir);
        if (!directoryInfo.Exists) return [];

        var fileInfoList = directoryInfo.GetFiles();
        return fileInfoList.Length == 0
            ? []
            : fileInfoList.Where(f => f.Extension.Equals(".ncm")).ToList();
    }
}