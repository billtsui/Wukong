using GoldenCudgel.Entities;
using TagLib;
using File = TagLib.File;

namespace GoldenCudgel.Chain;

public class FileCreateHandler : AbstractHandler
{
    public override void Handle(FileInfo file, FileStream fs, NcmObject ncmObject)
    {
        var currentDir = file.DirectoryName;
        if (OperatingSystem.IsMacOS()) currentDir += "/";
        if (OperatingSystem.IsWindows()) currentDir += "\\";

        var destPath = $"{currentDir + file.Name[..^4]}.{ncmObject.NeteaseCopyrightData.Format}";

        using var stream = new FileStream(destPath, FileMode.Create, FileAccess.Write);
        stream.Write(ncmObject.MusicDataArray.ToArray());
        stream.Close();

        try
        {
            var musicFile = File.Create(destPath);
            var tagPic = new Picture(new ByteVector(ncmObject.AlbumImageContentArray));
            musicFile.Tag.Pictures = [tagPic];
            musicFile.Tag.Title = ncmObject.NeteaseCopyrightData.MusicName;
            musicFile.Tag.Album = ncmObject.NeteaseCopyrightData.Album;
            musicFile.Tag.Performers = [ncmObject.NeteaseCopyrightData.Artist[0][0]];
            musicFile.Save();
            musicFile.Dispose();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(destPath);
            Console.WriteLine(e);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        base.Handle(file, fs, ncmObject);
    }
}