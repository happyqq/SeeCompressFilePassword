using System.Collections.Generic;
using System.IO;
using SCFP_Compress.Common;
using SCFP_Compress.Common.Rar;
using SCFP_Compress.Common.Rar.Headers;
using SCFP_Compress.IO;

namespace SCFP_Compress.Reader.Rar
{
    public class RarReaderVolume : RarVolume
    {
        internal RarReaderVolume(Stream stream, string password, Options options)
            : base(StreamingMode.Streaming, stream, password, options)
        {
        }

        internal override RarFilePart CreateFilePart(FileHeader fileHeader, MarkHeader markHeader)
        {
            return new NonSeekableStreamFilePart(markHeader, fileHeader);
        }

        internal override IEnumerable<RarFilePart> ReadFileParts()
        {
            return GetVolumeFileParts();
        }
    }
}