using GoldenCudgel.Chain;
using GoldenCudgel.Entities;
using GoldenCudgel.Utils;

namespace GoldenCudgel;

public class Program
{
    public static void Main(string[] args)
    {
        var fileInfoList = FileUtils.ReadFileList(Directory.GetCurrentDirectory());
        if (fileInfoList.Count == 0)
        {
            Console.WriteLine("No file found.");
            return;
        }

        Console.WriteLine($"Found {fileInfoList.Count} songs.");

        var headerHandler = new HeaderHandler();
        var jumpHandler = new JumpHandler();
        var rc4LengthHandler = new Rc4LengthHandler();
        var rc4ContentHandler = new Rc4ContentHandler();
        var metaLengthHandler = new MetaLengthHandler();
        var metaContentHandler = new MetaContentHandler();
        var checkHandler = new CheckHandler();
        var jump2Handler = new Jump2Handler();
        var albumImageLengthHandler = new AlbumImageLengthHandler();
        var albumImageHandler = new AlbumImageHandler();

        headerHandler.SetNext(jumpHandler)
                     .SetNext(rc4LengthHandler)
                     .SetNext(rc4ContentHandler)
                     .SetNext(metaLengthHandler)
                     .SetNext(metaContentHandler)
                     .SetNext(checkHandler)
                     .SetNext(jump2Handler)
                     .SetNext(albumImageLengthHandler)
                     .SetNext(albumImageHandler);

        foreach (var fileInfo in fileInfoList)
        {
            var ncmObject = new NcmObject
            {
                FileName = fileInfo.Name
            };
            
            using var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            headerHandler.Handle(fs, ncmObject);
            
            Console.WriteLine(ncmObject.ToString());
        }


        Console.ReadLine();
    }
}