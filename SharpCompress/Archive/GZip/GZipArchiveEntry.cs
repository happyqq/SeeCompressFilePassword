using System.IO;
using System.Linq;
using SCFP_Compress.Common.GZip;

namespace SCFP_Compress.Archive.GZip
{
    public class GZipArchiveEntry : GZipEntry, IArchiveEntry
    {

        internal GZipArchiveEntry(GZipArchive archive, GZipFilePart part)
            : base(part)
        {
            Archive = archive;
        }

        public virtual Stream OpenEntryStream()
        {
            return Parts.Single().GetCompressedStream();
        }

        #region IArchiveEntry Members
        public IArchive Archive { get; private set; }

        public bool IsComplete
        {
            get { return true; }
        }

        #endregion
    }
}