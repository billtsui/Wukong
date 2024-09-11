using GoldenCudgel.Entities;
using GoldenCudgel.Utils;

namespace GoldenCudgel.Chain;

public class MusicDataHandler : AbstractHandler
{
    public override void Handle(FileInfo file, FileStream fs, NcmObject ncmObject)
    {
        var rc4 = new RC4();
        rc4.KSA(ncmObject.Rc4KeyContentArray);
        var buffer = new byte[0x8000];
        ncmObject.MusicDataArray = [];
        for (int len; (len = fs.Read(buffer)) > 0;)
        {
            rc4.PRGA(buffer, len);
            ncmObject.MusicDataArray.AddRange(buffer);
        }

        base.Handle(file, fs, ncmObject);
    }
}