using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public class CheckHandler : AbstractHandler
{
    public override void Handle(FileStream fs, NcmObject ncmObject)
    {
        var crcArray = new Byte[4];
        var readResult = fs.Read(crcArray, 0, crcArray.Length);
        ncmObject.CrcArray = crcArray;
        
        base.Handle(fs, ncmObject);
    }
}