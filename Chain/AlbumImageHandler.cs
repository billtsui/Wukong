using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public class AlbumImageHandler : AbstractHandler
{
    public override void Handle(FileStream fs, NcmObject ncmObject)
    {
        int length = ncmObject.AlbumImageLength;
        var imageArray = new byte[length];
        var readResult = fs.Read(imageArray, 0, length);

        ncmObject.AlbumImageContentArray = imageArray;
        base.Handle(fs, ncmObject);
    }
}