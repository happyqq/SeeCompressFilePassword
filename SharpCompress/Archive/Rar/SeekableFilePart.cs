using System;
using System.IO;
using SCFP_Compress.Common.Rar;
using SCFP_Compress.Common.Rar.Headers;

namespace SCFP_Compress.Archive.Rar
{
    internal class SeekableFilePart : RarFilePart
    {
        private readonly Stream stream;
        private readonly string password;

        internal SeekableFilePart(MarkHeader mh, FileHeader fh, Stream stream, string password)
            : base(mh, fh)
        {
            this.stream = stream;
            this.password = password;
        }

        internal override Stream GetCompressedStream()
        {
            stream.Position = FileHeader.DataStartPosition;
            if (FileHeader.Salt != null)
            {
#if PORTABLE
                throw new NotSupportedException("Encrypted Rar files aren't supported in portable distro.");
#else
                return new RarCryptoWrapper(stream, password, FileHeader.Salt);
#endif
            }
            return stream;
        }

        internal override string FilePartName
        {
            get { return "Unknown Stream - File Entry: " + FileHeader.FileName; }
        }
    }
}