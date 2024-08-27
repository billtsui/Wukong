using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace NCMDecrypter;

public class FileHandling
{
    public static string MAGIC_HEAD = "CTENFDAM";

    /**
    * RC4密钥
    */
    public static byte[] CORE_KEY = "hzHRAmso5kInbaxW"u8.ToArray();

    /**
     * 加密 META信息的密钥
     */
    public static byte[] META_KEY = "#14ljk_!\\]&0U<'("u8.ToArray();

    public List<String> ReadFileList(string directoryPath)
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
            : fileInfoList.ToList().Where(f => f.Extension.Equals(".ncm")).Select(i => i.FullName).ToList();
    }

    /**
     * 8 bytes Magic Header
     */
    public byte[] ReadMagicHead(FileStream fs)
    {
        byte[] head = new byte[8];
        var readResult = fs.Read(head, 0, head.Length);
        return head;
    }

    /**
     * 4 bytes key length
     */
    public int ReadKeyLength(FileStream fs)
    {
        byte[] keyLengthArray = new byte[4];
        var readResult = fs.Read(keyLengthArray, 0, keyLengthArray.Length);
        return BitConverter.ToInt32(keyLengthArray, 0);
    }

    /**
     * 读取密钥内容
     */
    public byte[] ReadKeyContent(FileStream fs, int length)
    {
        byte[] keyContent = new byte[length];
        var readResult = fs.Read(keyContent, 0, length);
        //按 byte 对 0x64 异或
        for (int i = 0; i < keyContent.Length; i++)
        {
            keyContent[i] ^= 0x64;
        }

        //AES解密 
        Aes aesAlg = Aes.Create();
        aesAlg.Mode = CipherMode.ECB;
        aesAlg.Key = FileHandling.CORE_KEY;
        aesAlg.Padding = PaddingMode.PKCS7;

        var cryptoTransform = aesAlg.CreateDecryptor();
        var transformFinalBlock = cryptoTransform.TransformFinalBlock(keyContent, 0, keyContent.Length);
        cryptoTransform.Dispose();
        //3.去除前面`neteasecloudmusic`的17个字节
        byte[] key = new byte[transformFinalBlock.Length - 17];
        Array.Copy(transformFinalBlock, 17, key, 0, key.Length);
        return key;
    }

    /**
     * 读取 meta length
     */
    public int ReadMetaLength(FileStream fs)
    {
        byte[] metaLengthArray = new byte[4];
        var readResult = fs.Read(metaLengthArray, 0, metaLengthArray.Length);
        return BitConverter.ToInt32(metaLengthArray);
    }


    /**
     * 读取 meta 内容
     */
    public string ReadMetaContent(FileStream fs, int length)
    {
        byte[] metaContent = new byte[length];
        fs.Read(metaContent, 0, length);

        //按字节对 0x63 异或
        for (var i = 0; i < metaContent.Length; i++)
        {
            metaContent[i] ^= 0x63;
        }

        //去除固定字符 163 key(Don't modify): 前 22个字节
        byte[] metaContent2 = new byte[metaContent.Length - 22];
        Array.Copy(metaContent, 22, metaContent2, 0, metaContent2.Length);

        var fromBase64Array = Convert.FromBase64String(Encoding.UTF8.GetString(metaContent2));

        //AES解密 
        Aes aesAlg = Aes.Create();
        aesAlg.Mode = CipherMode.ECB;
        aesAlg.Key = FileHandling.META_KEY;
        aesAlg.Padding = PaddingMode.PKCS7;

        var cryptoTransform = aesAlg.CreateDecryptor();
        var transformFinalBlock = cryptoTransform.TransformFinalBlock(fromBase64Array, 0, fromBase64Array.Length);
        cryptoTransform.Dispose();

        var meta = Encoding.UTF8.GetString(transformFinalBlock);
        
        //去除 music: 字符
        return meta.Replace("music:", "");
    }
}