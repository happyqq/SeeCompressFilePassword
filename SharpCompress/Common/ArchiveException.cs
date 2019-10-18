using System;

namespace SCFP_Compress.Common
{
    public class ArchiveException : Exception
    {
        public ArchiveException(string message)
            : base(message)
        {
        }
    }
}