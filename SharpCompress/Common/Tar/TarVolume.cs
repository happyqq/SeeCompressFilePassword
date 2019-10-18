using System.IO;

namespace SCFP_Compress.Common.Tar
{
    public class TarVolume : Volume
    {
        public TarVolume(Stream stream, Options options)
            : base(stream, options)
        {
        }
    }
}