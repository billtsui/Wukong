using GoldenCudgel.Entities;
using GoldenCudgel.Exceptions;

namespace GoldenCudgel.Chain;

public abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual void Handle(FileInfo file, FileStream fs, NcmObject ncmObject)
    {
        try
        {
            _nextHandler?.Handle(file, fs, ncmObject);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (e is FileCreateException exception)
            {
                Console.WriteLine(exception.filePath);
            }
            Console.WriteLine(e);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}