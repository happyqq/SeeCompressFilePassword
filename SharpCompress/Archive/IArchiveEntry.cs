using System.IO;
using SCFP_Compress.Common;

namespace SCFP_Compress.Archive
{
    public interface IArchiveEntry : IEntry
    {
        /// <summary>
        /// Opens the current entry as a stream that will decompress as it is read.
        /// Read the entire stream or use SkipEntry on EntryStream.
        /// </summary>
        Stream OpenEntryStream();

        /// <summary>
        /// The archive can find all the parts of the archive needed to extract this entry.
        /// </summary>
        bool IsComplete { get; }

        /// <summary>
        /// The archive instance this entry belongs to
        /// </summary>
        IArchive Archive { get; }
    }
}