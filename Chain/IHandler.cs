using GoldenCudgel.Entities;

namespace GoldenCudgel.Chain;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(FileStream fs,NcmObject ncmObject);
}