﻿using System.IO;

namespace SCFP_Compress.Common.GZip
{
    public class GZipVolume : Volume
    {
#if !PORTABLE && !NETFX_CORE
        private readonly FileInfo fileInfo;
#endif

        public GZipVolume(Stream stream, Options options)
            : base(stream, options)
        {
        }

#if !PORTABLE && !NETFX_CORE
        public GZipVolume(FileInfo fileInfo, Options options)
            : base(fileInfo.OpenRead(), options)
        {
            this.fileInfo = fileInfo;
        }
#endif

        public override bool IsFirstVolume
        {
            get { return true; }
        }

        public override bool IsMultiVolume
        {
            get { return true; }
        }
    }
}