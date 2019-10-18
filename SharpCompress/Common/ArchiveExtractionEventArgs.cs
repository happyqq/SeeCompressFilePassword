using System;

namespace SCFP_Compress.Common
{
    public class ArchiveExtractionEventArgs<T> : EventArgs
    {
        internal ArchiveExtractionEventArgs(T entry)
        {
            Item = entry;
        }

        public T Item { get; private set; }
    }
}