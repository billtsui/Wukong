using System.Text;

namespace NCMDecrypter;

public class Program
{
    public static void Main(string[] args)
    {
        FileHandling fileHandling = new FileHandling();
        var fileNameList = fileHandling.ReadFileList(Directory.GetCurrentDirectory());
        if (fileNameList.Count == 0)
        {
            Console.WriteLine("No file found.");
            return;
        }

        Console.WriteLine($"Found {fileNameList.Count} songs.");

        foreach (var filePath in fileNameList)
        {
            //读取 8 bytes的 magic header
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                /*
                 * 读取 magic head
                 */
                var magicHead = fileHandling.ReadMagicHead(fs);
                string head = Encoding.UTF8.GetString(magicHead);
                if (head != FileHandling.MAGIC_HEAD)
                {
                    Console.WriteLine($"File {filePath} is not a valid netease cloud music file.");
                    continue;
                }

                //跳过 2 bytes 
                fs.Seek(2, SeekOrigin.Current);

                /*
                 * 读取 4 bytes 的 key length，并且按照小端法转整型
                 * C# 在 Windows 平台上是小端法，所以不需要转
                 */
                var keyLength = fileHandling.ReadKeyLength(fs);

                /*
                 * 读取 keyLength 长度的密钥
                 */
                var KeyContent = fileHandling.ReadKeyContent(fs, keyLength);

                /*
                 * 读取 meta 长度
                 */
                var metaLength = fileHandling.ReadMetaLength(fs);

                /*
                 * 读取 meta 内容
                 */

                var metaInfo = fileHandling.ReadMetaContent(fs, metaLength);
                Console.WriteLine(metaInfo);
            }
        }


        Console.ReadLine();
    }
}