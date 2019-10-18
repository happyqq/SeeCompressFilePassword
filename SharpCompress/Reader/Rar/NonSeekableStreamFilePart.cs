using System.IO;
using SCFP_Compress.Common.Rar;
using SCFP_Compress.Common.Rar.Headers;

namespace SCFP_Compress.Reader.Rar
{
    internal class NonSeekableStreamFilePart : RarFilePart
    {
        internal NonSeekableStreamFilePart(MarkHeader mh, FileHeader fh)
            : base(mh, fh)
        {
        }

        internal override Stream GetCompressedStream()
        {
            return FileHeader.PackedStream;
        }

        internal override string FilePartName
        {
            get { return "Unknown Stream - File Entry: " + FileHeader.FileName; }
        }
    }
}