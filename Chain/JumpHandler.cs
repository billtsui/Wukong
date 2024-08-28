using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public class JumpHandler : AbstractHandler
{
    public override void Handle(FileStream fs, NcmObject ncmObject)
    {
        fs.Seek(2, SeekOrigin.Current);
        
        base.Handle(fs, ncmObject);
    }
}