using System.IO;

namespace SCFP_Compress.Common.SevenZip
{
    public class SevenZipVolume : Volume
    {
        public SevenZipVolume(Stream stream, Options options)
            : base(stream, options)
        {
        }
    }
}