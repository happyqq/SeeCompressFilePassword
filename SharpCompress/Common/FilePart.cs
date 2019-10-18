using System.IO;

namespace SCFP_Compress.Common
{
    public abstract class FilePart
    {
        internal abstract string FilePartName { get; }

        internal abstract Stream GetCompressedStream();
        internal abstract Stream GetRawStream();
    }
}