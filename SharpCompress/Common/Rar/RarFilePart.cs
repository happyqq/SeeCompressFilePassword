using System.IO;
using SCFP_Compress.Common.Rar.Headers;

namespace SCFP_Compress.Common.Rar
{
    /// <summary>
    /// This represents a single file part that exists in a rar volume.  A compressed file is one or many file parts that are spread across one or may rar parts.
    /// </summary>
    internal abstract class RarFilePart : FilePart
    {
        internal RarFilePart(MarkHeader mh, FileHeader fh)
        {
            MarkHeader = mh;
            FileHeader = fh;
        }

        internal MarkHeader MarkHeader { get; private set; }

        internal FileHeader FileHeader { get; private set; }

        internal override Stream GetRawStream()
        {
            return null;
        }
    }
}