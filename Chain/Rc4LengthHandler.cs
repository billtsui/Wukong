using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public class Rc4LengthHandler : AbstractHandler
{
    public override void Handle(FileStream fs, NcmObject ncmObject)
    {
        byte[] keyLengthArray = new byte[4];
        var readResult = fs.Read(keyLengthArray, 0, keyLengthArray.Length);
        ncmObject.Rc4KeyLengthArray = keyLengthArray;
        ncmObject.Rc4KeyLength = BitConverter.ToInt32(keyLengthArray);
        
        base.Handle(fs, ncmObject);
    }
}