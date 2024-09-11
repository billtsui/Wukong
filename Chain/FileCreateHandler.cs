using GoldenCudgel.Entities;
using TagLib;
using File = TagLib.File;

namespace GoldenCudgel.Chain;

public class FileCreateHandler : AbstractHandler
{
    public override void Handle(FileInfo file, FileStream fs, NcmObject ncmObject)
    {
        var destFileName = $"{file.Name[..^4]}.{ncmObject.NeteaseCopyrightData.Format}";

        using var stream = new FileStream(destFileName, FileMode.Create, FileAccess.Write);
        stream.Write(ncmObject.MusicDataArray.ToArray());
        stream.Close();

        var musicFile = File.Create(destFileName);
        var tagPic = new Picture(new ByteVector(ncmObject.AlbumImageContentArray));
        musicFile.Tag.Pictures = [tagPic];

        musicFile.Tag.Comment = ncmObject.NeteaseCopyrightData.Album;
        musicFile.Tag.Title = ncmObject.NeteaseCopyrightData.MusicName;
        musicFile.Tag.Album = ncmObject.NeteaseCopyrightData.Album;
        musicFile.Save();
        musicFile.Dispose();
        base.Handle(file, fs, ncmObject);
    }
}