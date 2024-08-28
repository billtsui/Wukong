using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public class AlbumImageLengthHandler : AbstractHandler
{
    public override void Handle(FileStream fs, NcmObject ncmObject)
    {
        var albumImageArray = new Byte[4];
        var readResult = fs.Read(albumImageArray, 0, albumImageArray.Length);

        ncmObject.AlbumImageLengthArray = albumImageArray;
        ncmObject.AlbumImageLength = BitConverter.ToInt32(albumImageArray, 0);
        
        base.Handle(fs, ncmObject);
    }
}