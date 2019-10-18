using System.IO;

namespace SCFP_Compress.Archive
{
    internal interface IWritableArchiveEntry
    {
        Stream Stream { get; }
    }
}