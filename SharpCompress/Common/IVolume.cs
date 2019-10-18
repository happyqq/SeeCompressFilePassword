using System;
#if !PORTABLE && !NETFX_CORE
using System.IO;
#endif

namespace SCFP_Compress.Common
{
    public interface IVolume : IDisposable
    {
    }
}