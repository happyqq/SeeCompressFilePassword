using System;
using System.IO;
using SCFP_Compress.Common;

namespace SCFP_Compress.Archive
{
    public interface IWritableArchive : IArchive
    {
        void RemoveEntry(IArchiveEntry entry);

        IArchiveEntry AddEntry(string key, Stream source, bool closeStream, long size = 0, DateTime? modified = null);

        void SaveTo(Stream stream, CompressionInfo compressionType);
    }
}