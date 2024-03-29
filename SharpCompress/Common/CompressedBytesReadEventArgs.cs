﻿using System;

namespace SCFP_Compress.Common
{
    public class CompressedBytesReadEventArgs : EventArgs
    {
        /// <summary>
        /// Compressed bytes read for the current entry
        /// </summary>
        public long CompressedBytesRead { get; internal set; }

        /// <summary>
        /// Current file part read for Multipart files (e.g. Rar)
        /// </summary>
        public long CurrentFilePartCompressedBytesRead { get; internal set; }
    }
}