﻿using CommandLine;
using GoldenCudgel.Chain;
using GoldenCudgel.Entities;
using GoldenCudgel.Utils;

namespace GoldenCudgel;

public class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
    }

    private static void Run(Options options)
    {
        var fileInfoList = FileUtils.ReadFileList(options.Path);
        if (fileInfoList.Count == 0)
        {
            Console.WriteLine("No such file found.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Find {fileInfoList.Count} songs.");

        var headerHandler = AssembleChain();

        foreach (var fileInfo in fileInfoList)
        {
            var ncmObject = new NcmObject
            {
                FileName = fileInfo.Name
            };

            using var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            headerHandler.Handle(fileInfo, fs, ncmObject);
            fs.Close();
            Console.WriteLine(ncmObject.ToString());
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Done!");
    }

    private static HeaderHandler AssembleChain()
    {
        var headerHandler = new HeaderHandler();
        var jumpHandler = new JumpHandler();
        var rc4LengthHandler = new Rc4LengthHandler();
        var rc4ContentHandler = new Rc4ContentHandler();
        var metaLengthHandler = new MetaLengthHandler();
        var metaContentHandler = new MetaContentHandler();
        var checkHandler = new CheckHandler();
        var jump2Handler = new Jump2Handler();
        var albumImageLengthHandler = new AlbumImageLengthHandler();
        var albumImageHandler = new AlbumImageHandler();
        var musicDataHandler = new MusicDataHandler();
        var fileCreateHandler = new FileCreateHandler();

        headerHandler.SetNext(jumpHandler)
            .SetNext(rc4LengthHandler)
            .SetNext(rc4ContentHandler)
            .SetNext(metaLengthHandler)
            .SetNext(metaContentHandler)
            .SetNext(checkHandler)
            .SetNext(jump2Handler)
            .SetNext(albumImageLengthHandler)
            .SetNext(albumImageHandler)
            .SetNext(musicDataHandler)
            .SetNext(fileCreateHandler);

        return headerHandler;
    }

    private class Options
    {
        [Option('p', "path", Required = true, HelpText = "网易云音乐下载目录")]
        public string Path { get; set; }
    }
}