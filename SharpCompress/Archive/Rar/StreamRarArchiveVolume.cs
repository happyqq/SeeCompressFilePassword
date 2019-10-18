using System.Collections.Generic;
using System.IO;
using SCFP_Compress.Common;
using SCFP_Compress.Common.Rar;
using SCFP_Compress.Common.Rar.Headers;
using SCFP_Compress.IO;

namespace SCFP_Compress.Archive.Rar
{
    internal class StreamRarArchiveVolume : RarVolume
    {
        internal StreamRarArchiveVolume(Stream stream, string password, Options options)
            : base(StreamingMode.Seekable, stream, password, options)
        {
        }

        internal override IEnumerable<RarFilePart> ReadFileParts()
        {
            return GetVolumeFileParts();
        }

        internal override RarFilePart CreateFilePart(FileHeader fileHeader, MarkHeader markHeader)
        {
            return new SeekableFilePart(markHeader, fileHeader, Stream, Password);
        }
    }
}