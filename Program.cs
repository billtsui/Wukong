using GoldenCudgel.Chain;
using GoldenCudgel.Entities;
using GoldenCudgel.Utils;
using TagLib;
using Picture = TagLib.Flac.Picture;

namespace GoldenCudgel;

public class Program
{
    [Obsolete("Obsolete")]
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
        var musicDataHandler = new MusicDataHandler();

        headerHandler.SetNext(jumpHandler)
            .SetNext(rc4LengthHandler)
            .SetNext(rc4ContentHandler)
            .SetNext(metaLengthHandler)
            .SetNext(metaContentHandler)
            .SetNext(checkHandler)
            .SetNext(jump2Handler)
            .SetNext(albumImageLengthHandler)
            .SetNext(albumImageHandler)
            .SetNext(musicDataHandler);

        foreach (var fileInfo in fileInfoList)
        {
            var ncmObject = new NcmObject
            {
                FileName = fileInfo.Name
            };

            using var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            headerHandler.Handle(fs, ncmObject);
            fs.Close();

            Console.WriteLine(ncmObject.ToString());

            string destFileName = string.Format("{0}.{1}", fileInfo.Name.Substring(0, fileInfo.Name.Length - 4),
                ncmObject.NeteaseCopyrightData.Format);

            using var stream = new FileStream(destFileName, FileMode.Create, FileAccess.Write);
            stream.Write(ncmObject.MusicDataArray.ToArray());
            stream.Close();

            var file = TagLib.File.Create(destFileName);
            var tag_pic = new TagLib.Picture(new ByteVector(ncmObject.AlbumImageContentArray));
            file.Tag.Pictures = [tag_pic];
            
            file.Tag.Comment = ncmObject.NeteaseCopyrightData.Album;
            file.Tag.Title = ncmObject.NeteaseCopyrightData.MusicName;
            file.Tag.Album = ncmObject.NeteaseCopyrightData.Album;
            file.Save();
        }

        Console.WriteLine("Done!");
    }
}