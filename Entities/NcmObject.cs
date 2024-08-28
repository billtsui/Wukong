namespace GoldenCudgel.Entities;

public class NcmObject
{
    private string _fileName;

    private byte[] _headerArray;
    private string _header;

    private byte[] _rc4KeyLengthArray;
    private int _rc4KeyLength;

    private byte[] _rc4KeyContentArray;

    private byte[] _metaLengthArray;
    private int _metaLength;

    private byte[] _metaDataArray;
    private string _metaData;

    private byte[] _crcArray;

    private byte[] _albumImageLengthArray;
    private int _albumImageLength;

    private byte[] _albumImageContentArray;

    public string FileName
    {
        get => _fileName;
        set => _fileName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public byte[] HeaderArray
    {
        get => _headerArray;
        set => _headerArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Header
    {
        get => _header;
        set => _header = value ?? throw new ArgumentNullException(nameof(value));
    }

    public byte[] Rc4KeyLengthArray
    {
        get => _rc4KeyLengthArray;
        set => _rc4KeyLengthArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Rc4KeyLength
    {
        get => _rc4KeyLength;
        set => _rc4KeyLength = value;
    }

    public byte[] Rc4KeyContentArray
    {
        get => _rc4KeyContentArray;
        set => _rc4KeyContentArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public byte[] MetaLengthArray
    {
        get => _metaLengthArray;
        set => _metaLengthArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int MetaLength
    {
        get => _metaLength;
        set => _metaLength = value;
    }

    public byte[] MetaDataArray
    {
        get => _metaDataArray;
        set => _metaDataArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string MetaData
    {
        get => _metaData;
        set => _metaData = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return
            $"File name: {FileName},RC4 key length :{Rc4KeyLength} bytes" +
            $",Meta length :{MetaLength} bytes, Album image length :{AlbumImageLength} bytes";
    }

    public byte[] CrcArray
    {
        get => _crcArray;
        set => _crcArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public byte[] AlbumImageLengthArray
    {
        get => _albumImageLengthArray;
        set => _albumImageLengthArray = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int AlbumImageLength
    {
        get => _albumImageLength;
        set => _albumImageLength = value;
    }

    public byte[] AlbumImageContentArray
    {
        get => _albumImageContentArray;
        set => _albumImageContentArray = value ?? throw new ArgumentNullException(nameof(value));
    }
}